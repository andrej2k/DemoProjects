using System.Web.Mvc;

namespace ClassicDemo
{
    public class MobileDeviceRule : IDeviceRule
    {
        public IBrowserCapabilities BrowserCapabilities { get; private set; }
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
                result.ViewName = DeviceRulesHelper.AddPathToViewName(viewName, DeviceRulesHelper.MobileDeviceSubPath);
            }
            return result;
        }

    }
}