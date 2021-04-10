using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Helpers
{
    public class UserSession
    {
        private static bool isLoggedIn;
        private static string userLoggedIn;
        private static bool isAdministrator;
        public static bool GetLoginStatus()
        {
            return isLoggedIn;
        }
        public static bool IsAdministrator()
        {
            return isAdministrator;
        }
        public static void SetAdministrator(bool isAdmin)
        {
            isAdministrator = isAdmin;
        }
        public static void SetLoginStatus(bool loginStatus)
        {
            isLoggedIn = loginStatus;
        }
        public static void SetLoginUser(string user)
        {
            userLoggedIn = user;
        }
        public static void RemoveLoggedinUser()
        {
            userLoggedIn = null;
        }
        public static string GetLoggedInUser()
        {
            return userLoggedIn;
        }
    }
}