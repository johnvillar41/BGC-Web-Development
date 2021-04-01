using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository.AdministratorRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftEngWebEmployee.Views
{
    public partial class Administrators : System.Web.UI.Page
    {
        private readonly IAdministratorRepository _repository = new AdministratorRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public IEnumerable<AdministratorModel> DisplayMockAdmins()
        {
            var administrators = _repository.FetchAdministrators();
            return administrators;           
        }

    }
}