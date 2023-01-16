using AutomationTask.ContextInjection;
using AutomationTask.Controls.ListViewGrid;
using AutomationTask.Views;
using AutomationTask.Views.Reports;
using NUnit.Framework;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTask.Steps
{
    [Binding]
    public sealed class AutomationTask_Steps : MainSteps
    {
        private readonly AutomationContextInjection _contextInjection;

        public AutomationTask_Steps(ScenarioContext scenarioContext, FeatureContext featureContext, AutomationContextInjection contextInjection) : base(scenarioContext, featureContext)
        {
            _contextInjection = contextInjection;
        }

        [Given(@"I am on the HomePage")]
        public void GivenIAmOnTheHomePage()
        {
            HomePage.LoadHomePage();
        }

        [Given(@"I have expanded '(.*)' section")]
        public void GivenIHaveExpandedSection(string sectionName)
        {
            if (sectionName == null)
            {
                throw new ArgumentNullException(nameof(sectionName));
            }

            HomePage.GroupMenu.ExpandSection(sectionName);
        }

        [Given(@"I have navigated to '(.*)' page")]
        public void GivenIHaveNavigatedToPage(string subSectionName)
        {
            if (subSectionName == null)
            {
                throw new System.ArgumentNullException(nameof(subSectionName));
            }

            switch (subSectionName)
            {
                case "Contacts":
                    _contextInjection.ContactsPage = HomePage.GroupMenu.NavigateToSubsectionPage<ContactsPage>(subSectionName);
                    break;
                case "Activity Log":
                    _contextInjection.ActivityLogPage = HomePage.GroupMenu.NavigateToSubsectionPage<ActivityLogPage>(subSectionName);
                    break;
                case "Reports":
                    _contextInjection.BrowseReportsPage = HomePage.GroupMenu.NavigateToSubsectionPage<BrowseReportsPage>(subSectionName);
                    break;
                default:
                    throw new NotSupportedException($"{subSectionName} is not valid option to choose");
            }
        }

        [When(@"I create new contact adding '(.*)' firstname, '(.*)' lastname, '(.*)' role, '(.*)' and '(.*)' categories")]
        public void WhenICreateNewContactAddingFirstnameLastnameRoleAndCategories(string firstName, string lastName, string role, string firstCategory, string secondCategory)
        {
            var createContactForm = _contextInjection.ContactsPage.GetNewContactPanel();

            createContactForm.Category.SelectItem(firstCategory);
            createContactForm.BusinessRole.SelectItem(role);
            createContactForm.FillField(createContactForm.FirstName, firstName);
            createContactForm.FillField(createContactForm.LastName, lastName);
            createContactForm.Category.SelectItem(secondCategory);

            _contextInjection.NewContactPanel = createContactForm.SaveNewContact();

            _contextInjection.Role = role;
            _contextInjection.FirstName = firstName;
            _contextInjection.LastName = lastName;
            _contextInjection.FirstCategory = firstCategory;
            _contextInjection.SecondCategory = secondCategory;

        }

        [Then(@"Contact is created")]
        public void ThenContactIsCreated()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(_contextInjection.Role, _contextInjection.NewContactPanel.BusinessRole.Text, "Business Role is not corect");
                Assert.That(_contextInjection.NewContactPanel.Category.Text.Contains(_contextInjection.FirstCategory), $"{_contextInjection.FirstCategory} category is missing");
                Assert.That(_contextInjection.NewContactPanel.Category.Text.Contains(_contextInjection.SecondCategory), $"{_contextInjection.SecondCategory} category is missing");
                Assert.That(_contextInjection.NewContactPanel.ContactName.Text.Contains(_contextInjection.FirstName), $"{_contextInjection.FirstName} first name is missing");
                Assert.That(_contextInjection.NewContactPanel.ContactName.Text.Contains(_contextInjection.LastName), $"{_contextInjection.LastName} last name is missing");
            });
        }

        [When(@"I mark first (.*) rows")]
        public void WhenIMarkFirstRows(int rowsToCheckNumber)
        {
            var headerRowNumberToIgnore = 1;
            var rowsToDelete = _contextInjection.ActivityLogPage.Grid.GetAllVisibleRows.ToList().Skip(headerRowNumberToIgnore).Take(rowsToCheckNumber).ToArray();

            _contextInjection.RecentActivityTextOfSeletedRows = rowsToDelete.Select(x => x.GetAllCells[1].Text).ToArray();

            _contextInjection.ActivityLogPage.DeleteRows(rowsToDelete);
        }

        [Then(@"First (.*) rows are no longer present")]
        public void ThenFirstRowsAreNoLongerPresent(int rowsToCheckNumber)
        {
            var headerRowNumberToIgnore = 1;
            var currentRows = _contextInjection.ActivityLogPage.Grid.GetAllVisibleRows.ToList().Skip(headerRowNumberToIgnore).Take(rowsToCheckNumber).ToArray();

            var currentfirstRowsRecentActivityText = currentRows.Select(x => x.GetAllCells[1].Text).ToArray();

            CollectionAssert.AreNotEqual(_contextInjection.RecentActivityTextOfSeletedRows, currentfirstRowsRecentActivityText, "Rows were not deleted");
        }

        [When(@"I go to '(.*)' report")]
        public void WhenIGoToReport(string reportName)
        {
            if (reportName == null)
            {
                throw new ArgumentNullException(nameof(reportName));
            }

            _contextInjection.ProjectReportsPage = _contextInjection.BrowseReportsPage.GoToReport(reportName);
        }

        [When(@"I run report")]
        public void WhenIRunReport()
        {
            _contextInjection.ProjectReportsPage.RunReport();
        }

        [Then(@"Results are shown")]
        public void ThenResultsAreShown()
        {
            Assert.False(_contextInjection.ProjectReportsPage.ResultsGrid.IsGridEmpty(), "Results were not shown");
        }

        [AfterScenario]
        public void CleanUp()
        {
            if(_contextInjection.NewContactPanel != null)
            {
                _contextInjection.NewContactPanel.DeleteContact();
            }
        }

    }
}
