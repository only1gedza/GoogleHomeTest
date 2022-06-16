using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.ObjectModel;

namespace ChromeTest
{
    [TestClass]
    public class Homepage
    {
        IWebDriver _driver;
        
        [TestMethod]
        public void ShouldSearchForAnItem()
        {
            var outPutDirectory =
                 Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _driver = new ChromeDriver(outPutDirectory);
            _driver.Navigate().GoToUrl("https://www.google.com");

            /*var searchLocator = _driver.FindElement(By.XPath("//input[@class='gLFyf gsfi']"));*/

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var searchLocator = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@class='gLFyf gsfi']")));

            searchLocator.SendKeys("HCA");
            searchLocator.Click();
            searchLocator.SendKeys(Keys.Enter);

            /*Verify returns results divs with class yuRUbf*/
            var resultLinks = _driver.FindElements(By.ClassName("yuRUbf"));
            Assert.IsTrue(resultLinks.Count >= 5);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _driver.Quit();
        }

    }
}