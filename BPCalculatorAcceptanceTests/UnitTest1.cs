using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;

// NuGet install PhantomJS driver (or Chrome or Firefox...)

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace BPCalculatorAcceptanceTests
{
    [TestClass]
    public class UnitTest1
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
            this.webAppUri = "http://localhost:40327/bloodpressure";
        }

        // one unit test
        [TestMethod]
        public void TestCelsiusToFahrenheit()
        {
            // PhantomJSDriver will work in hosted agent
            // in VSTS (others won't)
            using (IWebDriver driver = new ChromeDriver(@"C:\Users\etrismo\Documents\MSc DevOps\Continuous Delivery\csd_ca1_project\BPCalculatorAcceptanceTests\bin\Debug\netcoreapp2.1"))
            {
                // any exception below result in a test fail

                // navigate to URI for temperature converter
                // web app running on IIS express
                driver.Navigate().GoToUrl(webAppUri);

                // get celsius element
                IWebElement systolicElement = driver.FindElement(By.Id("systolic-input"));
                // IWebElement diastolicElement = driver.FindElement(By.Id("diastolic-input"));

                // enter 10 in element
                systolicElement.SendKeys("500");

                // submit the form
                // driver.FindElement(By.Id("convertform")).Submit();

                // explictly wait for result with "fahrenheit" item
                // IWebElement fahrenheitElement = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                //    .Until(ExpectedConditions.ElementExists((By.Id("fahrenheit"))));
                IWebElement systolicErrorElement = driver.FindElement(By.Id("systolic-error"));
                // item comes back like "Faherheit: 50"
                String systolicError = systolicErrorElement.Text.ToString();

                // 10 Celsius = 50 Fahrenheit, assert it
                StringAssert.Equals(systolicError, "Invalid Systolic Value");

                driver.Quit();
            }
        }
    }
}
