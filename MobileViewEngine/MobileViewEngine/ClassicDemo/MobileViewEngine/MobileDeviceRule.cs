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

        public virtual RuleResult IsRightDevice(ControllerContext controllerContext)
        {
            RuleResult result = new RuleResult();
            if (BrowserCapabilities.IsMobileDevice)
            {
                result.DeviceIsRight = true;
                result.AllowFallback = AllowFallbackToOriginalView;
                result.ViewSubPath = MobileDeviceSubPath;
            }
            return result;
        }
    }

    //public enum 
}