using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using RelevantCodes.ExtentReports;
using System.Threading;
using AutoFrameworkTest.Helpers;
using AutoFrameworkTest.Pages;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using AutoFrameworkTest.Data;
using AutoFrameworkTest.BaseObjects;

namespace AutoFrameworkTest
{
    [TestClass]
    public class AutoFrameworkTest : Base
    {
        #region Fields
        public TestContext TestContext { get; set; }
        public static string startupPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public static string url = ConfigurationManager.AppSettings["URL"];
        public static string user = ConfigurationManager.AppSettings["User"];
        public static string pass = ConfigurationManager.AppSettings["Password"];
        string driverPath = startupPath + ConfigurationManager.AppSettings["driverPath"];
        string fileDownloadPath = startupPath + ConfigurationManager.AppSettings["FileDownloadPath"];
        #endregion

        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            ExtentManager();
        }

        #region private methods

        public BrowserType SetBrowser(String Browser)
        {
            if(Browser.Equals("Chrome"))
                return BrowserType.Chrome;
            else if(Browser.Equals("FireFox"))
                return BrowserType.FireFox;
            else if(Browser.Equals("Edge"))
                return BrowserType.InternetExplorer;
            else
                return BrowserType.Chrome;
        }

        public void OpenBrowser(BrowserType browserType)
        {
            switch (browserType)
            {

                case BrowserType.InternetExplorer:
                    System.Environment.SetEnvironmentVariable("IWebDriver.edge.driver", driverPath + @"\msedgedriver.exe");
                    var edgeDriverService = Microsoft.Edge.SeleniumTools.EdgeDriverService.CreateChromiumService();
                    var edgeoptions = new Microsoft.Edge.SeleniumTools.EdgeOptions();
                    edgeoptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    edgeoptions.UseChromium = true;
                    var FileDownloadPathEdge = fileDownloadPath;
                    edgeoptions.AddUserProfilePreference("download.default_directory", FileDownloadPathEdge);
                    edgeoptions.AddUserProfilePreference("intl.accept_languages", "nl");
                    edgeoptions.AddUserProfilePreference("disable-popup-blocking", "true");
                    //edgeoptions.AddArguments("--headless");
                    DriverContext.Driver = new Microsoft.Edge.SeleniumTools.EdgeDriver(edgeDriverService, edgeoptions);
                    DriverContext.Browser = new Browser(DriverContext.Driver);
                    break;

                case BrowserType.FireFox:
                    var firefoxoption = new FirefoxOptions();
                    var FileDownloadPathFirefox = fileDownloadPath;
                    firefoxoption.SetPreference("download.default_directory", FileDownloadPathFirefox);
                    firefoxoption.SetPreference("intl.accept_languages", "nl");
                    firefoxoption.SetPreference("disable-popup-blocking", "true");
                    //firefoxoption.AddArguments("--headless");
                    DriverContext.Driver = new FirefoxDriver(firefoxoption);
                    DriverContext.Browser = new Browser(DriverContext.Driver);
                    break;

                case BrowserType.Chrome:
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("no-sandbox");
                    var FileDownloadPath = fileDownloadPath;
                    chromeOptions.AddUserProfilePreference("download.default_directory", FileDownloadPath);
                    chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
                    chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                    //chromeOptions.AddArguments("headless");
                    DriverContext.Driver = new ChromeDriver(driverPath, chromeOptions, TimeSpan.FromMinutes(30));
                    DriverContext.Browser = new Browser(DriverContext.Driver);
                    break;
                   
            }

        }
        #endregion

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\TestData\\TestData.csv", "TestData#csv", DataAccessMethod.Sequential)]
        public void VerifyLoginTest()
        {
            try
            {
                var testdata = ObjectMother.TestData(TestContext);
                test = extent.StartTest(testdata.BrowserType + " Login test case");

                OpenBrowser(SetBrowser(testdata.BrowserType));
                test.Log(LogStatus.Pass, "Open Browser " + testdata.BrowserType, testdata.BrowserType + " Browser has been opened");
                DriverContext.Browser.GotoUrl(url);
                test.Log(LogStatus.Pass, "Navigate URL", "Navigated on this :" + DriverContext.Driver.Url);
                Thread.Sleep(5000);
                CurrentPage = GetInstance<LoginPage>();
                CurrentPage.As<LoginPage>().VerifyHomePageOpned();
                CurrentPage.As<LoginPage>().Login(testdata.UserName, testdata.Password);

                test.Log(LogStatus.Pass, "Login test case status", "Passed");
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                ScreenShot.CaptureScreenshot(DriverContext.Driver, "VerifyLoginTest");
                test.Log(LogStatus.Fail, ex.Message, test.AddBase64ScreenCapture(ScreenShot.imgFormat));
            }

        }

        [TestCleanup]
        public void TeardownTest()
        {
            try
            {
                DriverContext.Driver.Quit();
                extent.EndTest(test);
            }
            catch (System.Exception)
            {
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            extent.Flush();
            extent.Close();
        }


    }
}
