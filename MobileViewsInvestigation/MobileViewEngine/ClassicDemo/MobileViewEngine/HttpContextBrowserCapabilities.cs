using System;
using System.Web;

namespace ClassicDemo
{
  public class HttpContextBrowserCapabilities : IBrowserCapabilities
  {
    public bool IsMobileDevice
    {
      get { return HttpContext.Current.Request.Browser.IsMobileDevice; }
    }

    public string Platform
    {
      get { return HttpContext.Current.Request.Browser.Platform; }
    }
  }
}