using SoftEngWebEmployee.Models;
using System;

namespace SoftEngWebEmployee.Repository
{
    public class ProductSalesReportRepository
    {
        private static ProductSalesReportRepository instance = null;
        public static ProductSalesReportRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductSalesReportRepository();
            }
            return instance;
        }

        private ProductSalesReportRepository()
        {

        }

        public ProductSalesReportModel FetchProductSalesReport(int productID)
        {

            throw new Exception();
        }
    }
}