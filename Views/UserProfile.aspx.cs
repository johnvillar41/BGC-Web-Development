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
            }            
        }

        protected async void ButtonSaveProfile_Click(object sender, EventArgs e)
        {
            UpdateProgress1.Visible = true;
            var updatedUser = new AdministratorModel()
            {
                Username = Username.Text,
                Fullname = Fullname.Text,
                Password = Password.Text
            };
            await UserProfileRepository.SingleInstance.UpdateProfileAsync(updatedUser);
            FullnameLabel.Text = updatedUser.Fullname;
            Thread.Sleep(5000);
            UpdateProgress1.Visible = false;          
        }
            

        protected async void UploadImage_Click(object sender, EventArgs e)
        {
            Stream fs = ProfileFileUpload.PostedFile.InputStream;           
            await UserProfileRepository.SingleInstance.UpdateProfilePictureAsync(fs);
            Response.Redirect(Request.RawUrl, false);
        }
    }
}