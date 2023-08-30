using AutoFrameworkTest.BaseObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoFrameworkTest.Pages
{
    class LoginPage : BasePage
    {

        [FindsBy(How = How.Id, Using = "user-name")]
        IWebElement Username { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        IWebElement Password { get; set; }

        [FindsBy(How = How.XPath, Using = ".//input[@type='submit']")]
        IWebElement LoginButton { get; set; }

        [FindsBy(How = How.XPath, Using = ".//button[contains(text(),'Open Menu')]")]
        IWebElement Navbar { get; set; }

        [FindsBy(How = How.Id, Using = "logout_sidebar_link")]
        IWebElement LogoutButton { get; set; }

        public void VerifyHomePageOpned()
        {
            Thread.Sleep(5000);
            WaitForElement(DriverContext.Driver, Username, 20);
            Assert.IsTrue(Username.Displayed);
            test.Log(LogStatus.Pass, "Verify Home Page is open", "Home Page is opened verified successfully");
        }

        public void Login(String uname, String pass)
        {
            WaitForElement(DriverContext.Driver, Username, 20);
            Username.SendKeys(uname);
            Password.SendKeys(pass);
            Thread.Sleep(2000);
            LoginButton.Click();
            Thread.Sleep(5000);
            test.Log(LogStatus.Pass, "Login", "User has Logged in with user: " + uname);
        }

        public void VerifyLoginConfirm()
        {
            WaitForElement(DriverContext.Driver, Navbar, 20);
            Navbar.Click();
            Thread.Sleep(2000);
            Assert.IsTrue(LogoutButton.Displayed);
            test.Log(LogStatus.Pass, "Verify User is logged in", "User is logged in verified ");
        }
    }
}
