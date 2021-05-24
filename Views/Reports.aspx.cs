using SoftEngWebEmployee.Helpers;
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
        public int TotalSaleOnsite { get; set; }
        public int TotalSaleOrder { get; set; }
        public int TotalSaleOnsite_GivenDate { get; set; }
        public int TotalSaleOrder_GivenDate { get; set; }
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
        protected void FindDate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Date.Text.ToString()))
            {
                SweetAlertBuilder sweetAlert = new SweetAlertBuilder
                {
                    HexaBackgroundColor = "#fff",
                    Title = "Error Date",
                    Message = "Invalid Date Pls Pick a Date!",
                    AlertIcons = Constants.AlertStatus.error
                };
                sweetAlert.BuildSweetAlert(this);
                return;
            }
            DateTime date = DateTime.Parse(Date.Text.ToString());
            LoadTotalSalesAtGivenDate(date);
        }
        private async void DisplayProductSalesReport()
        {
            var productList = await ProductRepository.SingleInstance.FetchAllProductsAsync();
            List<ProductSalesReportViewModel> ProductSalesList = new List<ProductSalesReportViewModel>();
            foreach (var product in productList)
            {
                var productSalesReport = await ProductSalesReportRepository.SingleInstance.FetchProductSalesReportAsync(product.Product_ID);
                var listOfQuantitySold_Onsite = await ProductSalesReportRepository.SingleInstance.FetchQuantitySoldOnsiteAsync(product.Product_ID);
                var listOfQuantitySold_Order = await ProductSalesReportRepository.SingleInstance.FetchQuantitySoldOrderAsync(product.Product_ID);
                ProductSalesList.Add(
                        new ProductSalesReportViewModel
                        {
                            ProductReport = productSalesReport,
                            QuantitySold_Onsite = listOfQuantitySold_Onsite,
                            QuantitySold_Order = listOfQuantitySold_Order
                        }
                    );

            }
            ProductSalesListDisplay = ProductSalesList;
        }
        private async void LoadTotalSalesAtGivenDate(DateTime date)
        {
            var orderIds = await SalesIncomeReportRepository.SingleInstance.FetchOrderIdsAsync(date);
            var onsiteIds = await SalesIncomeReportRepository.SingleInstance.FetchOnsiteIdsAsync(date);

            var totalSaleOrders = 0;
            var totalSaleOnsite = 0;
            foreach (var id in orderIds)
            {
                totalSaleOrders += await OrdersRepository.SingleInstance.CalculateTotalSaleOrderAsync(id);
            }
            foreach (var id in onsiteIds)
            {
                totalSaleOnsite += await OnsiteTransactionRepository.SingleInstance.CalculateTotalSaleOnsite(id);
            }
            int totalSale = totalSaleOrders + totalSaleOnsite;
            TotalSaleGivenDate.Text = totalSale.ToString();
            TotalSaleOnsite_GivenDate = totalSaleOnsite;
            TotalSaleOrder_GivenDate = totalSaleOrders;
        }
        private async void LoadTotalSalesToday()
        {
            var orderIds = await SalesIncomeReportRepository.SingleInstance.FetchOrderIdsAsync(DateTime.Now);
            var onsiteIds = await SalesIncomeReportRepository.SingleInstance.FetchOnsiteIdsAsync(DateTime.Now);

            var totalSaleOrders = 0;
            var totalSaleOnsite = 0;
            foreach (var id in orderIds)
            {
                totalSaleOrders += await OrdersRepository.SingleInstance.CalculateTotalSaleOrderAsync(id);
            }
            foreach (var id in onsiteIds)
            {
                totalSaleOnsite += await OnsiteTransactionRepository.SingleInstance.CalculateTotalSaleOnsite(id);
            }
            int totalSale = totalSaleOrders + totalSaleOnsite;
            TotalSaleOnsite = totalSaleOnsite;
            TotalSaleOrder = totalSaleOrders;
            TotalSale.Text = totalSale.ToString();
        }
        private async void DisplayListOfAdministrators()
        {
            var admins = await AdministratorRepository.SingleInstance.FetchAdministratorsAsync();
            List<SalesIncomeReportViewModel> listOfIncomeReports = new List<SalesIncomeReportViewModel>();
            foreach (var admin in admins)
            {
                try
                {
                    var totalSale = await SalesIncomeReportRepository.SingleInstance.FetchTotalSaleOfAdminAsync(admin.Username);
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
            var totalSales = await DashboardRepository.SingleInstance.FetchTotalSalesAsync();
            var totalInventory = await DashboardRepository.SingleInstance.FetchTotalInventoryAsync();
            var totalProducts = await DashboardRepository.SingleInstance.FetchTotalProductsAsync();

            total_sales.Text = totalSales.ToString();
            total_inventory.Text = totalInventory.ToString();
            total_products.Text = totalProducts.ToString();
        }


    }
}