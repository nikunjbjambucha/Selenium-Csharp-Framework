using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoFrameworkTest.BaseObjects
{
    public class Browser
    {
        private readonly IWebDriver _driver;

        public Browser(IWebDriver driver)
        {
            _driver = driver;

        }
        public void GotoUrl(string url)
        {
            DriverContext.Driver.Manage().Window.Maximize();
            DriverContext.Driver.Url = url;
        }

    }
    public enum BrowserType
    {
        InternetExplorer,
        FireFox,
        Chrome
    }
}
