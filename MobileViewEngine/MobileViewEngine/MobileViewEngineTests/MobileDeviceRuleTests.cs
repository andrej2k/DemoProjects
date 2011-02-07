using ClassicDemo;
using NUnit.Framework;

namespace MobileViewEngineTests
{
    [TestFixture]
    public class MobileDeviceRuleTests
    {
        private MobileDeviceRule mobileDeviceRule;
        private BrowserCapabilitiesStub browserCapabilities;

        [SetUp]
        public void Setup()
        {
            browserCapabilities = new BrowserCapabilitiesStub();
            mobileDeviceRule = new MobileDeviceRule(browserCapabilities);
        }

        [Test]
        public void CanAddMobilePathToShortIndexName()
        {
            Assert.That(MobileDeviceRule.AddPathToViewName("Index", "Mobile/"), Is.EqualTo("Mobile/Index"));
        }

        [Test]
        public void CanAddMobilePathToSpecifIndexName()
        {
            Assert.That(MobileDeviceRule.AddPathToViewName("~/Views/Account/LogOn.aspx", "Mobile/"),
                        Is.EqualTo("~/Views/Account/Mobile/LogOn.aspx"));
        }

        [Test]
        public void CanAddMobilePathToSpecifIndexNameShort()
        {
            Assert.That(MobileDeviceRule.AddPathToViewName("~/LogOn.aspx", "Mobile/"), Is.EqualTo("~/Mobile/LogOn.aspx"));
        }

        [Test]
        public void CanAddMobilePathToViewNameFileNameOnly()
        {
            Assert.That(MobileDeviceRule.AddPathToViewName("LogOn.aspx", "Mobile/"), Is.EqualTo("Mobile/LogOn.aspx"));
        }

        [Test]
        public void ReturnFalseOnNonMobileBrowser()
        {
            browserCapabilities.IsMobileDevice = false;
            RuleResult result = mobileDeviceRule.IsRightDevice(null, "Index", string.Empty);
            Assert.That(result.DeviceIsRight, Is.EqualTo(false));
        }

        [Test]
        public void ChangeViewNameOnMobileBrowser()
        {
            browserCapabilities.IsMobileDevice = true;
            RuleResult result = mobileDeviceRule.IsRightDevice(null, "Index", string.Empty);
            Assert.That(result.DeviceIsRight, Is.EqualTo(true));
            Assert.That(result.ViewName, Is.EqualTo("Mobile/Index"));
        }
    }
}