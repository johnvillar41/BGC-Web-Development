using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                if (instance == null)
                {
                    instance = new UserSession();
                }
                return instance;
            }
        }
        public bool GetLoginStatus()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];
            if(cookie != null)
            {
                if (cookie == null)
                    return false;
                else
                    return true;
            }
            return false;            
        }
        public bool IsAdministrator()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];
            if (cookie != null)
            {
                if (cookie["employeeType"].Equals(Constants.EmployeeType.Administrator.ToString()))
                    return true;
                else
                    return false;
            }
            return false;
        }
        public void SetLoginUser(string user, Constants.EmployeeType employeeType)
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
                HttpContext.Current.Session.Abandon();
            }
        }
        public string GetLoggedInUser()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["UserInfo"];
            if (cookie != null)
                return cookie["username"];
            return null;
        }
        public int FetchTotalNumberOfNotificationsToday()
        {
            int totalCount = 0;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                connection.Open();
                string queryString = null;
                var isAdmin = UserSession.SingleInstance.IsAdministrator();
                if (isAdmin)
                    queryString = "SELECT COUNT(*) FROM notifications_table WHERE notif_date LIKE @date";
                else
                    queryString = "SELECT COUNT(*) FROM notifications_table WHERE notif_date LIKE @date AND user_name=@username";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@date", "%" + DateTime.Now.ToString("yyyy-MM-dd") + "%");
                if (!isAdmin)
                    command.Parameters.AddWithValue("@username", UserSession.SingleInstance.GetLoggedInUser());
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    totalCount = int.Parse(reader[0].ToString());
                }
            }
            return totalCount;
        }

    }
}