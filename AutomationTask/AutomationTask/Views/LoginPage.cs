using AutomationTask.Controls;
using OpenQA.Selenium;
using System;
using System.Configuration;

namespace AutomationTask.Views
{
    public class LoginPage : BasePage
    {
        protected static string UserName { get; private set; }
        protected static string Password { get; private set; }

        public InputField userNameField => new InputField(Driver.FindElement(By.Id("login_user")));
        public InputField passwordField => new InputField(Driver.FindElement(By.Id("login_pass")));
        public Button LoginButton =>  new Button(Driver.FindElement(By.Id("login_button")));

        public void Login()
        {
            UserName = ConfigurationManager.AppSettings["userName"];
            Password = ConfigurationManager.AppSettings["password"];

            userNameField.SendKeys(UserName);
            passwordField.SendKeys(Password);

            LoginButton.Click();

            WaitTillPageIsLoaded();
        }


        public void LoadLoginPage()
        {
            Driver.Navigate().GoToUrl(Url);
        }
    }
}
