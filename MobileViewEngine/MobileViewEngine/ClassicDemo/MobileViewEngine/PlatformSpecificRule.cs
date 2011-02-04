using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ClassicDemo
{
    public class PlatformSpecificRule : MobileDeviceRule
    {
        private readonly IDictionary<string, string> supportedPlatforms;

        public PlatformSpecificRule(IBrowserCapabilities browserCapabilities, IDictionary<string, string> supportedPlatforms)
            : base(browserCapabilities)
        {
            this.supportedPlatforms = supportedPlatforms;
        }

        public override RuleResult IsRightDevice(ControllerContext controllerContext)
        {
            RuleResult baseResult = base.IsRightDevice(controllerContext);
            if (!baseResult.DeviceIsRight)
            {
                //Device is not mobile, we have nothinig to do here
                return baseResult;
            }

            RuleResult result = new RuleResult();
            string devicePlatform = BrowserCapabilities.Platform;
            if (!(string.IsNullOrEmpty(devicePlatform)) && (supportedPlatforms.ContainsKey(devicePlatform)))
            {
                //device belongs to supported platforms, return platform specific subpath e.g. "Mobile/IPhone/ViewName"
                result.DeviceIsRight = true;
                result.AllowFallback = true; //allow fallback to "Mobile/ViewName" if platform specific view is not found
                result.ViewSubPath = supportedPlatforms[devicePlatform];
            }
            return result;
        }
    }
}