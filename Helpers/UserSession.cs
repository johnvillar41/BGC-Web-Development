using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SoftEngWebEmployee.Helpers
{
    public class UserSession
    {
        private static UserSession instance = null;
        private UserSession()
        {

        }
        public static UserSession SingleInstance
        {
            get
            {
                if(instance == null)
                {
                    instance = new UserSession();
                }
                return instance;
            }
        }       
        public bool GetLoginStatus()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];
            if (cookie == null)
                return false;
            else
                return true;            
        }
        public bool IsAdministrator()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];
            if (cookie["employeeType"].Equals(Constants.EmployeeType.Administrator.ToString()))
                return true;
            else
                return false;
        }               
        public void SetLoginUser(string user,Constants.EmployeeType employeeType)
        {
            HttpCookie cookie = new HttpCookie("UserInfo");
            cookie["username"] = user;
            cookie["employeeType"] = employeeType.ToString();
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public void RemoveLoggedinUser()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        public string GetLoggedInUser()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];
            return cookie["username"];
        }
    }
}