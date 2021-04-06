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

            AdministratorModel administratorModel = new AdministratorModel()
            {
                User_Username = username,
                User_Password = password
            };

            //bool loginResult = await LoginRepository.GetInstance().IsLoginSuccessfull(administratorModel);

            //if (loginResult == false)
            //{
            //    Response.Redirect(Request.RawUrl, false);
            //}
            //else
            //{
            //    Response.Redirect("Inventory", false);
            //    UserSession.IsLoggedin = true;
            //}

        }
    }
}