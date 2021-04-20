using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using System;
using System.Threading.Tasks;

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

        public async Task<ProductSalesReportModel>  FetchProductSalesReport(int productID)
        {
            ProductSalesReportModel ProductSalesReportModel = null;

            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                connection.Open();
                string queryString = "SELECT SUM(onsite_transaction_table.total_sale) as productRevenue," +
                    "onsite_products_transaction_table.transaction_id, " +
                    "SUM(onsite_products_transaction_table.total_product_count) as quantitySold," +
                    "onsite_products_transaction_table.product_id FROM onsite_transaction_table INNER JOIN onsite_products_transaction_table ON onsite_transaction_table.transaction_id = onsite_products_transaction_table.transaction_id WHERE onsite_products_transaction_table.product_id=@productID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@productID", productID);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ProductSalesReportModel = new ProductSalesReportModel();
                    ProductSalesReportModel.Product = await ProductRepository.GetInstance().FetchProductDetails(productID.ToString());                   
                    ProductSalesReportModel.QuantitySold = int.Parse(reader["quantitySold"].ToString());                   
                    ProductSalesReportModel.ProductRevenue = int.Parse(reader["productRevenue"].ToString());
                }

                return ProductSalesReportModel;
            }       
        }
    }
}