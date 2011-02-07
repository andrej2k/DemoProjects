using System.Collections.Generic;
using ClassicDemo;
using NUnit.Framework;
using Rhino.Mocks;

namespace MobileViewEngineTests
{
    [TestFixture]
    public class PlatformSpecificRuleTests
    {
        private PlatformSpecificRule platformSpecificRule;
        private BrowserCapabilitiesStub browserCapabilities;
        private readonly IList<string> supportedPlatforms = new List<string> { "Android", "IPhone" };

        [SetUp]
        public void Setup()
        {
            browserCapabilities = new BrowserCapabilitiesStub();
            platformSpecificRule = new PlatformSpecificRule(browserCapabilities, supportedPlatforms);
        }

        [Test]
        public void CanResolvePlatform()
        {
            browserCapabilities.IsMobileDevice = true;
            browserCapabilities.Platform = "IPhone";
            RuleResult result = platformSpecificRule.IsRightDevice(null, "Index", string.Empty);
            Assert.That(result.DeviceIsRight, Is.EqualTo(true));
            Assert.That(result.ViewName, Is.EqualTo("Mobile/IPhone/Index"));
        }

        [Test]
        public void ReturnFalseIfDeviceIsNotMobile()
        {
            browserCapabilities.IsMobileDevice = false;
            RuleResult result = platformSpecificRule.IsRightDevice(null, "Index", string.Empty);
            Assert.That(result.DeviceIsRight, Is.EqualTo(false));
        }

        [Test]
        public void ReturnFalseIfPlatformIsNotSupported()
        {
            browserCapabilities.IsMobileDevice = true;
            browserCapabilities.Platform = "SomeUnsupportedPlatform";
            browserCapabilities.IsMobileDevice = false;
            RuleResult result = platformSpecificRule.IsRightDevice(null, "Index", string.Empty);
            Assert.That(result.DeviceIsRight, Is.EqualTo(false));
        }
    }
}