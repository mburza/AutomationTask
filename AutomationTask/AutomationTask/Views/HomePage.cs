using AutomationTask.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AutomationTask.Views
{
    public sealed class HomePage : BasePage
    {
        public GroupMenuWrapPanel GroupMenu => this.GetPanel<GroupMenuWrapPanel>(Driver.FindElement(By.ClassName("nav-wrap")));

        public void LoadHomePage()
        {
            Driver.Navigate().GoToUrl(Url);
            WaitTillAutoLogin();
        }

        private void WaitTillAutoLogin()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("//div[class='wait-icon dynamic']")));
        }
    }
}
