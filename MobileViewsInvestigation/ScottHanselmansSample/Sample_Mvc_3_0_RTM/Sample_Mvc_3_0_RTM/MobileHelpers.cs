using System;
using System.Web.Mvc;

namespace ClassicDemo
{
  public static class MobileHelpers
  {

    public static bool IsMobileDevice(this ControllerContext c)
    {
      return c.HttpContext.Request.Browser.IsMobileDevice;
    }

    public static bool UserAgentContains(this ControllerContext c, string agentToFind)
    {
      return (c.HttpContext.Request.UserAgent.IndexOf(agentToFind, StringComparison.OrdinalIgnoreCase) > 0);
    }
 
    public static void AddMobile<T>(this ViewEngineCollection viewEngineCollection, Func<ControllerContext, bool> isTheRightDevice, string pathToSearch)
        where T : IViewEngine, new()
    {
      viewEngineCollection.Add(new CustomMobileViewEngine(isTheRightDevice, pathToSearch, new T()));
    }

    public static void AddIPhone<T>(this ViewEngineCollection ves) //specific example helper
        where T : IViewEngine, new()
    {
      ves.Add(new CustomMobileViewEngine(c => c.UserAgentContains("iPhone"), "Mobile/iPhone", new T()));
    }

    public static void AddGenericMobile<T>(this ViewEngineCollection ves)
        where T : IViewEngine, new()
    {
      ves.Add(new CustomMobileViewEngine(c => c.IsMobileDevice(), "Mobile", new T()));
    }
  }
}