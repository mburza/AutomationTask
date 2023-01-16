Feature: AutomationTask

Background:
	Given I am on the HomePage

Scenario: Create new contact
	Given I have expanded 'Sales & Marketing' section 
	And I have navigated to 'Contacts' page
	When I create new contact adding 'John' firstname, 'Wick' lastname, 'CEO' role, 'Suppliers' and 'Customers' categories
	Then Contact is created  

Scenario: Remove events from acitivity log
	Given I have expanded 'Reports & Settings' section
	And I have navigated to 'Activity Log' page
	When I mark first 3 rows
	Then First 3 rows are no longer present

Scenario: Run report
	Given I have expanded 'Reports & Settings' section
	And I have navigated to 'Reports' page
	When I go to 'Project Profitability' report
	And I run report
	Then Results are shown