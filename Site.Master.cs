using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Repository;
using System;
using System.Web.UI;

namespace SoftEngWebEmployee
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (UserSession.SingleInstance.GetLoginStatus() == false)
            {
                Response.Redirect("~/Views/Login.aspx");
                return;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserSession.SingleInstance.GetLoginStatus() == false)
            {
                Response.Redirect("~/Views/Login.aspx");
                return;
            }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            UserSession.SingleInstance.RemoveLoggedinUser();                    
            Response.Redirect("~/Views/Login.aspx", false);
        }
    }
}