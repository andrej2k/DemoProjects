using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ClassicDemo
{
    public class PlatformSpecificRule : MobileDeviceRule
    {
        private readonly IDictionary<string,string> supportedPlatformsAndViewPrefix;

        public PlatformSpecificRule(IBrowserCapabilities browserCapabilities, IEnumerable<string> supportedPlatforms)
            : base(browserCapabilities)
        {
            //this.supportedPlatforms = supportedPlatforms;
            supportedPlatformsAndViewPrefix = new Dictionary<string,string>();
            foreach (string supportedPlatform in supportedPlatforms)
            {
                string platformViewPrefix = string.Concat(MobileDeviceSubPath, supportedPlatform, "/");
                supportedPlatformsAndViewPrefix.Add(supportedPlatform, platformViewPrefix);
            }
        }

        public override RuleResult IsRightDevice(ControllerContext controllerContext, string viewName, string masterName)
        {
            RuleResult baseResult = base.IsRightDevice(controllerContext, viewName, masterName);
            if (!baseResult.DeviceIsRight)
            {
                //Device is not mobile, we have nothinig to do here
                return baseResult;
            }

            RuleResult result = new RuleResult();
            string devicePlatform = BrowserCapabilities.Platform;
            if (!(string.IsNullOrEmpty(devicePlatform)) && (supportedPlatformsAndViewPrefix.ContainsKey(devicePlatform)))
            {
                //device belongs to supported platforms, return platform specific subpath e.g. "Mobile/IPhone/ViewName"
                result.DeviceIsRight = true;
                result.AllowFallback = true; //allow fallback to "Mobile/ViewName" if platform specific view is not found
                result.ViewName = AddPathToViewName(viewName, supportedPlatformsAndViewPrefix[devicePlatform]);
            }
            return result;
        }

    }
}