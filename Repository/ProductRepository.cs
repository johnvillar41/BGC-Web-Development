using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoftEngWebEmployee.Repository
{
    public class ProductRepository
    {
        private static ProductRepository instance = null;
        private ProductRepository()
        {

        }
        public static ProductRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductRepository();
            }
            return instance;
        }

        public async Task<List<ProductModel>> FetchAllProducts()
        {
            List<ProductModel> productList = new List<ProductModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM products_table";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while(await reader.ReadAsync())
                {                    
                    productList.Add(
                            new ProductModel()
                            {
                                Product_ID = int.Parse(reader["product_id"].ToString()),
                                ProductName = reader["product_name"].ToString(),
                                ProductDescription = reader["product_description"].ToString(),
                                ProductPicture = reader["product_picture"].ToString(),
                                ProductStocks = int.Parse(reader["product_stocks"].ToString()),
                                ProductCategory = reader["product_category"].ToString(),
                                ProductPrice = int.Parse(reader["product_price"].ToString())
                            }
                        );
                }
            }
            return productList;
        }

        public async Task<ProductModel> GetProducts(int productID)
        {
            ProductModel productModel = null;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM products_table WHERE product_id=@productID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@productID", productID);
                MySqlDataReader reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    productModel = new ProductModel()
                    {
                        Product_ID = int.Parse(reader["product_id"].ToString()),
                        ProductName = reader["product_name"].ToString(),
                        ProductDescription = reader["product_description"].ToString(),
                        ProductPicture = reader["product_picture"].ToString(),
                        ProductStocks = int.Parse(reader["product_stocks"].ToString()),
                        ProductCategory = reader["product_category"].ToString(),
                        ProductPrice = int.Parse(reader["product_price"].ToString())
                    };
                }
            }
            return productModel;
        }
    }
}