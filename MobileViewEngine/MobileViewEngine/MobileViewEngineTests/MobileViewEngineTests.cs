using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ClassicDemo;
using NUnit.Framework;

namespace MobileViewEngineTests
{
    [TestFixture]
    public class MobileViewEngineTests
    {
        private MobileViewEngine mobileViewEngine;
        private ViewEngineStub originalViewEngineStub;
        private ViewEngineCollection viewEngineCollection;
        private BrowserCapabilitiesStub browserCapabilities;
        private ControllerContext controllerContext;
        private const string MasterName = "MasterPage";
        private const string DefaultTargetViewName = "Index";
        private IEnumerable<IDeviceRule> deviceRules;

        [SetUp]
        public void Setup()
        {
            controllerContext = new ControllerContext();
            originalViewEngineStub =
                new ViewEngineStub(new List<string>
                                       {
                                           "Index",
                                           "Mobile/Index",
                                           "Mobile/Android/Index",
                                           "Home",
                                           "ViewWithoutPlatformSpecifics",
                                           "Mobile/ViewWithoutPlatformSpecifics",
                                           "~/Views/Account/LogOn.aspx",
                                           "~/Views/Account/Mobile/LogOn.aspx",
                                           "~/Views/Account/WithoutMobileView.aspx"
                                       });
            browserCapabilities = new BrowserCapabilitiesStub();
            deviceRules = MobileViewEngineHelper.CreateDeviceRules(browserCapabilities);
            mobileViewEngine = new MobileViewEngine(originalViewEngineStub, deviceRules);
            
            IList<IViewEngine> myList = new List<IViewEngine>();
            myList.Add(mobileViewEngine);

            viewEngineCollection = new ViewEngineCollection {mobileViewEngine};
        }

        [Test]
        public void CanResolveNonMobileViewDuringSingleCall()
        {
            FindViewAndValidate(ConfigureBrowserToBeNonMobileDevice, "Index");
        }

        [Test]
        public void CanResolveMobileViewDuringSingleCall()
        {
            FindViewAndValidate(ConfigureBrowserToBeUnknownMobileDevice, "Mobile/Index");
        }

        [Test]
        public void CanResolveAndroidViewDuringSingleCall()
        {
            FindViewAndValidate(ConfigureBrowserToBeAndroidMobileDevice, "Mobile/Android/Index");
        }

        [Test]
        public void DoesNotFallBackToOriginalViewFromSupportePlatform()
        {
            FindViewAndValidate(ConfigureBrowserToBeAndroidMobileDevice, null, "Home");
        }

        [Test]
        public void CanFallBackToOriginalViewFromSupportePlatform()
        {
            SetFallbackToOriginalView();
            FindViewAndValidate(ConfigureBrowserToBeAndroidMobileDevice, "Home", "Home");
        }

        [Test]
        public void DoesNotFallBackToOriginalViewFromMobileDevice()
        {
            FindViewAndValidate(ConfigureBrowserToBeUnknownMobileDevice, null, "Home");
        }

        [Test]
        public void CanFallBackToOriginalViewFromMobileDevice()
        {
            SetFallbackToOriginalView();
            FindViewAndValidate(ConfigureBrowserToBeUnknownMobileDevice, "Home", "Home");
        }

        [Test]
        public void CanFallBackToMobileViewIfPlatformViewNotFound()
        {
            SetFallbackToOriginalView();
            FindViewAndValidate(ConfigureBrowserToBeAndroidMobileDevice,
                                "Mobile/ViewWithoutPlatformSpecifics",
                                "ViewWithoutPlatformSpecifics");
        }

        [Test]
// ReSharper disable InconsistentNaming
        public void Combination_NonMobile_Mobile_Android_NonMobile()
// ReSharper restore InconsistentNaming
        {
            FindViewAndValidate(ConfigureBrowserToBeNonMobileDevice, "Index");
            FindViewAndValidate(ConfigureBrowserToBeUnknownMobileDevice, "Mobile/Index");
            FindViewAndValidate(ConfigureBrowserToBeAndroidMobileDevice, "Mobile/Android/Index");
            FindViewAndValidate(ConfigureBrowserToBeNonMobileDevice, "Index");
        }

