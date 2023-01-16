using AutomationTask.Controls;
using OpenQA.Selenium;

namespace AutomationTask.Views
{
    public sealed class NewContactPanel : BasePanel
    {
        public Label ContactName => new Label(WebElement.FindElement(By.Id("_form_header")));
        public Label Category => new Label(WebElement.FindElement(By.XPath("//p[contains(@class, 'form-label') and text()='Category']/ancestor::li")));
        public Label BusinessRole => new Label(WebElement.FindElement(By.XPath("//p[contains(@class, 'form-label')  and text()='Business Role']/following-sibling::div")));

        private Button Delete => new Button(WebElement.FindElement(By.Id("DetailForm_delete2")));

        public void DeleteContact()
        {
            Delete.Click();
            Driver.SwitchTo().Alert().Accept();
            WaitTillPageIsLoaded();
        }
    }
}
