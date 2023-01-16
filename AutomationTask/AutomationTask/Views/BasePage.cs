using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Configuration;

namespace AutomationTask.Views
{
    public class BasePage
    {
        protected static IWebDriver Driver { get; private set; }
        protected static string Url { get; private set; }

        public BasePage()
        {
            Url = ConfigurationManager.AppSettings["url"];
        }

        public void InitialiseDriver(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        protected void WaitTillPageIsLoaded()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementExists(By.Id("ajaxStatusDiv")));

            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("ajaxStatusDiv")));
        }
    }
}