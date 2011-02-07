using System.Collections.Generic;
using System.Web.Mvc;

namespace ClassicDemo
{
    public static class MobileViewEngineHelper
    {
        public static IList<string> SupportedDevicePlatformsWithViewPath
        {
            get { return new List<string> {"Android", "IPhone"}; }
        }

        public static void AddMobileViewEngine<T>(this ViewEngineCollection ves) where T : IViewEngine, new()
        {
            IEnumerable<IDeviceRule> deviceRules = CreateDeviceRules(new HttpContextBrowserCapabilities());
            ves.Add(new MobileViewEngine(new T(), deviceRules));
        }

        public static IEnumerable<IDeviceRule> CreateDeviceRules(IBrowserCapabilities browserCapabilities)
        {
            //Order is important!!!
            return new List<IDeviceRule>
                       {
                           new MobileDeviceRule(browserCapabilities),
                           new PlatformSpecificRule(browserCapabilities,
                                                    SupportedDevicePlatformsWithViewPath),
                       };
        }
    }
}