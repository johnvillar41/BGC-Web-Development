using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var userLoggedIn = await UserProfileRepository.SingleInstance.FetchLoggedInModel();
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
            var updatedUser = new AdministratorModel()
            {
                Username = Username.Text,
                Fullname = Fullname.Text,
                Password = Password.Text
            };
            await UserProfileRepository.SingleInstance.UpdateProfile(updatedUser);
            Response.Redirect(Request.RawUrl, false);
        }

        protected async void UploadImage_Click(object sender, EventArgs e)
        {
            Stream fs = ProfileFileUpload.PostedFile.InputStream;           
            await UserProfileRepository.SingleInstance.UpdateProfilePicture(fs);
            Response.Redirect(Request.RawUrl, false);
        }
    }
}