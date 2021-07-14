using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace Dotnet.Testing.UITest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            FirefoxOptions options = new FirefoxOptions();
            RemoteWebDriver webdriver = new RemoteWebDriver(new Uri("http://localhost:4446/wd/hub"), options);
            //var webdriver = new FirefoxDriver();
            string exp_title = "About | Top Swag Code";
            string exp_text = "Code enthusiast.";

            webdriver.Navigate().GoToUrl("https://topswagcode.com/about/");
            webdriver.Manage().Window.Maximize();
            Console.WriteLine("Started session");

            try
            {
                var text = webdriver.FindElement(By.XPath("//ul/li[1]")).Text;
                Assert.AreEqual(exp_text, text);

                String curr_window_title = webdriver.Title;
                Assert.AreEqual(exp_title, curr_window_title);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.Fail(e.Message);
            }

            webdriver.Close();
        }
    }
}