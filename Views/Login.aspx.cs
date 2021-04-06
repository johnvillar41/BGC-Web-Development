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

            AdministratorModel administratorModel = new AdministratorModel();
            administratorModel.User_Username = username;
            administratorModel.User_Password = password;

            if (await LoginRepository.GetInstance().IsLoginSuccessfull(administratorModel) == true)
            {
                Response.Redirect("Inventory.aspx",false);
                UserSession.SetLoginStatus(true);
            }
            else
            {
                Response.Redirect(Request.RawUrl, false);
                UserSession.SetLoginStatus(false);
                //add pop up error message
            }



        }

        


    }
}