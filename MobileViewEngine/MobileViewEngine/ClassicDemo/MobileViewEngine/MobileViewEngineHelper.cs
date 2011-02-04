using System.Collections.Generic;
using System.Web.Mvc;

namespace ClassicDemo
{
    public static class MobileViewEngineHelper
    {
        //public const string MobileDeviceSubPath = "Mobile/";

        public static IDictionary<string, string> SupportedDevicePlatformsWithViewPath
        {
            get { return new Dictionary<string, string> {{"Android", "Mobile/Android/"}, {"IPhone", "Mobile/IPhone/"}}; }
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
                           new PlatformSpecificRule(browserCapabilities,
                                                    SupportedDevicePlatformsWithViewPath),
                           new MobileDeviceRule(browserCapabilities)
                       };
        }
    }
}