Automation task done by Marek Burzyński.



QA Automation Engineer test task
Please, create automated tests for portal https://demo.1crmcloud.com according to the requirements
and cases below.
Requirements (must have):
 Use C# + SpecFlow (https://docs.specflow.org/projects/getting-started/en/latest/index.html)
+ Selenium WebDriver
 Please, create a small framework for this task. What we expect to have:
o Use page object pattern
o Configuration via files
o Effective reuse of the code
 Follow coding best practices
Optional:
 Connect any HTML reporter
 Login using API (via hook)
Test cases
Scenario 1 – Create contact:
1. Login
2. Navigate to “Sales & Marketing” -> “Contacts”
3. Create new contact (Enter at least first name, last name, role and 2 categories: Customers and
Suppliers)
4. Open created contact and check that its data matches entered on the previous step
Scenario 2 – Run report:
1. Login
2. Navigate to “Reports & Settings” -> “Reports”
3. Find “Project Profitability” report
4. Run report and verify that some results were returned
Scenario 3 – Remove events from activity log:
1. Login
2. Navigate to “Reports & Settings” -> “Activity log”
3. Select first 3 items in the table
4. Click “Actions” -> “Delete”
5. Verify that items were deleted