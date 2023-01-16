using System;
using OpenQA.Selenium;

namespace AutomationTask.Controls
{
    public class InputField : BaseControl
    {
        public InputField(IWebElement webElement) : base(webElement)
        {
        }

        public void SendKeys(string text)
        {
            if (text == null)
            {
                throw new System.ArgumentNullException(nameof(text));
            }

            _webElement.SendKeys(text);
        }

        public void Clear()
        {
            _webElement.Clear();
        }

        public string GetValue()
        {
            return _webElement.GetAttribute("value");

        }
    }
}
