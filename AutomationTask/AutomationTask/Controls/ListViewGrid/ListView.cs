using OpenQA.Selenium;
using System.Linq;

namespace AutomationTask.Controls.ListViewGrid
{
    public sealed class ListView : BaseControl
    {
        public ListView(IWebElement webElement) : base(webElement)
        {
        }
        public bool IsGridEmpty() => GetAllVisibleRows.Length == null;

        public ListViewRow[] GetAllVisibleRows => _webElement.FindElements(By.TagName("tr")).Select(x => new ListViewRow(x)).ToArray();
    }
}
