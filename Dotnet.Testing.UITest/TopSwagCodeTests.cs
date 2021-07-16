using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace Dotnet.Testing.UITest
{
    public class TopSwagCodeTests
    {
        FirefoxOptions options = new FirefoxOptions();
        IWebDriver webdriver;

        [OneTimeSetUp]
        public void Setup()
        {
            if (string.Equals("ci", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")))
            {
                webdriver  = new RemoteWebDriver(new Uri("http://localhost:4446/wd/hub"), options);
            }
            else
            {
                webdriver = new FirefoxDriver(options);
            }
        }

        [OneTimeTearDown]
        public void TearDown() 
        {
            webdriver.Close();
        }

        [Test]
        public void Test1()
        {
            var webdriver = GetWebDriver();
            string exp_title = "About | Top Swag Code";
            string exp_text = "Code enthusiast.";

            webdriver.Navigate().GoToUrl("https://topswagcode.com/about/");
            webdriver.Manage().Window.Maximize();
            Console.WriteLine("Started session");

            try
            {
                var text = webdriver.FindElement(By.XPath("//ul/li[1]")).Text;
                Assert.AreEqual(exp_text, text);

                string curr_window_title = webdriver.Title;
                Assert.AreEqual(exp_title, curr_window_title);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test2()
        {
            var webdriver = GetWebDriver();
            string exp_title = "About | Top Swag Code";
            string exp_text = "Code enthusiast.";

            webdriver.Navigate().GoToUrl("https://topswagcode.com/about/");
            webdriver.Manage().Window.Maximize();
            Console.WriteLine("Started session");

            try
            {
                var text = webdriver.FindElement(By.XPath("//ul/li[1]")).Text;
                Assert.AreEqual(exp_text, text);

                string curr_window_title = webdriver.Title;
                Assert.AreEqual(exp_title, curr_window_title);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test3()
        {
            var webdriver = GetWebDriver();
            string exp_title = "About | Top Swag Code";
            string exp_text = "Code enthusiast.";

            webdriver.Navigate().GoToUrl("https://topswagcode.com/about/");
            webdriver.Manage().Window.Maximize();
            Console.WriteLine("Started session");

            try
            {
                var text = webdriver.FindElement(By.XPath("//ul/li[1]")).Text;
                Assert.AreEqual(exp_text, text);

                string curr_window_title = webdriver.Title;
                Assert.AreEqual(exp_title, curr_window_title);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Test4()
        {
            var webdriver = GetWebDriver();
            string exp_title = "About | Top Swag Code";
            string exp_text = "Code enthusiast.";

            webdriver.Navigate().GoToUrl("https://topswagcode.com/about/");
            webdriver.Manage().Window.Maximize();
            Console.WriteLine("Started session");

            try
            {
                var text = webdriver.FindElement(By.XPath("//ul/li[1]")).Text;
                Assert.AreEqual(exp_text, text);

                string curr_window_title = webdriver.Title;
                Assert.AreEqual(exp_title, curr_window_title);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.Fail(e.Message);
            }
        }

        public IWebDriver GetWebDriver()
        {
            return webdriver;
        }
    }
}