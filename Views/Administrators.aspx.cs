using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Views
{
    public partial class Administrators : System.Web.UI.Page
    {
        private List<AdministratorModel> Admins { get; set; }
        private bool IsAdminUser { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadAdministrators();
            LoadIsAdministrator(UserSession.GetLoggedInUser());
        }
        public List<AdministratorModel> DisplayAdministrators()
        {
            return Admins;
        }
        public bool IsAdmin()
        {
            return IsAdminUser;
        }
        protected async void BtnSave_Click(object sender, EventArgs e)
        {
            var username = Username.Text.ToString();
            var password = Password.Text.ToString();
            var fullName = FullName.Text.ToString();
            var email = Email.Text.ToString();
            if (!String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(password) && !String.IsNullOrWhiteSpace(fullName))
            {
                AdministratorModel administrator = null;
                Stream fs = ImageUpload.PostedFile.InputStream;
                BinaryReader br = new System.IO.BinaryReader(fs);
                byte[] bytes = br.ReadBytes((int)fs.Length);

                administrator = new AdministratorModel()
                {
                    Username = username,
                    Password = password,
                    Fullname = fullName,
                    Email = email,
                    ProfilePictureUpload = bytes,
                };
                if (RadioButtonPosition.SelectedValue == "E")
                {
                    administrator.EmployeeType = EmployeeType.Employee;
                }
                else
                {
                    administrator.EmployeeType = EmployeeType.Administrator;
                }
                try
                {
                    await AdministratorRepository.SingleInstance.CreateNewAdministrator(administrator);
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    if (ex.Message.Equals($"Duplicate entry '{administrator.Username}' for key 'user_username'"))
                    {
                        BuildSweetAlert("#fff", "Duplicate Username", $"Duplicate Username for {administrator.Username} try another one", AlertStatus.error);
                        return;
                    }
                }

                await NotificationRepository.SingleInstance
                    .InsertNewNotificationAsync(NotificationRepository
                    .SingleInstance
                    .GenerateNotification(NotificationType.CreateUser, username));
                BuildSweetAlert("#fff", "Successfull!", $"Successfully Added {administrator.Username} as {administrator.EmployeeType.ToString()}", AlertStatus.success);
                Response.Redirect(Request.RawUrl, false);               
            }
        }
        protected async void BtnDelete_Click(object sender, EventArgs e)
        {
            UpdateProgress_Delete.Visible = true;
            if (!String.IsNullOrWhiteSpace(AdministratorId_Delete.Text))
            {
                await AdministratorRepository.SingleInstance.DeleteAdministrator(int.Parse(AdministratorId_Delete.Text));
                await NotificationRepository.SingleInstance
                   .InsertNewNotificationAsync(NotificationRepository
                   .SingleInstance
                   .GenerateNotification(NotificationType.DeleteUser, AdministratorID.Text.ToString()));
            }
            Thread.Sleep(5000);
            LoadAdministrators();
            UpdateProgress_Delete.Visible = false;
        }

        protected async void ButtonFindID_Click(object sender, EventArgs e)
        {
            UpdateProgress_Main.Visible = true;
            if (!String.IsNullOrWhiteSpace(AdministratorID.Text))
            {
                AdministratorModel administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(int.Parse(AdministratorID.Text));
                if (administrator != null)
                {
                    UsernameUpdate.Text = administrator.Username;
                    FullnameUpdate.Text = administrator.Fullname;
                    PasswordUpdate.Text = administrator.Password;
                    EmailUpdate.Text = administrator.Email;
                }
            }
            UpdatePanel1.Update();
            UpdateProgress_Main.Visible = false;
        }

        protected async void ButtonUpdateUser_Click(object sender, EventArgs e)
        {
            UpdateProgress_Update.Visible = true;
            var userId = AdministratorID.Text.ToString();
            var username = UsernameUpdate.Text.ToString();
            var password = PasswordUpdate.Text.ToString();
            var fullName = FullnameUpdate.Text.ToString();
            var email = EmailUpdate.Text.ToString();

            if (!String.IsNullOrWhiteSpace(username) || !String.IsNullOrWhiteSpace(password) || !String.IsNullOrWhiteSpace(fullName))
            {
                AdministratorModel administrator = new AdministratorModel()
                {
                    User_ID = int.Parse(userId),
                    Username = username,
                    Password = password,
                    Fullname = fullName,
                    Email = email
                };
                await AdministratorRepository.SingleInstance.UpdateAdministrator(administrator);
                await NotificationRepository.SingleInstance
                   .InsertNewNotificationAsync(NotificationRepository
                   .SingleInstance
                   .GenerateNotification(NotificationType.UpdateUser, username));
            }
            Thread.Sleep(5000);
            LoadAdministrators();
            UpdateProgress_Update.Visible = false;
            BuildSweetAlert("#fff", "Successfull", $"Successfully Updated User:{username}", AlertStatus.success);
        }
        private void BuildSweetAlert(string hexbgColor, string title, string message, Constants.AlertStatus alertStatus)
        {
            var sweetAlert = new SweetAlertBuilder
            {
                HexaBackgroundColor = hexbgColor,
                Title = title,
                Message = message,
                AlertIcons = alertStatus,
                ShowConfirmationDialog = true,
            };
            sweetAlert.BuildSweetAlert(this);
        }
        private async void LoadAdministrators()
        {
            var administrators = await AdministratorRepository.SingleInstance.FetchAdministratorsAsync();
            Admins = (List<AdministratorModel>)administrators;
        }
        private async void LoadIsAdministrator(string username)
        {
            IsAdminUser = await AdministratorRepository.SingleInstance.CheckIfUserIsAdministratorAsync(username);
        }
    }
}