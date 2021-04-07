using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using SoftEngWebEmployee.Repository.LoginRepository;
using System;

namespace SoftEngWebEmployee.Views
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btn_login_Click(object sender, EventArgs e)
        {
            string username = txtbox_username.Text;
            string password = txtbox_password.Text;

            AdministratorModel administratorModel = new AdministratorModel
            {
                Username = username,
                Password = password
            };

            if (await LoginRepository.GetInstance().IsLoginSuccessfull(administratorModel) == true)
            {
                UserSession.SetLoginStatus(true);
                Response.Redirect("Inventory.aspx",false);               
            }
            else
            {
                Response.Redirect(Request.RawUrl, false);                                
            }
        }     
    }
}