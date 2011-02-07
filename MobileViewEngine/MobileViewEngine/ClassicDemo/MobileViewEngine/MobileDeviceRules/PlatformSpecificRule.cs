using System.Collections.Generic;
using System.Web.Mvc;

namespace ClassicDemo
{
    public class PlatformSpecificRule : IDeviceRule
    {
        private readonly IDictionary<string,string> supportedPlatformsAndViewPrefix;
        public IBrowserCapabilities BrowserCapabilities { get; private set; }

        public PlatformSpecificRule(IBrowserCapabilities browserCapabilities, IEnumerable<string> supportedPlatforms)
        {
            BrowserCapabilities = browserCapabilities;
            supportedPlatformsAndViewPrefix = new Dictionary<string,string>();
            foreach (string supportedPlatform in supportedPlatforms)
            {
                string platformViewPrefix = string.Concat(DeviceRulesHelper.MobileDeviceSubPath, supportedPlatform, "/");
                supportedPlatformsAndViewPrefix.Add(supportedPlatform, platformViewPrefix);
            }
        }

        public RuleResult IsRightDevice(ControllerContext controllerContext, string viewName, string masterName)
        {
            RuleResult result = new RuleResult();
            string devicePlatform = BrowserCapabilities.Platform;
            if (!(string.IsNullOrEmpty(devicePlatform)) && (supportedPlatformsAndViewPrefix.ContainsKey(devicePlatform)))
            {
                //device belongs to supported platforms, return platform specific subpath e.g. "Mobile/IPhone/ViewName"
                result.DeviceIsRight = true;
                result.AllowFallback = true; //allow fallback to "Mobile/ViewName" if platform specific view is not found
                result.ViewName = DeviceRulesHelper.AddPathToViewName(viewName, supportedPlatformsAndViewPrefix[devicePlatform]);
            }
            return result;
        }

    }
}