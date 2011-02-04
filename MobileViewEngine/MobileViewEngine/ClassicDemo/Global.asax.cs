using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClassicDemo
{
  // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
  // visit http://go.microsoft.com/?LinkId=9394801

  public class MvcApplication : System.Web.HttpApplication
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
          "Default", // Route name
          "{controller}/{action}/{id}", // URL with parameters
          new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
      );

    }

// ReSharper disable InconsistentNaming
    protected void Application_Start()
// ReSharper restore InconsistentNaming
    {
      HttpCapabilitiesBase.BrowserCapabilitiesProvider = new FiftyOne.Foundation.Mobile.Detection.MobileCapabilitiesProvider();

      AreaRegistration.RegisterAllAreas();
      RegisterRoutes(RouteTable.Routes);

      RegisterViewEngines();
    }

    private static void RegisterViewEngines()
    {
        ViewEngines.Engines.Clear();
        ViewEngines.Engines.AddMobileViewEngine<WebFormViewEngine>();
    }

    //protected void Application_AcquireRequestState(object sender, EventArgs e)
    //{
    //  // Process redirection logic.
    //  FiftyOne.Foundation.Mobile.Redirection.RedirectModule.OnPostAcquireRequestState(sender, e);
    //}

  }
}