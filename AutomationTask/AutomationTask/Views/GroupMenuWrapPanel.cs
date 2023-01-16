using AutomationTask.Controls;
using AutomationTask.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AutomationTask.Views
{
    public sealed class GroupMenuWrapPanel : BasePanel
    {
        public void ExpandSection(string sectionName)
        {
            if (sectionName == null)
            {
                throw new System.ArgumentNullException(nameof(sectionName));
            }

            var section = new LinkText(WebElement.FindElement(By.LinkText(sectionName)));
            section.HoverMouseOver();
        }

        private void WaitTillSectionIsExpanded(string subsectionName)
        {
            if (subsectionName == null)
            {
                throw new ArgumentNullException(nameof(subsectionName));
            }

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.LinkText(subsectionName)));
        }

        public T NavigateToSubsectionPage<T>(string subsectionName) where T : BasePage, new()
        {
            if (subsectionName == null)
            {
                throw new ArgumentNullException(nameof(subsectionName));
            }

            WaitTillSectionIsExpanded(subsectionName);

            var subsection = new LinkText(WebElement.FindElement(By.LinkText(subsectionName)));
            subsection.Click();
            WaitTillPageIsLoaded();

            return this.GetPage<T>();
        }



    }
}
