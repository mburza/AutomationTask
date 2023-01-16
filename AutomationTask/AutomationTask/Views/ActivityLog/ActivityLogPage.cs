using AutomationTask.Controls;
using AutomationTask.Controls.ListViewGrid;
using AutomationTask.Views.ActivityLog;
using OpenQA.Selenium;

namespace AutomationTask.Views
{
    public sealed class ActivityLogPage : BasePage
    {
        public const int CheckoboxColumnIndex = 0;
        public const int RecentActivityColumnIndex = 1;

        public ListView Grid => new ListView(Driver.FindElement(By.CssSelector("table[class='listView']")));

        private CustomDropdown Action => new CustomDropdown(Driver.FindElement(By.CssSelector("button[class='input-button menu-source']"))).InitialiseDriverAndSelector(Driver, "div[class='card-outer popup-default panel-outer panel-border panel-default panel-round-bottom active']");

        public void DeleteRows(ListViewRow[] rowsToDelete)
        {
            if (rowsToDelete == null)
            {
                throw new System.ArgumentNullException(nameof(rowsToDelete));
            }

            foreach (var row in rowsToDelete)
            {
                row.GetAllCells[CheckoboxColumnIndex].Checkbox.Check();
            }

            Action.SelectItem(ActionItems.Delete.ToString());
            Driver.SwitchTo().Alert().Accept();
            WaitTillPageIsLoaded();
        }
    }
}
