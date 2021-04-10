﻿using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.IO;
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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            var username = Username.Text.ToString();
            var password = Password.Text.ToString();
            var fullName = FullName.Text.ToString();

            if (!String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(password) && !String.IsNullOrWhiteSpace(fullName))
            {
                //Convert Image to String
                AdministratorModel administrator = null;
                Stream fs = ImageUpload.PostedFile.InputStream;
                BinaryReader br = new System.IO.BinaryReader(fs);
                byte[] bytes = br.ReadBytes((int)fs.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                var imageString = base64String;
                if(RadioButtonPosition.SelectedValue == "E")
                {
                    administrator = new AdministratorModel()
                    {
                        Username = username,
                        Password = password,
                        Fullname = fullName,
                        ProfilePicture = imageString,
                        EmployeeType = EmployeeType.Employee
                    };
                }
                else
                {
                    administrator = new AdministratorModel()
                    {
                        Username = username,
                        Password = password,
                        Fullname = fullName,
                        ProfilePicture = imageString,
                        EmployeeType = EmployeeType.Administrator
                    };
                }                

                AdministratorRepository.GetInstance().CreateNewAdministrator(administrator);
                NotificationRepository.GetInstance()
                    .InsertNewNotification(NotificationRepository
                    .GetInstance()
                    .GenerateNotification(NotificationType.CreateUser, username));                
                Response.Redirect(Request.RawUrl);                
            }

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(AdministratorId_Delete.Text))
            {
                AdministratorRepository.GetInstance().DeleteAdministrator(int.Parse(AdministratorId_Delete.Text));
                NotificationRepository.GetInstance()
                   .InsertNewNotification(NotificationRepository
                   .GetInstance()
                   .GenerateNotification(NotificationType.DeleteUser, AdministratorID.Text.ToString()));
                Response.Redirect(Request.RawUrl);
            }
        }

        protected async void ButtonFindID_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(AdministratorID.Text))
            {
                AdministratorModel administrator = await AdministratorRepository.GetInstance().FindAdministrator(int.Parse(AdministratorID.Text));
                if (administrator != null)
                {
                    UsernameUpdate.Text = administrator.Username;
                    FullnameUpdate.Text = administrator.Fullname;
                    PasswordUpdate.Text = administrator.Password;
                }
            }
            UpdatePanel1.Update();
        }

        protected void ButtonUpdateUser_Click(object sender, EventArgs e)
        {
            var userId = AdministratorID.Text.ToString();
            var username = UsernameUpdate.Text.ToString();
            var password = PasswordUpdate.Text.ToString();
            var fullName = FullnameUpdate.Text.ToString();

            if (!String.IsNullOrWhiteSpace(username) || !String.IsNullOrWhiteSpace(password) || !String.IsNullOrWhiteSpace(fullName))
            {
                AdministratorModel administrator = new AdministratorModel()
                {
                    User_ID = int.Parse(userId),
                    Username = username,
                    Password = password,
                    Fullname = fullName
                };
                AdministratorRepository.GetInstance().UpdateAdministrator(administrator);
                NotificationRepository.GetInstance()
                   .InsertNewNotification(NotificationRepository
                   .GetInstance()
                   .GenerateNotification(NotificationType.UpdateUser, username));
                Response.Redirect(Request.RawUrl);
            }
        }

        private async void LoadAdministrators()
        {
            var administrators = await AdministratorRepository.GetInstance().FetchAdministrators();
            Admins = (List<AdministratorModel>)administrators;
        }
        private async void LoadIsAdministrator(string username)
        {
            IsAdminUser = await AdministratorRepository.GetInstance().CheckIfUserIsAdministrator(username);           
        }
    }
}