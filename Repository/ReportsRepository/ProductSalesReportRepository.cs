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
                string queryString = "SELECT SUM(specific_orders_table.total_orders) as OrderTotal, SUM(onsite_products_transaction_table.total_product_count) as OnsiteTotal FROM specific_orders_table LEFT JOIN onsite_products_transaction_table ON specific_orders_table.product_id = onsite_products_transaction_table.product_id WHERE specific_orders_table.product_id = @productID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@productID", productID);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    int quantitySoldOrder = 0;
                    int quantitySoldOnsite = 0;
                    int total = 0;
                    ProductSalesReportModel = new ProductSalesReportModel();
                    ProductSalesReportModel.Product = await ProductRepository.SingleInstance.FetchProductDetailsAsync(productID.ToString());
                    if (reader["OrderTotal"] != DBNull.Value)
                    {                        
                        quantitySoldOrder = int.Parse(reader["OrderTotal"].ToString()) * ProductSalesReportModel.Product.ProductPrice;
                        int order = int.Parse(reader["OrderTotal"].ToString());
                        total += order;                        
                    }
                    if(reader["OnsiteTotal"] != DBNull.Value)
                    {
                        quantitySoldOnsite = int.Parse(reader["OnsiteTotal"].ToString()) * ProductSalesReportModel.Product.ProductPrice;
                        int onsite = int.Parse(reader["OnsiteTotal"].ToString());
                        total += onsite;
                    }
                    if(reader["OrderTotal"] == DBNull.Value && reader["OnsiteTotal"] == DBNull.Value)
                    {
                        ProductSalesReportModel.ProductRevenue = 0;
                        ProductSalesReportModel.QuantitySold = 0;
                        return ProductSalesReportModel;
                    }
                    ProductSalesReportModel.ProductRevenue = (int)quantitySoldOnsite + (int)quantitySoldOrder;
                    ProductSalesReportModel.QuantitySold = total;
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
        public async Task<List<QuantitySoldModel>> FetchQuantitySoldOnsiteAsync(int productID)
        {
            List<QuantitySoldModel> quantitySoldModels = new List<QuantitySoldModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT products_table.product_name as ProductName, " +
                "products_table.product_price as ProductPrice, " +
                "products_table.product_id as ProductID, " +
                "sales_table.sale_type as SaleType, " +
                "sales_table.date as Date, " +
                "sales_table.user_username as Administrator, " +
                "SUM(onsite_products_transaction_table.subtotal_price) as TotalSaleOnsite, " +
                "onsite_products_transaction_table.total_product_count as ProductCount " +
                "FROM products_table " +
                "INNER JOIN onsite_products_transaction_table ON onsite_products_transaction_table.product_id = products_table.product_id " +
                "INNER JOIN sales_table WHERE products_table.product_id = @productID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@productID", productID);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var quantitySoldModel = new QuantitySoldModel();
                    quantitySoldModel.ProductName = null;
                    quantitySoldModel.ProductPrice = 0;
                    quantitySoldModel.ProductID = -1;
                    quantitySoldModel.Date = null;
                    quantitySoldModel.Administrator = "Empty";
                    quantitySoldModel.TotalSale = 0;
                    quantitySoldModel.SaleType = "Empty";
                    quantitySoldModel.ProductCount = 0;

                    if (reader["ProductName"] != DBNull.Value)
                        quantitySoldModel.ProductName = reader["ProductName"].ToString();
                    if (reader["ProductPrice"] != DBNull.Value)
                        quantitySoldModel.ProductPrice = int.Parse(reader["ProductPrice"].ToString());
                    if (reader["ProductID"] != DBNull.Value)
                        quantitySoldModel.ProductID = int.Parse(reader["ProductID"].ToString());
                    if (reader["Date"] != DBNull.Value)
                        quantitySoldModel.Date = DateTime.Parse(reader["Date"].ToString());
                    if (reader["Administrator"] != DBNull.Value)
                        quantitySoldModel.Administrator = reader["Administrator"].ToString();
                    if (reader["TotalSaleOnsite"] != DBNull.Value)
                        quantitySoldModel.TotalSale = int.Parse(reader["TotalSaleOnsite"].ToString());
                    if (reader["SaleType"] != DBNull.Value)
                        quantitySoldModel.SaleType = reader["SaleType"].ToString();
                    if (reader["ProductCount"] != DBNull.Value)
                        quantitySoldModel.ProductCount = int.Parse(reader["ProductCount"].ToString());
                    quantitySoldModels.Add(quantitySoldModel);
                }
                return quantitySoldModels;
            }
        }
        public async Task<List<QuantitySoldModel>> FetchQuantitySoldOrderAsync(int productID)
        {
            List<QuantitySoldModel> quantitySoldModels = new List<QuantitySoldModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT products_table.product_name as ProductName," +
                "products_table.product_price as ProductPrice," +
                "products_table.product_id as ProductID," +
                "sales_table.sale_type as SaleType," +
                "sales_table.date as Date," +
                "sales_table.user_username as Administrator," +
                "SUM(specific_orders_table.subtotal_price) as TotalSaleOrder," +
                "specific_orders_table.total_orders as ProductCount " +
                "FROM products_table  " +
                "INNER JOIN specific_orders_table ON specific_orders_table.product_id = products_table.product_id " +
                "INNER JOIN sales_table WHERE products_table.product_id = @productID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@productID", productID);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var quantitySoldModel = new QuantitySoldModel();
                    quantitySoldModel.ProductName = null;
                    quantitySoldModel.ProductPrice = 0;
                    quantitySoldModel.ProductID = -1;
                    quantitySoldModel.Date = null;
                    quantitySoldModel.Administrator = "Empty";
                    quantitySoldModel.TotalSale = 0;
                    quantitySoldModel.SaleType = "Empty";
                    quantitySoldModel.ProductCount = 0;

                    if (reader["ProductName"] != DBNull.Value)
                        quantitySoldModel.ProductName = reader["ProductName"].ToString();
                    if (reader["ProductPrice"] != DBNull.Value)
                        quantitySoldModel.ProductPrice = int.Parse(reader["ProductPrice"].ToString());
                    if (reader["ProductID"] != DBNull.Value)
                        quantitySoldModel.ProductID = int.Parse(reader["ProductID"].ToString());
                    if (reader["Date"] != DBNull.Value)
                        quantitySoldModel.Date = DateTime.Parse(reader["Date"].ToString());
                    if (reader["Administrator"] != DBNull.Value)
                        quantitySoldModel.Administrator = reader["Administrator"].ToString();
                    if (reader["TotalSaleOrder"] != DBNull.Value)
                        quantitySoldModel.TotalSale = int.Parse(reader["TotalSaleOrder"].ToString());
                    if (reader["SaleType"] != DBNull.Value)
                        quantitySoldModel.SaleType = reader["SaleType"].ToString();
                    if (reader["ProductCount"] != DBNull.Value)
                        quantitySoldModel.ProductCount = int.Parse(reader["ProductCount"].ToString());
                    quantitySoldModels.Add(quantitySoldModel);
                }
                return quantitySoldModels;
            }
        }
    }
}