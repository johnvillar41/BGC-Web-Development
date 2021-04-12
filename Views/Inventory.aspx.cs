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
                DisplayInventoryTable();
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

        private async void DisplayInventoryTable()
        {
            var inventory = await ProductRepository.GetInstance().FetchAllProducts();
            
            InventoryRepeater.DataSource = inventory;
            InventoryRepeater.DataBind();
        }
    }
}