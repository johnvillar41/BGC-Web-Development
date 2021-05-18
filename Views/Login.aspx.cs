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
                BuildSweetAlert("#fff", "Login Error", "User not found!", Constants.AlertStatus.error);
            }
        }
        protected void BtnSendCode_Click(object sender, EventArgs e)
        {
            var email = EmailTextBox.Text.ToString();
            if (String.IsNullOrWhiteSpace(email))
            {
                BuildSweetAlert("#fff", "Empty Email!", "Please enter a valid email", Constants.AlertStatus.error);
                return;
            }
            EmailSender.BuildEmailSender(email);
            BuildSweetAlert("#fff", "Email sent!", "A code has been sent to you please confirm the code below", Constants.AlertStatus.info);
        }
        private void BuildSweetAlert(string hexBackgroundColor,string title,string message,Constants.AlertStatus alertStatus)
        {
            SweetAlertBuilder sweetAlertBuilder = new SweetAlertBuilder
            {
                HexaBackgroundColor = hexBackgroundColor,
                Title = title,
                Message = message,
                AlertIcons = alertStatus,
                ShowCloseButton = true,
                AlertPositions = Constants.AlertPositions.CENTER
            };
            sweetAlertBuilder.BuildSweetAlert(this);
        }       
    }
}