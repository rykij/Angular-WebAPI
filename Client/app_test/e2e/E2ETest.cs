using System;
using System.Configuration;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using Protractor;

namespace ScenarioNavigator_WebPorting
{
    [TestFixture]
    public class E2ETest
    {
        IWebDriver driver; //Selenium driver
        NgWebDriver ngDriver; //Protractor driver

        static string currProjPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName;
        string driversPath = currProjPath + @"\app_test\e2e\config";

        string url = ConfigurationManager.AppSettings["Url"];
        
        [SetUp]
        public void SetUp()
        {
            /*All webdrivers files downloaded in NuGet are located in : 
             * D:\Projects\vs2012\WebApplication_PureMVCAngular_TDD\packages*/

            /* OPTION 1: Instantiate a driver */
            /* ########################################################### */

            /* Using NuGet Package 'PhantomJS' */
            //driver = new PhantomJSDriver(driversPath);
           
            /* Using NuGet Package 'WebDriver.ChromeDriver.win32' */
            //driver = new ChromeDriver(driversPath);

            /* Native for Selenium*/
            //driver = new FirefoxDriver();

            /* Using NuGet Package 'WebDriver.IEDriverServerDriver.win32' */
            //driver = new InternetExplorerDriver(driversPath); 

            //driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(10));
           
            /* OPTION 2: Instantiate a driver with more capabilities */
            /* ########################################################### */

            DesiredCapabilities caps = new DesiredCapabilities();
            caps.IsJavaScriptEnabled = true;
            PlatformType platform = caps.Platform.PlatformType;

            PhantomJSDriverService driverServices = PhantomJSDriverService.CreateDefaultService(driversPath, "phantomjs.exe");
            //driverServices.LogFile = "app_test/e2e/config/phantomLog.txt";
            driverServices.LogFile = driversPath + @"\phantomJs.log";
            caps.SetCapability("Phantomjs executable path", driverServices);

            driverServices.IgnoreSslErrors = true;
            driverServices.WebSecurity = false; 
            driverServices.LocalToRemoteUrlAccess = true;
            driverServices.DiskCache = true;

            driver = new PhantomJSDriver(driverServices);
            driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(100));
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 100));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
        }

        [Test]
        [Category("e2e")]
        public void e2e_SeleniumWebDriver_PageTitle_OpenPage_TitleIsTheSame()
        {
            driver.Navigate().GoToUrl(url + "/#/home");

            driver.FindElement(By.Id("tab_001")).Click();

            Assert.AreEqual("Angular Test", driver.Title);
        }

        [Test]
        [Category("e2e")]
        public void e2e_Home_ProtractorWebDriver_PageTitle_OpenPage_TitleIsTheSame()
        {
            ngDriver = new NgWebDriver(driver);
            ngDriver.Navigate().GoToUrl(url + "/#/");

            Assert.AreEqual("Angular Test", ngDriver.WrappedDriver.Title);
        }

        [Test]
        [Category("e2e")]
        public void e2e_Home_SeleniumWebDriver_InsertNewTitleName_LabelIsTheSame()
        {
            driver.Navigate().GoToUrl(url + "/#/home");

            driver.FindElement(By.Id("tab_001")).Click();

            IWebElement query = driver.FindElement(By.Id("titlebox"));
            query.Clear();
            query.SendKeys("Prova");

            query = driver.FindElement(By.Id("title"));
            string result = query.Text;

            Assert.AreEqual("This app is titled: Prova", result);
        }
 
        [Test]
        [Category("e2e")]
        /* ProtractorWebDriver give problems when used with PhantomJSDriver, FireFox
         a workaround is been implemented (see below)*/
        //N.B: IMPORTANT: Must use 'WrapperDriver' with Protractor Driver otherwise returned value is empty
        public void e2e_Home_ProtractorWebDriver_InsertNewTitleName_LabelIsTheSame()
        {
            ngDriver = new NgWebDriver(driver);
            ngDriver.Navigate().GoToUrl(url + "/#/home");

            ngDriver.WrappedDriver.FindElement(By.Id("tab_001")).Click();

            //workaround
            ngDriver.IgnoreSynchronization = true;
            Thread.Sleep(3000);

            ngDriver.WrappedDriver.FindElement(NgBy.Model("main.header.title")).Clear();
            ngDriver.WrappedDriver.FindElement(NgBy.Model("main.header.title")).SendKeys("Prova");

            var result = ngDriver.WrappedDriver.FindElement(By.Id("title")).Text;

            //To install FluentAssertion library: Install-Package FluentAssertions
            //ex. latestResult.Should().Be("This app is titled: Prova");
            Assert.AreEqual("This app is titled: Prova", result);
        }

        #region All test are executed with Protractor

        [Test]
        [Category("e2e")]
        public void e2e_Login_FillForm_DomAndModelHaveSameValue()
        {
            ngDriver = new NgWebDriver(driver);
            ngDriver.Navigate().GoToUrl(url + "/#/home");

            ngDriver.WrappedDriver.FindElement(By.Id("tab_002")).Click();

            //workaround
            ngDriver.IgnoreSynchronization = true;
            Thread.Sleep(3000);

            ngDriver.WrappedDriver.FindElement(By.Id("user")).Clear();
            ngDriver.WrappedDriver.FindElement(By.Id("user")).SendKeys("Flynn");

            ngDriver.WrappedDriver.FindElement(By.Id("pwd")).Clear();
            ngDriver.WrappedDriver.FindElement(By.Id("pwd")).SendKeys("abc123");

            ngDriver.WrappedDriver.FindElement(By.Id("confirmpwd")).Clear();
            ngDriver.WrappedDriver.FindElement(By.Id("confirmpwd")).SendKeys("abc123");
            
            var name = ngDriver.WrappedDriver.FindElement(NgBy.Model("form.login.UserName")).GetAttribute("value");
            var password = ngDriver.WrappedDriver.FindElement(NgBy.Model("form.login.Password")).GetAttribute("value");
            var confirmpwd = ngDriver.WrappedDriver.FindElement(NgBy.Model("form.login.ConfirmPassword")).GetAttribute("value");

            Assert.AreEqual("Flynn", name);
            Assert.AreEqual("abc123", password);
            Assert.AreEqual("abc123", confirmpwd);
        }

        [Test]
        [Category("e2e")]
        public void e2e_Login_CheckRememberMe_IsChecked()
        {
            ngDriver = new NgWebDriver(driver);
            ngDriver.Navigate().GoToUrl(url + "/#/home");

            ngDriver.WrappedDriver.FindElement(By.Id("tab_002")).Click();

            //workaround
            ngDriver.IgnoreSynchronization = true;
            Thread.Sleep(3000);

            ngDriver.WrappedDriver.FindElement(NgBy.Model("form.login.rememberMe")).Click();

            var result = ngDriver.WrappedDriver.FindElement(NgBy.Model("form.login.rememberMe")).Selected;

            Assert.AreEqual(true, result);

            ngDriver.WrappedDriver.FindElement(NgBy.Model("form.login.rememberMe")).Click();

            result = ngDriver.WrappedDriver.FindElement(NgBy.Model("form.login.rememberMe")).Selected;

            Assert.AreEqual(false, result);
        }

        [Test]
        [Category("e2e")]
        public void e2e_Job_PressJobSubmitButton()
        {
            ngDriver = new NgWebDriver(driver);
            ngDriver.Navigate().GoToUrl(url + "/#/home");

            ngDriver.WrappedDriver.FindElement(By.Id("tab_003")).Click();

            //workaround
            ngDriver.IgnoreSynchronization = true;
            Thread.Sleep(3000);

            ngDriver.WrappedDriver.FindElement(By.Id("runjob_tab")).Click();

            ngDriver.WrappedDriver.FindElement(By.Id("jobsubmit")).Click();

            //awaiting response
            ngDriver.IgnoreSynchronization = true;
            Thread.Sleep(10000);

            var result = ngDriver.WrappedDriver.FindElement(By.Id("jobstatustable")).Displayed;

            //awaiting response
            ngDriver.IgnoreSynchronization = true;
            Thread.Sleep(5000);

            Assert.AreEqual(true, result);

            var repeaterElements = ngDriver.WrappedDriver.FindElements(NgBy.Repeater("key in manager.notSortedJson(manager.job)"));

            Assert.IsTrue(repeaterElements.Count > 0);
        }

        #endregion
    }
}