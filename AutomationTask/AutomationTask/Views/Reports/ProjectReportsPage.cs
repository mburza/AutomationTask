using AutomationTask.Controls;
using AutomationTask.Controls.ListViewGrid;
using OpenQA.Selenium;

namespace AutomationTask.Views.Reports
{
    public sealed class ProjectReportsPage : BasePage
    {
        private Button Run => new Button(Driver.FindElement(By.CssSelector("button[name='FilterForm_applyButton']")));
        public ListView ResultsGrid => new ListView(Driver.FindElement(By.CssSelector("table[class='listView']")));

        public void RunReport()
        {
            Run.Click();
            WaitTillPageIsLoaded();
        }

    }
}
