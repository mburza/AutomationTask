using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using AutomationTask.Views;
using JetBrains.Annotations;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UBSAutomation.Utilities
{
    public static class UserManagement
    {
        private static readonly string LoginUserProfileName = "Login.data";

        public static void StoreLoginDataUserProfile()
        {
            var driver = new ChromeDriver(); //todo: check other browsers
            using (driver)
            {
                var loginPage = new LoginPage();
                loginPage.InitialiseDriver(driver);
                loginPage.LoadLoginPage();
                loginPage.Login();

                SaveUserProfile(driver.Manage().Cookies.AllCookies, LoginUserProfileName);
            }
        }

        private static void SaveUserProfile([NotNull] ReadOnlyCollection<Cookie> allCookies, [NotNull] string profileName)
        {
            if (allCookies == null) throw new ArgumentNullException(nameof(allCookies));
            if (profileName == null) throw new ArgumentNullException(nameof(profileName));

            using (var writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + profileName, false))
            {
                foreach (var cookie in allCookies)
                {
                    var cookieStr =
                        $"{cookie.Name};{cookie.Value};{cookie.Domain};{cookie.Path};{cookie.Expiry};{cookie.Secure};";
                    writer.Write(cookieStr);
                    writer.WriteLine();
                }
            }
        }

        public static bool DoesLoginUserProfileExist()
        {
            var directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            return directoryInfo.GetFiles(LoginUserProfileName).Length != 0;
        }

        [NotNull]
        [ItemNotNull]
        public static List<Cookie> LoadLoginDataUserProfile()
        {
            var profile =  LoadProfile(LoginUserProfileName);
            return profile;
        }

        [NotNull]
        [ItemNotNull]
        private static List<Cookie> LoadProfile([NotNull] string profileName)
        {
            if (profileName == null) throw new ArgumentNullException(nameof(profileName));
            var pattern = new Regex(@"(?<name>[\s\S]*);(?<value>[\s\S]*);(?<domain>[\s\S]*);(?<path>[\s\S]*);(?<expiry>[\s\S]*);(?<secure>[\s\S]*);", RegexOptions.Compiled);
            using (var reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + profileName))
            {
                string line;
                var cookies = new List<Cookie>();
                while ((line = reader.ReadLine()) != null)
                {
                    var match = pattern.Match(line);
                    var expiryStr = match.Groups["expiry"].ToString();
                    var expiry = string.IsNullOrEmpty(expiryStr) ? DateTime.MaxValue : DateTime.Parse(expiryStr);

                    var cookie = new Cookie(
                        match.Groups["name"].ToString(),
                        match.Groups["value"].ToString(),
                        match.Groups["domain"].ToString(),
                        match.Groups["path"].ToString(),
                        expiry);
                    cookies.Add(cookie);
                }
                return cookies;
            }
        }
    }
}
