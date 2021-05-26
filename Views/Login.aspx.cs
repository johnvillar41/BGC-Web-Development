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
        public bool IsCodeConfirmed { get; set; }
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
            var isLoginSuccesfull = await LoginRepository.SingleInstance.IsLoginSuccessfullAsync(administratorModel);
            if (isLoginSuccesfull)
            {
                bool isAdmin = await AdministratorRepository.SingleInstance.CheckIfUserIsAdministratorAsync(username);               
                if (isAdmin)
                    UserSession.SingleInstance.SetLoginUser(username,Constants.EmployeeType.Administrator);
                else
                    UserSession.SingleInstance.SetLoginUser(username, Constants.EmployeeType.Employee);
                Response.Redirect("~/Views/Inventory.aspx", false);
            }
            else
            {
                BuildSweetAlert("#fff", "Login Error", "User not found!", Constants.AlertStatus.error);
            }
        }
        protected async void BtnSendCode_Click(object sender, EventArgs e)
        {
            var email = EmailTextBox.Text.ToString();
            if (String.IsNullOrWhiteSpace(email))
            {
                BuildSweetAlert("#fff", "Empty Email!", "Please enter a valid email", Constants.AlertStatus.error);
                return;
            }
            var generatedCode = EmailSender.GenerateRandomCode();
            EmailSender.BuildEmailSender(email, generatedCode,this);
            await LoginRepository.SingleInstance.UpdateCode(email, generatedCode);
            BuildSweetAlert("#fff", "Email sent!", "A code has been sent to you please confirm the code below", Constants.AlertStatus.info);
        }
        protected async void BtnConfirmCode_Click(object sender, EventArgs e)
        {
            var email = EmailTextBox.Text.ToString();
            var code = CodeConfirmation.Text.ToString();
            var isCodeEqual = await LoginRepository.SingleInstance.CheckIfCodeIsEqual(email, code);
            if (!String.IsNullOrWhiteSpace(code) && isCodeEqual)
            {                
                IsCodeConfirmed = true;
                var newPassword = NewPassword.Text.ToString();
                if (!String.IsNullOrWhiteSpace(newPassword))
                {
                    await LoginRepository.SingleInstance.UpdateNewPassword(email, newPassword);
                    BuildSweetAlert("#fff", "Password Updated!", "Your password has been updated", Constants.AlertStatus.success);
                }
                return;
            }
            BuildSweetAlert("#fff", "Code not equal!", "The code you entered is not the same!", Constants.AlertStatus.error);
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
                AlertPositions = Constants.AlertPositions.TOP_START
            };
            sweetAlertBuilder.BuildSweetAlert(this);
        }        
    }
}