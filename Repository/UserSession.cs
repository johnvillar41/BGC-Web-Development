using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Repository
{
    public class UserSession
    {
        private static bool isLoggedIn;

        public static bool GetLoginStatus()
        {
            return isLoggedIn;
        }

        public static void SetLoginStatus(bool loginStatus)
        {
            isLoggedIn = loginStatus;
        }
    }
}