using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;

namespace SoftEngWebEmployee.Views
{
    public partial class AddInformation : System.Web.UI.Page
    {
        private ProductModel Product { get; set; }
        private string Information { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null && !IsPostBack)
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
            ProductIDTextBox.Text = id.ToString();
            InformationTextBox.Text = information.ProductInformation.ToString();
        }

        protected async void BtnSubmitInformation_Click(object sender, EventArgs e)
        {
            var product = new ProductModel()
            {
                Product_ID = int.Parse(ProductIDTextBox.Text)
            };
            string information = InformationTextBox.Text.ToString();
            var informationObj = new InformationModel()
            {
                Product = product,
                ProductInformation = information
            };
            await InformationRepository.GetInstance().UpdateInformation(informationObj);
            Response.Redirect("Information.aspx", false);

        }
    }
}