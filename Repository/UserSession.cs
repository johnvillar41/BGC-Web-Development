using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Repository
{
    public class UserSession
    {
        private static bool isLoggedIn;
        private static string userLoggedIn;
        public static bool GetLoginStatus()
        {
            return isLoggedIn;
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