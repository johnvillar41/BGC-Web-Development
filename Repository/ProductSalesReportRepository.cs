using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public ProductSalesReportModel FetchProductSalesReport()
        {
            throw new Exception();
            //planning pa po
        }


    }
}