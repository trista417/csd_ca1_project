using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;

// NuGet install PhantomJS driver (or Chrome or Firefox...)

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace BPCalculatorAcceptanceTests
{
    [TestClass]
    public class SeleniumTestSuite
    {
        // .runsettings file contains test run parameters
        // e.g. URI for app
        // test context for this run

        private TestContext testContextInstance;

        // test harness uses this property to initliase test context
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        // URI for web app being tested
        private String webAppUri;

        // .runsettings property overriden in vsts test runner
        // release task
        [TestInitialize]                // run before each unit test
        public void Setup()
        {
            //  this.webAppUri = testContextInstance.Properties["webAppUri"].ToString();
            this.webAppUri = "https://bmicalculator-csdca1-staging.azurewebsites.net/bloodpressure";
        }

        // one unit test
        [TestMethod]
        public void TestSystolicRangeValidation()
        {
            // PhantomJSDriver will work in hosted agent
            // in VSTS (others won't)
            using (IWebDriver driver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver")))
            {
                driver.Navigate().GoToUrl(webAppUri);

                IWebElement systolicElement = driver.FindElement(By.Id("systolic-input"));

                systolicElement.SendKeys("500");

                IWebElement systolicErrorElement = driver.FindElement(By.Id("systolic-error"));
                String systolicError = systolicErrorElement.Text.ToString();

                StringAssert.Equals(systolicError, "Invalid Systolic Value");

                driver.Quit();
            }
        }

        [TestMethod]
        public void TestDiastolicRangeValidation()
        {
            using (IWebDriver driver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver")))
            {

                driver.Navigate().GoToUrl(webAppUri);

                IWebElement diastolicElement = driver.FindElement(By.Id("diastolic-input"));

                diastolicElement.SendKeys("500");

                IWebElement diastolicErrorElement = driver.FindElement(By.Id("diastolic-error"));

                String diastolicError = diastolicErrorElement.Text.ToString();

                StringAssert.Equals(diastolicError, "Invalid Diastolic Value");

                driver.Quit();
            }
        }

        [TestMethod]
        public void TestValidNormalBP()
        {
            using (IWebDriver driver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver")))
            {

                driver.Navigate().GoToUrl(webAppUri);
                
                IWebElement systolicElement = driver.FindElement(By.Id("systolic-input"));

                systolicElement.Clear();
                systolicElement = driver.FindElement(By.Id("systolic-input"));
                systolicElement.SendKeys("110");

                IWebElement diastolicElement = driver.FindElement(By.Id("diastolic-input"));
                diastolicElement.Clear();
                diastolicElement = driver.FindElement(By.Id("diastolic-input"));
                diastolicElement.SendKeys("70");

                driver.FindElement(By.Id("form1")).Submit();

                IWebElement bpElement = driver.FindElement(By.Id("bp-element"));

                String bpValue = bpElement.Text.ToString();

                StringAssert.Equals(bpValue, "Normal Blood Pressure");

                driver.Quit();
            }
        }
    }
}
