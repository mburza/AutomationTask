using OpenQA.Selenium;

namespace AutomationTask.Controls.ListViewGrid
{
    public sealed class ListViewCell : BaseControl
    {
        public ListViewCell(IWebElement webElement) : base(webElement)
        {
        }

        public Checkbox Checkbox => new Checkbox(_webElement.FindElement(By.CssSelector("input[class='checkbox']")));
    }
}
