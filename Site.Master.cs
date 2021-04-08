using SoftEngWebEmployee.Repository;
using System;
using System.Web.UI;

namespace SoftEngWebEmployee
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserSession.GetLoginStatus() == false)
            {
                Response.Redirect("Login", false);
            }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            UserSession.SetLoginStatus(false);
            UserSession.RemoveLoggedinUser();
            Response.Redirect("Login", false);
        }
    }
}