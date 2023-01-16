using System;
using System.Linq;
using AutomationTask.Controls;
using AutomationTask.Controls.ListViewGrid;
using AutomationTask.Extensions;
using OpenQA.Selenium;
using Polly;

namespace AutomationTask.Views.Reports
{
    public sealed class BrowseReportsPage : BasePage
    {
        private IWebElement TriggerGridReloadElement => Driver.FindElement(By.XPath("//*[text()='Browse All']"));
        private InputField Search => new InputField(Driver.FindElement(By.CssSelector("input[class='input-text input-entry ']")));
        public ListView Grid => new ListView(Driver.FindElement(By.CssSelector("table[class='listView']")));
        private const int NameColumnIndex = 2;
        private const int FirstDataRowIndex = 1;

        public ProjectReportsPage GoToReport(string reportName)
        {
            if (reportName == null)
            {
                throw new ArgumentNullException(nameof(reportName));
            }

            SearchForReport(reportName);

            if (Grid.IsGridEmpty())
                throw new ElementNotVisibleException($"{reportName} was not found, grid with results is empty");

            var report = Grid.GetAllVisibleRows[FirstDataRowIndex].GetAllCells.SingleOrDefault(x => x.Text == reportName);
            if (report == null)
                throw new ElementNotVisibleException($"{reportName} was not found");

            report.Click();
            WaitTillPageIsLoaded();

            return this.GetPage<ProjectReportsPage>();
        }

        private void SearchForReport(string reporttName)
        {
            if (reporttName == null)
            {
                throw new ArgumentNullException(nameof(reporttName));
            }

            ClearSearch();
            var rowsCount = Grid.GetAllVisibleRows.Length;

            Search.SendKeys(reporttName);
            TriggerGridReloadElement.Click();

            WaitTillGridIsReloaded(rowsCount);
        }

        private void ClearSearch()
        {
            var rowsCount = Grid.GetAllVisibleRows.Length;
            if(Search.GetValue() != string.Empty)
            {
                Search.Clear();
                WaitTillGridIsReloaded(rowsCount);
            }
        }

        private void WaitTillGridIsReloaded(int previousRowsCount)
        {
            var result = Policy
                .HandleResult(false)
                .WaitAndRetry(10, i => TimeSpan.FromMilliseconds(500))
                .ExecuteAndCapture(() => Grid.GetAllVisibleRows.Length != previousRowsCount);

            if (result.Outcome == OutcomeType.Failure)
                throw new TimeoutException("Grid is not reloaded after search");


        }
    }
}
