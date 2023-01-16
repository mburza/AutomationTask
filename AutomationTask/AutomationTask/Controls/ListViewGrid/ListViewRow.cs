using OpenQA.Selenium;
using System.Linq;

namespace AutomationTask.Controls.ListViewGrid
{
    public sealed class ListViewRow : BaseControl
    {
        public ListViewRow(IWebElement webElement) : base(webElement)
        {
        }

        public ListViewCell[] GetAllCells => _webElement.FindElements(By.TagName("td")).Select(x => new ListViewCell(x)).ToArray();

    }
}
