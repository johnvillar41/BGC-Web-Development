using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository.AdministratorRepository;
using System;
using System.Collections.Generic;
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


    }
}