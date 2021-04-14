using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
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
            }
        }
        private async void LoadSales()
        {
            var salesList = await SalesRepository.GetInstance().FetchAllSales();
            SalesRepeater.DataSource = salesList;
            SalesRepeater.DataBind();
        }

        protected void SalesRepeater_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "SalesCommand")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                Response.Redirect("DisplaySales.aspx?id=" + id);             
            }
        }       
    }
}