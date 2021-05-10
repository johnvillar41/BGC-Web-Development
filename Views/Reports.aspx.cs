using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Models.ReportModel;
using SoftEngWebEmployee.Repository;
using SoftEngWebEmployee.Repository.ReportsRepository;
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
        public List<ProductSalesReportViewModel> ProductSalesListDisplay { get; set; }
        public List<SalesIncomeReportViewModel> SalesIncomeDisplay { get; set; }        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayDashBoard();
                DisplayProductSalesReport();
                DisplayListOfAdministrators();
                LoadTotalSalesToday();
            }
        }
        private async void DisplayProductSalesReport()
        {
            var productList = await ProductRepository.GetInstance().FetchAllProducts();
            List<ProductSalesReportViewModel> ProductSalesList = new List<ProductSalesReportViewModel>();
            foreach (var product in productList)
            {
                try
                {
                    var productSalesReport = await ProductSalesReportRepository.GetInstance().FetchProductSalesReport(product.Product_ID);
                    var listOfQuantitySold = await ProductSalesReportRepository.GetInstance().FetchQuantitySoldList(product.Product_ID);
                    ProductSalesList.Add(
                            new ProductSalesReportViewModel
                            {
                                ProductReport = productSalesReport,
                                QuantitySold = listOfQuantitySold
                            }
                        );
                }
                catch (Exception)
                {

                }
            }
            ProductSalesListDisplay = ProductSalesList;
        }
        private async void LoadTotalSalesToday()
        {
            var orderIds = await SalesIncomeReportRepository.GetInstance().FetchOrderIds();
            var onsiteIds = await SalesIncomeReportRepository.GetInstance().FetchOnsiteIds();

            var totalSaleOrders = 0;
            var totalSaleOnsite = 0;
            foreach (var id in orderIds)
            {
                totalSaleOrders += await OrdersRepository.GetInstance().CalculateTotalSaleOrder(id);
            }
            foreach(var id in onsiteIds)
            {
                totalSaleOnsite += await OnsiteTransactionRepository.GetInstance().CalculateTotalSaleOnsite(id);
            }
            int totalSale = totalSaleOrders + totalSaleOnsite;
            TotalSale.Text = totalSale.ToString();
        }
        private async void DisplayListOfAdministrators()
        {
            var admins = await AdministratorRepository.GetInstance().FetchAdministrators();
            List<SalesIncomeReportViewModel> listOfIncomeReports = new List<SalesIncomeReportViewModel>();
            foreach (var admin in admins)
            {
                try
                {
                    var totalSale = await SalesIncomeReportRepository.GetInstance().FetchTotalSaleOfAdmin(admin.Username);
                    SalesIncomeReportViewModel salesIncomeReportViewModel = new SalesIncomeReportViewModel
                    {
                        Administrator = admin,
                        TotalSale = totalSale.TotalSale,
                        TotalSaleOnsite = totalSale.TotalSaleOnsite,
                        TotalSaleOrders = totalSale.TotalSaleOrders
                    };
                                       
                    listOfIncomeReports.Add(salesIncomeReportViewModel);
                }
                catch (Exception)
                {
                   
                }

            }
            SalesIncomeDisplay = listOfIncomeReports;
        }
        private async void DisplayDashBoard()
        {
            var totalSales = await DashboardRepository.GetInstance().FetchTotalSales();
            var totalInventory = await DashboardRepository.GetInstance().FetchTotalInventory();
            var totalProducts = await DashboardRepository.GetInstance().FetchTotalProducts();

            total_sales.Text = totalSales.ToString();
            total_inventory.Text = totalInventory.ToString();
            total_products.Text = totalProducts.ToString();
        }

    }
}