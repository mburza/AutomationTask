using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace AutomationTask.Controls
{
    public class BaseControl
    {
        protected readonly IWebElement _webElement;

        public BaseControl(IWebElement webElement)
        {
            _webElement = webElement ?? throw new System.ArgumentNullException(nameof(webElement));
        }

        public string Text => _webElement.Text;

        public void Click()
        {
            _webElement.Click();
        }

        public void HoverMouseOver()
        {
            Actions action = new Actions(GetWebDriver());
            action.MoveToElement(_webElement).Perform();
        }

        private IWebDriver GetWebDriver()
        {
            IWebDriver driver;

            if(_webElement.GetType().ToString() == "OpenQA.Selenium.Support.PageObjects.WebElementProxy")
            {
                driver = ((IWrapsDriver)_webElement.GetType().GetProperty("WrappedElement").GetValue(_webElement, null)).WrappedDriver;
            }
            else
            {
                driver = ((IWrapsDriver)_webElement).WrappedDriver;
            }
            return driver;
        }
    }
}
