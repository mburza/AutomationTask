using AutomationTask.Controls;
using AutomationTask.Extensions;
using OpenQA.Selenium;

namespace AutomationTask.Views
{
    public sealed class CreateContactPanel : BasePanel
    {
        public InputField FirstName => new InputField(WebElement.FindElement(By.Id("DetailFormfirst_name-input")));
        public InputField LastName => new InputField(WebElement.FindElement(By.Id("DetailFormlast_name-input")));
        private Button SaveContact => new Button(WebElement.FindElement(By.Id("DetailForm_save")));
        public CustomDropdown Category => new CustomDropdown(WebElement.FindElement(By.Id("DetailFormcategories-input"))).InitialiseDriverAndSelector(Driver, "div[id = 'DetailFormcategories-input-search-list']");
        public CustomDropdown BusinessRole => new CustomDropdown(WebElement.FindElement(By.Id("DetailFormbusiness_role-input"))).InitialiseDriverAndSelector(Driver, "div[id = 'DetailFormbusiness_role-input-popup']");

        public void FillField(InputField fieldToFill, string text)
        {
            if (fieldToFill == null)
            {
                throw new System.ArgumentNullException(nameof(fieldToFill));
            }

            if (text == null)
            {
                throw new System.ArgumentNullException(nameof(text));
            }

            fieldToFill.SendKeys(text);
        }

        public NewContactPanel SaveNewContact()
        {
            SaveContact.Click();
            WaitTillPageIsLoaded();

            return this.GetPanel<NewContactPanel>(Driver.FindElement(By.Id("DetailForm")));
        }

    }
}
