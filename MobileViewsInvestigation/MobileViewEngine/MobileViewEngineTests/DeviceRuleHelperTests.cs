using ClassicDemo;
using NUnit.Framework;

namespace MobileViewEngineTests
{
    [TestFixture]
    public class DeviceRuleHelperTests
    {
        [Test]
        public void CanAddMobilePathToShortIndexName()
        {
            Assert.That(DeviceRulesHelper.AddPathToViewName("Index", "Mobile/"), Is.EqualTo("Mobile/Index"));
        }

        [Test]
        public void CanAddMobilePathToSpecifIndexName()
        {
            Assert.That(DeviceRulesHelper.AddPathToViewName("~/Views/Account/LogOn.aspx", "Mobile/"),
                        Is.EqualTo("~/Views/Account/Mobile/LogOn.aspx"));
        }

        [Test]
        public void CanAddMobilePathToSpecifIndexNameShort()
        {
            Assert.That(DeviceRulesHelper.AddPathToViewName("~/LogOn.aspx", "Mobile/"), Is.EqualTo("~/Mobile/LogOn.aspx"));
        }

        [Test]
        public void CanAddMobilePathToViewNameFileNameOnly()
        {
            Assert.That(DeviceRulesHelper.AddPathToViewName("LogOn.aspx", "Mobile/"), Is.EqualTo("Mobile/LogOn.aspx"));
        }

    }
}