using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Repository;
using System;
using System.Web.UI;

namespace SoftEngWebEmployee
{
    public partial class SiteMaster : MasterPage
    {
        public Constants.EmployeeType EmployeeType { get; set; }        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserSession.SingleInstance.GetLoginStatus() == false)
            {
                Response.Redirect("~/Views/Login.aspx",false);
                return;
            }
            if (UserSession.SingleInstance.IsAdministrator())
                EmployeeType = Constants.EmployeeType.Administrator;
            else
                EmployeeType = Constants.EmployeeType.Employee;
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            UserSession.SingleInstance.RemoveLoggedinUser();                    
            Response.Redirect("~/Views/Login.aspx", false);
        }
    }
}