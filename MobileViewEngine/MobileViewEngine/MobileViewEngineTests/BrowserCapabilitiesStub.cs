using System;
using ClassicDemo;

namespace MobileViewEngineTests
{
    public class BrowserCapabilitiesStub : IBrowserCapabilities
    {
        public bool IsMobileDevice { get; set; }
        public string Platform { get; set; }
    }
}