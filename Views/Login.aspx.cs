using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using SoftEngWebEmployee.Repository.LoginRepository;
using System;
using System.Web.UI;

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
                UserSession.SetLoginUser(username);
                Response.Redirect("~/Views/Inventory.aspx", false);
            }
            else
            {
                SweetAlertBuilder.BuildMessage(this, Constants.AlertStatus.error, "Error Loggin In!", "Invalid Credentials");               
            }
        }
    }
}