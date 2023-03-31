using AutoFrameworkTest.BaseObjects;
using OpenQA.Selenium;

namespace AutoFrameworkTest.BaseObjects
{
    public static class DriverContext
    {
        private static IWebDriver _driver;
        public static IWebDriver Driver
        {
            get
            {
                return _driver;
            }
            set { _driver = value; }
        }

        public static Browser Browser { get; set; }
    }
}
