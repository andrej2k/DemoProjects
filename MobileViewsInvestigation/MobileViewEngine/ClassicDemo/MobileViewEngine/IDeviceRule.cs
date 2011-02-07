using System.Web.Mvc;

namespace ClassicDemo
{
    public interface IDeviceRule
    {
        RuleResult IsRightDevice(ControllerContext controllerContext, string viewName, string masterName);
    }
}