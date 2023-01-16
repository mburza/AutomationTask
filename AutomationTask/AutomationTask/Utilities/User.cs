using JetBrains.Annotations;
using OpenQA.Selenium;
using System.Collections.Generic;
using UBSAutomation.Utilities;

namespace AutomationTask.Utilities
{
    public class User
    {
        public User()
        {
            Cookies = new List<Cookie>();
        }

        [NotNull]
        [ItemNotNull]
        public List<Cookie> Cookies { get; }

        [NotNull]
        public User WithLoginUserData()
        {
            Cookies.AddRange(UserManagement.LoadLoginDataUserProfile());
            return this;
        }
    }
}
