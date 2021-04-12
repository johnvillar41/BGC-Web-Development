using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftEngWebEmployee.Views
{
    public partial class AddInformation : System.Web.UI.Page
    {
        private ProductModel Product { get; set; }
        private string Information { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                string id = Request.QueryString["id"].ToString();                         
                LoadProduct(int.Parse(id));
                LoadInformation(int.Parse(id));
            }
        }     
        public ProductModel DisplayProduct()
        {
            return Product;
        }
        public string DisplayInformation()
        {
            return Information;
        }
        private async void LoadProduct(int id)
        {
            var product = await ProductRepository.GetInstance().GetProducts(id);
            Product = product;
        }
        private async void LoadInformation(int id)
        {
            var information = await InformationRepository.GetInstance().FetchInformation(id);
            Information = information.ProductInformation;
        }       
    }
}