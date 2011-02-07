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