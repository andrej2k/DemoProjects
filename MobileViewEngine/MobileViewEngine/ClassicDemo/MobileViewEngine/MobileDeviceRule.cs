using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ClassicDemo
{
    public class MobileDeviceRule : IDeviceRule
    {
        //public const bool AllowFallbackToOriginalView = false;
        public IBrowserCapabilities BrowserCapabilities { get; private set; }
        public const string MobileDeviceSubPath = "Mobile/";
        public bool AllowFallbackToOriginalView { get; set; }

        public MobileDeviceRule(IBrowserCapabilities browserCapabilities)
        {
            BrowserCapabilities = browserCapabilities;
            AllowFallbackToOriginalView = false;
        }

        public virtual RuleResult IsRightDevice(ControllerContext controllerContext, string viewName, string masterName)
        {
            RuleResult result = new RuleResult();
            if (BrowserCapabilities.IsMobileDevice)
            {
                result.DeviceIsRight = true;
                result.AllowFallback = AllowFallbackToOriginalView;
                result.ViewName = AddPathToViewName(viewName, MobileDeviceSubPath);
            }
            return result;
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

    }
}