using AutomationTask.Views;
using OpenQA.Selenium;

namespace AutomationTask.Extensions
{
    public static class PageExtensions
    {
        public static T GetPage<T>(this BasePage page) where T : BasePage, new()
        {
            return new T();
        }

        public static T GetPanel<T>(this BasePage page, IWebElement initialiseElement) where T : BasePanel, new()
        {
            if (initialiseElement == null)
            {
                throw new System.ArgumentNullException(nameof(initialiseElement));
            }

            var panel = new T();
            panel.InitialisePanel(initialiseElement);

            return panel;
        }
    }
}
