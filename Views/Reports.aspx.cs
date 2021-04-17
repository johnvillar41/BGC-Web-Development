using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftEngWebEmployee.Views
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportsRepository reportsRepository = new ReportsRepository();

                total_sales.Text = reportsRepository.FetchTotalSales().ToString();
                total_inventory.Text = reportsRepository.FetchTotalInventory().ToString();
                total_products.Text = reportsRepository.FetchTotalProducts().ToString();


            }
        }
    }
}