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
                while (await reader.ReadAsync())
                {
                    string base64String = Convert.ToBase64String((byte[])(reader["product_picture"]));
                    productList.Add(
                            new ProductModel()
                            {
                                Product_ID = int.Parse(reader["product_id"].ToString()),
                                ProductName = reader["product_name"].ToString(),
                                ProductDescription = reader["product_description"].ToString(),
                                ProductPicture = base64String,
                                ProductStocks = int.Parse(reader["product_stocks"].ToString()),
                                ProductCategory = reader["product_category"].ToString(),
                                ProductPrice = int.Parse(reader["product_price"].ToString())
                            }
                        );
                }
            }
            return productList;
        }

        // / Search Product
        // / Dropdown text updates
        // / Update dropdown visuals to have arrow
        // / View Product Details
        // / Delete Product
        // / Responsiveness
        // x Inventory: Fix code so that only the first repeater gets updated by the dropdown list,
        //   since the Lottie animations disappear right now when dropdown is click
        // x Add settings to first repeater, orders products by name, ID, number of stocks, etc.
        // x Fix code to properly display images

        /*
        NOTE: For card format: product pictures tend to have an approx. 7:10 aspect ratio.
        Pictures will be placed to the left, with the other details on the right.
        */

        // x Finalize Format (modal size, text position, modal elements)
        // x Fix resetting of position of repeaters when details modal loads
        // x Fix Lottie animations for situations of no products being found (animations not working in mobile)
        // x test vertical scrolling in modal collapsible cards (in case of long descriptions)
        // x Have dropdown menu on site.master align right when screen is small
        // x Change images being squished in cards
        // / Have card displays have equal size during display (maybe remove "descriptions" as the displayed detail?)

        // x Add Product
        // x Update product


        public async Task<List<ProductModel>> FetchGHProducts()
        {
            List<ProductModel> productList = new List<ProductModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM products_table WHERE product_category='Greenhouse'";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
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

        public async Task<List<ProductModel>> FetchHPProducts()
        {
            List<ProductModel> productList = new List<ProductModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM products_table WHERE product_category='Hydroponics'";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
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

        public async Task<List<ProductModel>> FetchAllCategories()
        {
            List<ProductModel> productList = new List<ProductModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT DISTINCT product_category FROM products_table ORDER BY product_category";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    productList.Add(
                            new ProductModel()
                            {
                                ProductCategory = reader["product_category"].ToString(),
                            }
                        );
                }
            }
            return productList;
        }

        public async Task<List<ProductModel>> FetchOnCategory(string category)
        {
            List<ProductModel> productList = new List<ProductModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM products_table WHERE product_category='" + category + "'";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    string base64String = Convert.ToBase64String((byte[])(reader["product_picture"]));
                    productList.Add(
                            new ProductModel()
                            {
                                Product_ID = int.Parse(reader["product_id"].ToString()),
                                ProductName = reader["product_name"].ToString(),
                                ProductDescription = reader["product_description"].ToString(),
                                ProductPicture = base64String,
                                ProductStocks = int.Parse(reader["product_stocks"].ToString()),
                                ProductCategory = reader["product_category"].ToString(),
                                ProductPrice = int.Parse(reader["product_price"].ToString())
                            }
                        );
                }
            }
            return productList;
        }

        public async Task<List<ProductModel>> FetchOnSearch(string search)
        {
            List<ProductModel> productList = new List<ProductModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM products_table WHERE product_name LIKE '" + search + "%'";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    string base64String = Convert.ToBase64String((byte[])(reader["product_picture"]));
                    productList.Add(
                            new ProductModel()
                            {
                                Product_ID = int.Parse(reader["product_id"].ToString()),
                                ProductName = reader["product_name"].ToString(),
                                ProductDescription = reader["product_description"].ToString(),
                                ProductPicture = base64String,
                                ProductStocks = int.Parse(reader["product_stocks"].ToString()),
                                ProductCategory = reader["product_category"].ToString(),
                                ProductPrice = int.Parse(reader["product_price"].ToString())
                            }
                        );
                }
            }
            return productList;
        }

        public async Task<ProductModel> FetchProductDetails(string productID)
        {
            ProductModel productModel = null;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM products_table WHERE product_id='" + productID + "%'";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    string base64String = Convert.ToBase64String((byte[])(reader["product_picture"]));

                    productModel = new ProductModel()
                    {
                        Product_ID = int.Parse(reader["product_id"].ToString()),
                        ProductName = reader["product_name"].ToString(),
                        ProductDescription = reader["product_description"].ToString(),
                        ProductPicture = base64String,
                        ProductStocks = int.Parse(reader["product_stocks"].ToString()),
                        ProductCategory = reader["product_category"].ToString(),
                        ProductPrice = int.Parse(reader["product_price"].ToString())
                    };
                }
            }
            return productModel;
        }

        public async void DeleteProduct(string productID)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "DELETE FROM products_table WHERE product_id='" + productID + "'";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.ExecuteNonQuery();
            }
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

                    string base64String = Convert.ToBase64String((byte[])(reader["product_picture"]));
                    productModel = new ProductModel()
                    {
                        Product_ID = int.Parse(reader["product_id"].ToString()),
                        ProductName = reader["product_name"].ToString(),
                        ProductDescription = reader["product_description"].ToString(),
                        ProductPicture = base64String,
                        ProductStocks = int.Parse(reader["product_stocks"].ToString()),
                        ProductCategory = reader["product_category"].ToString(),
                        ProductPrice = int.Parse(reader["product_price"].ToString())
                    };
                }
            }
            return productModel;
        }
        public async Task UpdateProductStocks(int stockSold, int productID)
        {
            bool isOk = false;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryCheckProduct = "SELECT product_stocks FROM products_table WHERE product_id=@productID";
                MySqlCommand commandCheck = new MySqlCommand(queryCheckProduct, connection);
                commandCheck.Parameters.AddWithValue("@productID", productID);
                MySqlDataReader reader = (MySqlDataReader)await commandCheck.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    if (stockSold < int.Parse(reader["product_stocks"].ToString()))
                    {
                        isOk = true;
                    }
                }
                reader.Close();
                if (isOk)
                {
                    string queryString = "UPDATE products_table SET product_stocks = (product_stocks - @stockSold) WHERE product_id=@productID";
                    MySqlCommand command = new MySqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@stockSold", stockSold);
                    command.Parameters.AddWithValue("@productID", productID);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}