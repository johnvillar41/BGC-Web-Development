using SoftEngWebEmployee.Models;
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
                DisplayDashBoard();
                DisplayProductSalesReport();
            }
        }


        private async void DisplayProductSalesReport()
        {
            var productList = await ProductRepository.GetInstance().FetchAllProducts();
            List<ProductSalesReportModel> ProductSalesList = new List<ProductSalesReportModel>();
            foreach (var product in productList)
            {
                try
                {
                    var productSalesReport = await ProductSalesReportRepository.GetInstance().FetchProductSalesReport(product.Product_ID);
                    ProductSalesList.Add(productSalesReport);
                }
                catch (Exception)
                {

                }
            }
            ProductsRepeater.DataSource = ProductSalesList;
            ProductsRepeater.DataBind();
        }


        private void DisplayDashBoard() 
        {
            total_sales.Text = ReportsRepository.GetInstance().FetchTotalSales().ToString();
            total_inventory.Text = ReportsRepository.GetInstance().FetchTotalInventory().ToString();
            total_products.Text = ReportsRepository.GetInstance().FetchTotalProducts().ToString();
        }

    }
}