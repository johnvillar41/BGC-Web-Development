using SoftEngWebEmployee.Repository;
using System;
using System.Web.UI;
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
                LoadCategories();
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
        private async void LoadCategories()
        {
            var categories = await ProductRepository.GetInstance().FetchAllCategories();
            CategoryRepeater.DataSource = categories;
            CategoryRepeater.DataBind();
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

        protected async void CategoryBtn_Click(object sender, EventArgs e)
        {
            string category = (sender as Button).Text.ToString();

            var newSearch = await ProductRepository.GetInstance().FetchOnCategory(category);
            ProductsRepeater.DataSource = newSearch;
            ProductsRepeater.DataBind();

        }


        protected void CategoryRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            Button button = e.Item.FindControl("CategoryBtn") as Button;

            ScriptManager current = ScriptManager.GetCurrent(Page);
            if (current != null)
                current.RegisterAsyncPostBackControl(button);
        }

        protected async void CategoryBtnAllProducts_Click(object sender, EventArgs e)
        {
            var newSearch = await ProductRepository.GetInstance().FetchAllProducts();
            ProductsRepeater.DataSource = newSearch;
            ProductsRepeater.DataBind();
        }
    }
}