        [Test]
        // ReSharper disable InconsistentNaming
        public void Combination_NonMobile_Android_Mobile_NonMobile()
            // ReSharper restore InconsistentNaming
        {
            FindViewAndValidate(ConfigureBrowserToBeNonMobileDevice, "Index");
            FindViewAndValidate(ConfigureBrowserToBeAndroidMobileDevice, "Mobile/Android/Index");
            FindViewAndValidate(ConfigureBrowserToBeUnknownMobileDevice, "Mobile/Index");
            FindViewAndValidate(ConfigureBrowserToBeNonMobileDevice, "Index");
        }

        [Test]
        // ReSharper disable InconsistentNaming
        public void Combination_Mobile_NonMobile_Android_Mobile()
            // ReSharper restore InconsistentNaming
        {
            FindViewAndValidate(ConfigureBrowserToBeUnknownMobileDevice, "Mobile/Index");
            FindViewAndValidate(ConfigureBrowserToBeNonMobileDevice, "Index");
            FindViewAndValidate(ConfigureBrowserToBeAndroidMobileDevice, "Mobile/Android/Index");
            FindViewAndValidate(ConfigureBrowserToBeUnknownMobileDevice, "Mobile/Index");
        }

        [Test]
        // ReSharper disable InconsistentNaming
        public void Combination_Mobile_Android_NonMobile_Mobile()
            // ReSharper restore InconsistentNaming
        {
            FindViewAndValidate(ConfigureBrowserToBeUnknownMobileDevice, "Mobile/Index");
            FindViewAndValidate(ConfigureBrowserToBeNonMobileDevice, "Index");
            FindViewAndValidate(ConfigureBrowserToBeAndroidMobileDevice, "Mobile/Android/Index");
            FindViewAndValidate(ConfigureBrowserToBeUnknownMobileDevice, "Mobile/Index");
        }

        [Test]
        // ReSharper disable InconsistentNaming
        public void Combination_Android_NonMobile_Mobile_Android()
            // ReSharper restore InconsistentNaming
        {
            FindViewAndValidate(ConfigureBrowserToBeAndroidMobileDevice, "Mobile/Android/Index");
            FindViewAndValidate(ConfigureBrowserToBeNonMobileDevice, "Index");
            FindViewAndValidate(ConfigureBrowserToBeUnknownMobileDevice, "Mobile/Index");
            FindViewAndValidate(ConfigureBrowserToBeAndroidMobileDevice, "Mobile/Android/Index");
        }

        [Test]
        // ReSharper disable InconsistentNaming
        public void Combination_Android_Mobile_NonMobile_Android()
            // ReSharper restore InconsistentNaming
        {
            FindViewAndValidate(ConfigureBrowserToBeAndroidMobileDevice, "Mobile/Android/Index");
            FindViewAndValidate(ConfigureBrowserToBeUnknownMobileDevice, "Mobile/Index");
            FindViewAndValidate(ConfigureBrowserToBeNonMobileDevice, "Index");
            FindViewAndValidate(ConfigureBrowserToBeAndroidMobileDevice, "Mobile/Android/Index");
        }

        private void FindViewAndValidate(Action configureBrowserAction,
                                         string targetViewPath,
                                         string viewName = DefaultTargetViewName)
        {
            configureBrowserAction();
            Assert.That(FindViewAndGetFoundViewLocation(viewName), Is.EqualTo(targetViewPath));
        }

        private string FindViewAndGetFoundViewLocation(string viewName)
        {
            ViewEngineResult viewEngineResult = viewEngineCollection.FindView(controllerContext, viewName, MasterName);
            ViewStub viewStub = (ViewStub) viewEngineResult.View;
            if (viewStub == null)
            {
                return null;
            }
            return viewStub.ViewLocation;
        }

        private void ConfigureBrowserToBeUnknownMobileDevice()
        {
            browserCapabilities.IsMobileDevice = true;
            browserCapabilities.Platform = "SomeUnknownDevicePlatform";
        }

        private void ConfigureBrowserToBeAndroidMobileDevice()
        {
            browserCapabilities.IsMobileDevice = true;
            browserCapabilities.Platform = "Android";
        }

        private void ConfigureBrowserToBeNonMobileDevice()
        {
            browserCapabilities.IsMobileDevice = false;
            browserCapabilities.Platform = "Firefox";
        }

        private void SetFallbackToOriginalView()
        {
            foreach (IDeviceRule deviceRule in deviceRules)
            {
                if (deviceRule.GetType() == typeof (MobileDeviceRule))
                {
                    ((MobileDeviceRule) deviceRule).AllowFallbackToOriginalView = true;
                    return;
                }
            }
            throw new Exception("MobileDeviceRule was not found");
        }
    }
}