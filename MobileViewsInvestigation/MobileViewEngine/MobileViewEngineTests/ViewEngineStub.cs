using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MobileViewEngineTests
{
  public class ViewEngineStub : IViewEngine
  {
    private readonly IDictionary<string, ViewEngineResult> existingViews;
    private readonly IDictionary<string, ViewEngineResult> viewsCache = new Dictionary<string, ViewEngineResult>();
    private readonly ViewEngineResult emptyViewEngineResult = new ViewEngineResult(new List<string>());

    public ViewEngineStub(IEnumerable<string> viewNames)
    {
      existingViews = new Dictionary<string, ViewEngineResult>();
      foreach (string viewName in viewNames)
      {
        existingViews.Add(viewName, new ViewEngineResult(new ViewStub(viewName), this));
      }
    }

    public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
    {
      throw new NotImplementedException();
    }

    public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
    {
      if (useCache)
      {
        string cacheKey = CalculateCacheKey(controllerContext, viewName, masterName);
        if (viewsCache.ContainsKey(cacheKey))
        {
          return viewsCache[cacheKey];
        }
        return emptyViewEngineResult;
      }
      if (existingViews.ContainsKey(viewName))
      {
        //view found, add view into cache and return it
        string cacheKey = CalculateCacheKey(controllerContext, viewName, masterName);
        ViewEngineResult foundViewEngineResult = existingViews[viewName];
        viewsCache[cacheKey] = foundViewEngineResult;
        return foundViewEngineResult;
      }

      return emptyViewEngineResult; //return empty view
    }

    private static string CalculateCacheKey(ControllerContext controllerContext, string viewName, string masterName)
    {
      //For simplicity we will use only viewname and masterName without contrloller for cache key signature
      return string.Format("{0} - {1}", viewName, masterName);
    }

    public void ReleaseView(ControllerContext controllerContext, IView view)
    {
      throw new NotImplementedException();
    }
  }
}