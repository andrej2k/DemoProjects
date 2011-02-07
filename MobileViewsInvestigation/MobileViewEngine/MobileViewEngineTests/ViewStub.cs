using System;
using System.IO;
using System.Web.Mvc;

namespace MobileViewEngineTests
{
  public class ViewStub : IView
  {
    private readonly string viewLocation;

    public ViewStub(string viewLocation)
    {
      this.viewLocation = viewLocation;
    }

    public string ViewLocation
    {
      get { return viewLocation; }
    }

    public void Render(ViewContext viewContext, TextWriter writer)
    {
      throw new NotImplementedException();
    }
  }
}