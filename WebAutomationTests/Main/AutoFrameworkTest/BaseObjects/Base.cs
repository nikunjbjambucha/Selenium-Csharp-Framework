using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoFrameworkTest.BaseObjects
{
    public class Base
    {
        private IWebDriver _driver { get; set; }
        public BasePage CurrentPage { get; set; }

        protected TPage GetInstance<TPage>() where TPage : BasePage, new()
        {
            TPage pageInstance = new TPage()
            {
                _driver = DriverContext.Driver


            };
            PageFactory.InitElements(DriverContext.Driver, pageInstance);

            return pageInstance;
        }
        public TPage As<TPage>() where TPage : BasePage
        {
            return (TPage)this;

        }
        public T AfterPageLoad<T>() where T : BasePage, new()
        {
            bool wait = true;
            int counter = 0;
            while (wait)
            {
                if (DriverContext.Driver.FindElements(By.TagName("body")).Count() > 0)
                    break;
                Thread.Sleep(50);
                wait = counter++ < 100;
            }
            return GetInstance<T>();
        }

        //================================================================================================================================
        //-----------------------------------------------------Extend Report Set up--------------------------------------------------------------
        //================================================================================================================================

        public static ExtentReports extent;
        public static ExtentTest test;
        public static string HTMLReportPath;
        public static string EnvironmentName = ConfigurationManager.AppSettings["Environment"];

        public static String datetime()
        {
            var time = DateTime.Now;
            string formattedTime = time.ToString("MM, dd, yyyy, hh, mm, ss");
            formattedTime = formattedTime.Replace(",", "_");
            Console.WriteLine(formattedTime);
            return formattedTime;
        }

        public static string GetParentDirectory()
        {
            System.IO.DirectoryInfo myDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            myDirectory = myDirectory.Parent;
            String parentDirectory = myDirectory.Parent.FullName;
            return parentDirectory;
        }

        public static void ExtentManager()
        {
            String currentdatetime = datetime();

            HTMLReportPath = GetParentDirectory() + "\\Reports\\AutomationTestReport" + "_" + currentdatetime + ".html";
            extent = new ExtentReports(HTMLReportPath, true, DisplayOrder.OldestFirst);

            extent.AddSystemInfo("Host Name", "Test Host").AddSystemInfo("Environment", EnvironmentName);
        }

    }
}
