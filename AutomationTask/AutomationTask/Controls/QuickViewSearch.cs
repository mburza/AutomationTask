using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;

namespace AutomationTask.Controls
{
    public sealed class QuickViewSearch : BaseControl
    {
        private IWebDriver _driver;
        private string _selectPanelSelectorText;

        public QuickViewSearch(IWebElement webElement) : base(webElement)
        {
        }

        public QuickViewSearch InitialiseDriverAndSelector(IWebDriver Driver, string selectorText)
        {
            _driver = Driver;
            _selectPanelSelectorText = selectorText;

            return this;
        }

        private InputField SearchField => new InputField(_webElement);

        private IWebElement QuickViewPanel => _driver.FindElement(By.CssSelector(_selectPanelSelectorText));
        private IWebElement[] AllItems => QuickViewPanel.FindElements(By.CssSelector("div[class='menu-option single']")).ToArray();

        public void SearchAndSelect(string searchPhrase)
        {
            SearchField.Clear();
            SearchField.SendKeys(searchPhrase);
            WaitTillResultsShown();

            var searchItem = AllItems.FirstOrDefault(x => x.Text == searchPhrase);
            if(searchItem == null)
                throw new ElementNotVisibleException($"Item {searchPhrase} is not valid element to choose");

            searchItem.Click();
        }

        private void WaitTillResultsShown()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div[class='menu-option single']")));
        }

    }
}
