using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassicDemo.Controllers
{
  [HandleError]
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      ViewData["Message"] = "Welcome to ASP.NET MVC!";

      return View();
      //~/Views/Error.aspx
      //return View("~/Views/Home/Index.aspx");
    }

    public ActionResult About()
    {
        return View("~/Views/Home/About.aspx");
      //return View();
    }
  }
}
