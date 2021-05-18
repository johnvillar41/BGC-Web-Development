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

            if (await LoginRepository.SingleInstance.IsLoginSuccessfullAsync(administratorModel) == true)
            {
                UserSession.SetLoginStatus(true);
                UserSession.SetLoginUser(username);
                Response.Redirect("~/Views/Inventory.aspx", false);
            }
            else
            {
                BuildSweetAlert();
            }
        }       
        private void BuildSweetAlert()
        {
            SweetAlertBuilder sweetAlertBuilder = new SweetAlertBuilder
            {
                HexaBackgroundColor = "#fff",
                Title = "Login Error!",
                Message = "Invalid Credentials",
                AlertIcons = Constants.AlertStatus.error,
                ShowCloseButton = true,
                AlertPositions = Constants.AlertPositions.CENTER
            };
            sweetAlertBuilder.BuildSweetAlert(this);
        }
    }
}