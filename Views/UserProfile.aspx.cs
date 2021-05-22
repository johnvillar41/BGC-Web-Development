using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Views
{
    public partial class UserProfile : System.Web.UI.Page
    {
        public string ImageString { get; set; }
        public EmployeeType EmployeeType { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserProfile();
            }
        }
        private async void LoadUserProfile()
        {
            var userLoggedIn = await UserProfileRepository.SingleInstance.FetchLoggedInModelAsync();
            if (userLoggedIn != null)
            {
                Username.Text = userLoggedIn.Username;
                Fullname.Text = userLoggedIn.Fullname;
                Password.Text = userLoggedIn.Password;
                ImageString = userLoggedIn.ProfilePicture;
                EmployeeType = userLoggedIn.EmployeeType;
                FullnameLabel.Text = userLoggedIn.Fullname;
                Email.Text = userLoggedIn.Email;
            }            
        }

        protected async void ButtonSaveProfile_Click(object sender, EventArgs e)
        {
            UpdateProgress1.Visible = true;            
            var updatedUser = new AdministratorModel()
            {
                Username = Username.Text,
                Fullname = Fullname.Text,
                Password = Password.Text,
                Email = Email.Text
            };
            try
            {
                await UserProfileRepository.SingleInstance.UpdateProfileAsync(updatedUser, UserSession.SingleInstance.GetLoggedInUser());
            }
            catch(MySql.Data.MySqlClient.MySqlException ex)
            {
                if (ex.Message.Equals($"Duplicate entry '{updatedUser.Username}' for key 'user_username'"))
                {
                    BuildSweetAlert(updatedUser.Username);
                    return;
                }
            }
            FullnameLabel.Text = updatedUser.Fullname;            
            UpdateProgress1.Visible = false;          
        }
        private void BuildSweetAlert(string username)
        {
            var alert = new SweetAlertBuilder
            {
                HexaBackgroundColor = "#fff",
                Title = "Duplicate Name!",
                Message = $"Duplicate name for:{username}",
                AlertIcons = Constants.AlertStatus.warning
            };
            alert.BuildSweetAlert(this);
        }
        protected async void UploadImage_Click(object sender, EventArgs e)
        {
            Stream fs = ProfileFileUpload.PostedFile.InputStream;           
            await UserProfileRepository.SingleInstance.UpdateProfilePictureAsync(fs);
            Response.Redirect(Request.RawUrl, false);
        }
    }
}