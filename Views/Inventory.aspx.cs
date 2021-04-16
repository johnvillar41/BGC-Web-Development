using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;

namespace SoftEngWebEmployee.Views
{
    public partial class Inventory : System.Web.UI.Page
    {
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
            var categories = await ProductRepository.GetInstance().FetchAllCategories();
            CategoryRepeater.DataSource = categories;
            CategoryRepeater.DataBind();
        }

        private async void DisplayInventoryTables()
        {
            var inventory = await ProductRepository.GetInstance().FetchAllProducts();            
            SearchRepeater.DataSource = inventory;
            SearchRepeater.DataBind();

            var greenhouse = await ProductRepository.GetInstance().FetchGHProducts();
            GHRepeater.DataSource = greenhouse;
            GHRepeater.DataBind();

            var hydroponics = await ProductRepository.GetInstance().FetchHPProducts();
            HPRepeater.DataSource = hydroponics;
            HPRepeater.DataBind();
        }

        protected async void category_Click(object sender, EventArgs e)
        {
            string category = (sender as Button).Text.ToString();
            dropdownMenuReference1.Text = category;
            if (category=="All Products")
            {
                var newSearch = await ProductRepository.GetInstance().FetchAllProducts();
                SearchRepeater.DataSource = newSearch;
                SearchRepeater.DataBind();
            }
            else
            {
                var newSearch = await ProductRepository.GetInstance().FetchOnCategory(category);
                SearchRepeater.DataSource = newSearch;
                SearchRepeater.DataBind();
            }           
        }

        protected async void searchButton_Click(object sender, EventArgs e)
        {
            string search = searchBox.Text.ToString();
            var newSearch = await ProductRepository.GetInstance().FetchOnSearch(search);
            SearchRepeater.DataSource = newSearch;
            SearchRepeater.DataBind();
        }

        protected void CategoryRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            Button button = e.Item.FindControl("category") as Button;

            ScriptManager current = ScriptManager.GetCurrent(Page);
            if (current != null)
                current.RegisterAsyncPostBackControl(button);
        }
    }
}