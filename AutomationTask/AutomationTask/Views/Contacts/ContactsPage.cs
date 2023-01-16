using AutomationTask.Controls;
using AutomationTask.Extensions;
using OpenQA.Selenium;

namespace AutomationTask.Views
{
    public sealed class ContactsPage : BasePage
    {
        private LinkText CreateContaxt => new LinkText(Driver.FindElement(By.LinkText("Create Contact")));
        public LinkText Contacts => new LinkText(Driver.FindElement(By.LinkText("Contacts")));

        public CreateContactPanel GetNewContactPanel()
        {
            CreateContaxt.Click();
            WaitTillPageIsLoaded();

            return this.GetPanel<CreateContactPanel>(Driver.FindElement(By.Id("DetailForm")));
        }

    }
}
