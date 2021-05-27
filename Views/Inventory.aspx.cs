using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using SoftEngWebEmployee.Helpers;
using System.Threading;

namespace SoftEngWebEmployee.Views
{
    public partial class Inventory : System.Web.UI.Page
    {
        public List<ProductModel> listSearchRepeater { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                DisplayInventoryTables();
            }
        }
        private async void LoadCategories()
        {
            var categories = await ProductRepository.SingleInstance.FetchAllCategoriesAsync();
            CategoryRepeater.DataSource = categories;
            CategoryRepeater.DataBind();
        }
        private async void DisplayInventoryTables()
        {
            var inventory = await ProductRepository.SingleInstance.FetchAllProductsAsync();
            SearchRepeater.DataSource = inventory;
            listSearchRepeater = inventory;
            SearchRepeater.DataBind();            
        }
        protected void btnInventoryAdd_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("InventoryAdd.aspx", false);
        }
        protected void CategoryRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            Button button = e.Item.FindControl("category") as Button;
            ScriptManager current = ScriptManager.GetCurrent(Page);
            if (current != null)
                current.RegisterAsyncPostBackControl(button);
        }
        protected async void Category_Click(object sender, EventArgs e)
        {
            string category = (sender as Button).Text.ToString();
            char caret = Convert.ToChar(0x000025BC);
            dropdownMenuReference1.Text = category + " " + caret;
            if (category == "All Products")
            {
                DisplayInventoryTables();
            }
            else
            {
                var newSearch = await ProductRepository.SingleInstance.FetchOnCategoryAsync(category);
                SearchRepeater.DataSource = newSearch;
                listSearchRepeater = newSearch;
                SearchRepeater.DataBind();               
            }
        }
        protected async void SearchButton_Click(object sender, EventArgs e)
        {
            string search = searchBox.Text.ToString();
            var newSearch = await ProductRepository.SingleInstance.FetchOnSearchAsync(search);
            SearchRepeater.DataSource = newSearch;
            listSearchRepeater = newSearch;
            SearchRepeater.DataBind();            
        }
        protected async void RetrieveDetails(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var productID = button.CommandArgument.ToString();
            ProductModel Details = await ProductRepository.SingleInstance.FetchProductDetailsAsync(productID);

            List<ProductModel> ProductDetail = new List<ProductModel>
            {
                Details
            };

            DetailsRepeater.DataSource = ProductDetail;
            DetailsRepeater.DataBind();                 
        }        
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var productID = button.CommandArgument.ToString();
            Response.Redirect($"InventoryUpdate.aspx?id={productID}",false);            
        }       

        protected void StocksLabel_DataBinding(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            int stocksNumber = (int)Eval("ProductStocks");
            lbl.Text = $"Stocks: {stocksNumber}";
            if(stocksNumber > 50)
                lbl.ForeColor = System.Drawing.Color.Green;
            if (stocksNumber < 50)            
                lbl.ForeColor = System.Drawing.Color.Blue;            
            if(stocksNumber < 20)            
                lbl.ForeColor = System.Drawing.Color.Red;
            
        }
        
    }
}