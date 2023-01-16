using System;
using System.Linq;
using OpenQA.Selenium;

namespace AutomationTask.Controls
{
    public class CustomDropdown : BaseControl
    {
        private IWebDriver _driver;
        private string _selectPanelSelectorText;
        public CustomDropdown(IWebElement webElement) : base(webElement)
        {
        }

        public CustomDropdown InitialiseDriverAndSelector(IWebDriver Driver, string selectorText)
        {
            _driver = Driver;
            _selectPanelSelectorText = selectorText;

            return this;
        }

        private DropdownItem[] AllItems => selectPanel.FindElements(By.CssSelector("div[class='menu-option single']")).Select(x => new DropdownItem(x)).ToArray();
        private IWebElement selectPanel => _driver.FindElement(By.CssSelector(_selectPanelSelectorText));

        private void Expand()
        {
            _webElement.Click();
        }

        public void SelectItem(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            Expand();
            var itemToSelect = AllItems.SingleOrDefault(x => x.Text == text);
            if (itemToSelect == null)
                throw new ElementNotVisibleException($"Item {text} is not valid element to choose");

            itemToSelect.HoverMouseOver();
            itemToSelect.Click();
        }
    }
}
