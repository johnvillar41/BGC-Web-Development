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
        public static ProductSalesReportRepository SingleInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductSalesReportRepository();
                }
                return instance;
            }           
        }
        private ProductSalesReportRepository()
        {

        }
        /// <summary>
        ///     Calculates and Fetches the total number
        ///     of sales made in a certain product
        /// </summary>
        /// <param name="productID">
        ///     Passes a product id to be searched in 
        ///     the database
        /// </param>
        /// <returns>
        ///     <para>Returns the total amount os sales for the certain product</para>
        ///     <para>Type: ProductSalesReportModel</para>
        /// </returns>
        public async Task<ProductSalesReportModel> FetchProductSalesReportAsync(int productID)
        {
            ProductSalesReportModel ProductSalesReportModel = null;

            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT onsite_products_transaction_table.total_product_count as ProductCount, " +
                    "(SELECT products_table.product_price FROM products_table WHERE products_table.product_id = @productID) as ProductPrice, " +
                    "SUM(onsite_products_transaction_table.total_product_count) as quantitySold, " +
                    "SUM(onsite_products_transaction_table.total_product_count * (SELECT products_table.product_price FROM products_table WHERE products_table.product_id = @productID)) AS Total " +
                    "FROM onsite_transaction_table INNER JOIN onsite_products_transaction_table ON onsite_transaction_table.transaction_id = onsite_products_transaction_table.transaction_id WHERE onsite_products_transaction_table.product_id = @productID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@productID", productID);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    ProductSalesReportModel = new ProductSalesReportModel();
                    ProductSalesReportModel.Product = await ProductRepository.SingleInstance.FetchProductDetailsAsync(productID.ToString());
                    if (reader["quantitySold"] != DBNull.Value)
                    {
                        ProductSalesReportModel.ProductRevenue = int.Parse(reader["quantitySold"].ToString()) * ProductSalesReportModel.Product.ProductPrice;
                        ProductSalesReportModel.QuantitySold = int.Parse(reader["quantitySold"].ToString());
                    }
                    else
                    {
                        ProductSalesReportModel.ProductRevenue = 0;
                        ProductSalesReportModel.QuantitySold = 0;                        
                    }
                }
                return ProductSalesReportModel;
            }
        }
        /// <summary>
        ///     Fetches a list of products transactions sold.
        /// </summary>
        /// <param name="productID">
        ///     Passes a productID as a parameter
        /// </param>
        /// <returns>
        ///     <para>Returs a list of all the products that are sold</para>
        ///     <para>Type: List<QuantitySoldModel></para>
        /// </returns>
        public async Task<List<QuantitySoldModel>> FetchQuantitySoldListAsync(int productID)
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
                while (await reader.ReadAsync())
                {
                    quantitySoldModels.Add(
                            new QuantitySoldModel
                            {
                                ProductName = reader["ProductName"].ToString(),
                                ProductPrice = int.Parse(reader["ProductPrice"].ToString()),
                                ProductID = int.Parse(reader["ProductID"].ToString()),
                                Date = DateTime.Parse(reader["Date"].ToString()),
                                Administrator = reader["Administrator"].ToString(),
                                TotalSale = int.Parse(reader["ProductCount"].ToString()) * int.Parse(reader["ProductPrice"].ToString()),
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