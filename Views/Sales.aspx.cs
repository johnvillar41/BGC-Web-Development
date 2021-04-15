using SoftEngWebEmployee.Repository;
using System;
using System.Web.UI.WebControls;

namespace SoftEngWebEmployee.Views
{
    public partial class Sales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSales();
                LoadProducts();
            }
        }
        private async void LoadSales()
        {
            var salesList = await SalesRepository.GetInstance().FetchAllSales();
            if (salesList != null)
            {
                SalesRepeater.DataSource = salesList;
                SalesRepeater.DataBind();
            }           
        }
        private async void LoadProducts()
        {
            var productsList = await ProductRepository.GetInstance().FetchAllProducts();
            ProductsRepeater.DataSource = productsList;
            ProductsRepeater.DataBind();
        }

        protected void IDS_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] arguments = btn.CommandArgument.Split(';');
            string orderID = arguments[0];
            string onsiteID = arguments[1];
            if (orderID.Length != 0)
            {                
                Session["orderID"] = orderID;                
            }
            else
            {
                Session["onsiteID"] = onsiteID;               
            }
            Response.Redirect("DisplaySales");
        }
    }
}