using OpenQA.Selenium;

namespace AutomationTask.Views
{
    public class BasePanel : BasePage
    {
        protected static IWebElement WebElement { get; private set; }

        public BasePanel InitialisePanel(IWebElement webElement)
        {
            WebElement = webElement ?? throw new System.ArgumentNullException(nameof(webElement));
            return this;
        }

        public BasePanel InitialiseWebElement(By searchBy)
        {
            if (searchBy == null)
            {
                throw new System.ArgumentNullException(nameof(searchBy));
            }

            WebElement = Driver.FindElement(searchBy);

            return this;
        }
    }
}
