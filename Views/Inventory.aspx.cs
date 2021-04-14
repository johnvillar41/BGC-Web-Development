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

        protected void InventoryRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "InventoryCommand")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                Response.Redirect("UpdateInformation.aspx?id=" + id);
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
        
        protected void SearchOnCategory(object source, RepeaterCommandEventArgs e)
        {
            string category="";

            // categoryAll.ToString();

            // For categories other than All Products
            category = CategoryRepeater.Items.ToString();
            var newSearch = ProductRepository.GetInstance().FetchOnCategory(category);
            SearchRepeater.DataSource = newSearch;
            SearchRepeater.DataBind();
        }
    }
}