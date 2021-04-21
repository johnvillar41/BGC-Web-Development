using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftEngWebEmployee.Repository.ReportsRepository
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
                await connection.OpenAsync();
                string queryString = "SELECT SUM(onsite_transaction_table.total_sale) as productRevenue," +
                    "onsite_products_transaction_table.transaction_id, " +
                    "SUM(onsite_products_transaction_table.total_product_count) as quantitySold," +
                    "onsite_products_transaction_table.product_id FROM onsite_transaction_table INNER JOIN onsite_products_transaction_table ON onsite_transaction_table.transaction_id = onsite_products_transaction_table.transaction_id WHERE onsite_products_transaction_table.product_id=@productID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@productID", productID);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

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
        public async Task<List<QuantitySoldModel>> FetchQuantitySoldList(int productID)
        {
            List<QuantitySoldModel> quantitySoldModels = new List<QuantitySoldModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT products_table.product_name as ProductName, " +
                    "products_table.product_price as ProductPrice," +
                    "products_table.product_id as ProductID, " +
                    "sales_table.sale_type as SaleType, sales_table.date as Date," +
                    "sales_table.user_username as Administrator, " +
                    "onsite_transaction_table.total_sale as TotalSale, " +
                    "onsite_products_transaction_table.total_product_count as ProductCount " +
                    "FROM onsite_transaction_table INNER JOIN onsite_products_transaction_table " +
                    "INNER JOIN sales_table " +
                    "INNER JOIN products_table " +
                    "ON onsite_transaction_table.transaction_id = onsite_products_transaction_table.transaction_id " +
                    "AND onsite_transaction_table.transaction_id = sales_table.onsite_transaction_id " +
                    "WHERE onsite_products_transaction_table.product_id = @productID AND products_table.product_id = @productID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@productID", productID);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while(await reader.ReadAsync())
                {
                    quantitySoldModels.Add(
                            new QuantitySoldModel
                            {
                                ProductName = reader["ProductName"].ToString(),
                                ProductPrice = int.Parse(reader["ProductPrice"].ToString()),
                                ProductID = int.Parse(reader["ProductID"].ToString()),
                                Date = DateTime.Parse(reader["Date"].ToString()),
                                Administrator = reader["Administrator"].ToString(),
                                TotalSale = int.Parse(reader["TotalSale"].ToString()),
                                SaleType = reader["SaleType"].ToString(),
                                ProductCount = int.Parse(reader["ProductCount"].ToString())
                            }
                        );
                }
                return quantitySoldModels;
            }
        }
    }
}