using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository.AdministratorRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftEngWebEmployee.Views
{
    public partial class Administrators : System.Web.UI.Page
    {
        public List<AdministratorModel> Admins { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadAdministrators();
        }

        public List<AdministratorModel> DisplayAdministrators()
        {
            return Admins;
        }

        private async void LoadAdministrators()
        {
            var administrators = await AdministratorRepository.GetInstance().FetchAdministrators();
            Admins = (List<AdministratorModel>)administrators;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {            
            var username = Username.Text.ToString();
            var password = Password.Text.ToString();
            var fullName = FullName.Text.ToString();

            if(!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password) && !String.IsNullOrEmpty(fullName))
            {
                //Convert Image to String
                Stream fs = ImageUpload.PostedFile.InputStream;
                BinaryReader br = new System.IO.BinaryReader(fs);
                byte[] bytes = br.ReadBytes((int)fs.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                var imageString = base64String;

                AdministratorModel administrator = new AdministratorModel()
                {
                    Username = username,
                    Password = password,
                    Fullname = fullName,
                    ProfilePicture = imageString
                };

                AdministratorRepository.GetInstance().CreateNewAdministrator(administrator);
                Response.Redirect(Request.RawUrl);
            }
            
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {            
            AdministratorRepository.GetInstance().DeleteAdministrator(int.Parse(AdministratorId_Delete.Text));
            Response.Redirect(Request.RawUrl);
        }
    }
}