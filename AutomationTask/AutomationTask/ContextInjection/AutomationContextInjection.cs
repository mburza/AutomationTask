using AutomationTask.Views;
using AutomationTask.Views.Reports;

namespace AutomationTask.ContextInjection
{
    public class AutomationContextInjection
    {
        public ContactsPage ContactsPage { get; internal set; }
        public NewContactPanel NewContactPanel { get; internal set; }
        public string Role { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string FirstCategory { get; internal set; }
        public string SecondCategory { get; internal set; }
        public ActivityLogPage ActivityLogPage { get; internal set; }
        public string[] RecentActivityTextOfSeletedRows { get; internal set; }
        public BrowseReportsPage BrowseReportsPage { get; internal set; }
        public ProjectReportsPage ProjectReportsPage { get; internal set; }
    }
}
