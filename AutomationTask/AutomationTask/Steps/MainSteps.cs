using AutomationTask.Utilities;
using AutomationTask.Views;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using UBSAutomation.Utilities;

namespace AutomationTask.Steps
{
    [Binding]
    public class MainSteps
    {
        protected readonly ScenarioContext MainScenarioContext;
        protected readonly FeatureContext MainFeatureContext;

        private static IWebDriver _webDriver;
        public static HomePage HomePage;

        public MainSteps(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            MainFeatureContext = featureContext;
            MainScenarioContext = scenarioContext;
        }

        [BeforeFeature]
        public static void Init()
        {
            //if (!UserManagement.DoesLoginUserProfileExist())
            UserManagement.StoreLoginDataUserProfile();

            LoadDriver();

            var loginPage = new LoginPage();
            loginPage.InitialiseDriver(_webDriver);
            loginPage.LoadLoginPage();
            LoadCookies();

            HomePage = new HomePage();
            HomePage.InitialiseDriver(_webDriver);
        }

        [AfterFeature]
        public static void Dispose()
        {
            _webDriver?.Quit();
        }

        [AfterStep]
        public void Check()
        {
            var exception = MainScenarioContext.TestError;

            if (exception == null)
                return;

            if(exception is ElementNotVisibleException)
            {
                Assert.Ignore(exception.Message);
            }
        }

        private static void LoadDriver()
        {
            _webDriver = new ChromeDriver();
        }

        private static void LoadCookies()
        {
            var user = new User().WithLoginUserData();
            _webDriver.Manage().Cookies.DeleteAllCookies();
            var cookies = _webDriver.Manage().Cookies;
            foreach (var cookie in user.Cookies)
            {
                cookies.AddCookie(cookie);
            }
        }
    }
}
