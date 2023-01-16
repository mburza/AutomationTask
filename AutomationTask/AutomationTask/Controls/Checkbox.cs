using OpenQA.Selenium;

namespace AutomationTask.Controls
{
    public sealed class Checkbox : BaseControl
    {
        public Checkbox(IWebElement webElement) : base(webElement)
        {
        }

        private bool Checked()
        {
            return _webElement.Selected;
        }

        public void Check()
        {
            if (!Checked())
                _webElement.Click(); 
        }

        public void Uncheck()
        {
            if (Checked())
                _webElement.Click();
        }
    }
}
