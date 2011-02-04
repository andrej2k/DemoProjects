using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ClassicDemo
{
    public class MobileViewEngine : IViewEngine
    {
        private static readonly ViewEngineResult emptyViewEngineResult = new ViewEngineResult(new List<string>());
        private readonly IEnumerable<IDeviceRule> deviceRules;

        public IViewEngine OriginalViewEngine { get; private set; }

        public MobileViewEngine(IViewEngine originalViewEngine, IEnumerable<IDeviceRule> deviceRules)
        {
            this.deviceRules = deviceRules;
            OriginalViewEngine = originalViewEngine;
        }

        public ViewEngineResult FindView(ControllerContext controllerContext,
                                         string viewName,
                                         string masterName,
                                         bool useCache)
        {
            //temporary log stub
            if ((controllerContext.HttpContext is HttpContextWrapper))
            {
                controllerContext.HttpContext.Items[Constants.DebugViewEngineContentKey] = viewName + " - " + useCache;
            }

            foreach (IDeviceRule deviceRule in deviceRules)
            {
                RuleResult ruleResult = deviceRule.IsRightDevice(controllerContext);
                if (ruleResult.DeviceIsRight)
                {
                    string changedViewName = AddPathToViewName(viewName, ruleResult.ViewSubPath);
                    FindViewResult findViewSearchResult = CallOriginalViewEngineAndCheckIfViewExists(controllerContext,
                                                                                                     changedViewName,
                                                                                                     masterName,
                                                                                                     useCache);
                    if ((findViewSearchResult.ViewExists) || (!ruleResult.AllowFallback))
                    {
                        return findViewSearchResult.ViewEngineResult;
                    }
                }
            }

            //if device does not fit to the rules or views specified by rules were not found, then call original viewEngine without changes
            return OriginalViewEngine.FindView(controllerContext, viewName, masterName, useCache);
        }

        public ViewEngineResult FindPartialView(ControllerContext controllerContext,
                                                string partialViewName,
                                                bool useCache)
        {
            return OriginalViewEngine.FindPartialView(controllerContext, partialViewName, useCache);
        }

        public void ReleaseView(ControllerContext controllerContext, IView view)
        {
            OriginalViewEngine.ReleaseView(controllerContext, view);
        }

        private static bool IsResultEmpty(ViewEngineResult result)
        {
            return result == null || result.View == null;
        }

        public static string AddPathToViewName(string viewName, string pathToCombine)
        {
            string combinedViewName;
            if (VirtualPathUtility.IsAppRelative(viewName))
            {
                string directory = VirtualPathUtility.GetDirectory(viewName);
                string newDirectory = VirtualPathUtility.Combine(directory, pathToCombine);
                string fileName = Path.GetFileName(viewName); //VirtualPathUtility.GetFileName does not work in unittest environment
                combinedViewName = VirtualPathUtility.Combine(newDirectory, fileName);
            }
            else
            {
                //simple view name: "Index" => "Mobile/Index"
                combinedViewName = pathToCombine + viewName;
            }
            return combinedViewName;
        }

        private bool ViewExistsOutOfCache(ControllerContext controllerContext, string viewName, string masterName)
        {
            //TODO: we can add cache here to avoid call of original view with useCache set to false
            ViewEngineResult nonCachedResult = OriginalViewEngine.FindView(controllerContext,
                                                                           viewName,
                                                                           masterName,
                                                                           false);
            if (!IsResultEmpty(nonCachedResult))
            {
                //that means that view exists, it just wasn't cached
                return true;
            }
            return false;
        }

        private FindViewResult CallOriginalViewEngineAndCheckIfViewExists(
            ControllerContext controllerContext,
            string mobileSpecificViewPath,
            string masterName,
            bool useCache)
        {
            ViewEngineResult result = OriginalViewEngine.FindView(controllerContext,
                                                                  mobileSpecificViewPath,
                                                                  masterName,
                                                                  useCache);
            if (!IsResultEmpty(result))
            {
                return new FindViewResult(result, true);
            }
            if (useCache && ViewExistsOutOfCache(controllerContext, mobileSpecificViewPath, masterName))
            {
                //that means that view exists, it just wasn't cached. The view will be cached when view engine will 
                //be called next time in the same request with (useCache == false)
                return new FindViewResult(emptyViewEngineResult, true);
            }
            return new FindViewResult(result, false);
        }

        private class FindViewResult
        {
            public FindViewResult(ViewEngineResult viewEngineResult, bool viewExists)
            {
                ViewEngineResult = viewEngineResult;
                ViewExists = viewExists;
            }

            public ViewEngineResult ViewEngineResult { get; private set; }
            public bool ViewExists { get; private set; }
        }
    }
}