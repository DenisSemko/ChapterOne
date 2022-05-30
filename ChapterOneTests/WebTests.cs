using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ChapterOneTests
{
    [TestClass]
    public class WebTests
    {
        private static IWebDriver _webDriver;
        private static string _url;
        private static TestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _webDriver = new ChromeDriver();
            _url = "http://localhost:4200";
            _testContext = context;
        }
        [TestMethod]
        public void LoginUser_InsertData_ReturnsAccessMessage()
        {
            _webDriver.Navigate().GoToUrl(_url);
            _webDriver.Manage().Window.Maximize();

            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            js.ExecuteScript("document.body.style.zoom='90%'");

            _webDriver.Navigate().GoToUrl(_url + "/login");

            _webDriver.FindElement(By.Id("username")).SendKeys("test");
            _webDriver.FindElement(By.Id("password")).SendKeys("test12!A");

            _webDriver.FindElement(By.XPath("//button//span[text()='SEND']")).Click();

            Thread.Sleep(4000);

            Assert.Equals(_webDriver.Url, _url + "/reader/dashboard");
        }
    }
}
