using System.IO;
using System.Web;

namespace ClassicDemo
{
    public static class DeviceRulesHelper
    {
        public const string MobileDeviceSubPath = "Mobile/";

        public static string AddPathToViewName(string viewName, string pathToCombine)
        {
            string combinedViewName;
            if (VirtualPathUtility.IsAppRelative(viewName))
            {
                string directory = VirtualPathUtility.GetDirectory(viewName);
                string newDirectory = VirtualPathUtility.Combine(directory, pathToCombine);
                string fileName = Path.GetFileName(viewName); //VirtualPathUtility.GetFileName does not work in unittest environment
                combinedViewName = VirtualPathUtility.Combine(newDirectory, fileName);
            }
            else
            {
                //simple view name: "Index" => "Mobile/Index"
                combinedViewName = pathToCombine + viewName;
            }
            return combinedViewName;
        }

    }
}