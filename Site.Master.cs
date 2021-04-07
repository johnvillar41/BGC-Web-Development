using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftEngWebEmployee
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (UserSession.GetLoginStatus() == false)
            //{
            //    Response.Redirect("Login.aspx", false);
            //}
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            UserSession.SetLoginStatus(false);
            Response.Redirect("Login.aspx", false);
        }
    }
}