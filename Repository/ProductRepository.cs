﻿using MySql.Data.MySqlClient;
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
        public static ProductRepository SingleInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductRepository();
                }
                return instance;
            }            
        }
        /// <summary>
        ///     Fetches all the products inside the database.
        /// </summary>
        /// <returns>
        ///     <para>Returns a list of all the products</para>
        ///     <para>Type: List<ProductModel></para>
        /// </returns>
        public async Task<List<ProductModel>> FetchAllProductsAsync()
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

        /// <summary>
        ///     Fetches all the products inside the database with a Greenhouse category
        /// </summary>
        /// <returns>
        ///     <para>Returns a list of products with greenhouse category</para>
        ///     <para>Type: List<ProductModel></para>
        /// </returns>
        public async Task<List<ProductModel>> FetchGHProductsAsync()
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
        /// <summary>
        ///     Fetches all the products inside the database with a Hydroponics category
        /// </summary>
        /// <returns>
        ///     <para>Returns a list of products with Hydroponics category</para>
        ///     <para>Type: List<ProductModel></para>
        /// </returns>
        public async Task<List<ProductModel>> FetchHPProductsAsync()
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
        /// <summary>
        ///     Fetches all the categories from the database
        /// </summary>
        /// <returns>
        ///     <para>Returns a list of categories</para>
        ///     <para>Type: string</para>
        /// </returns>        
        public async Task<List<string>> FetchAllCategoriesAsync()
        {
            List<string> categoryList = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT DISTINCT product_category FROM products_table ORDER BY product_category";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    categoryList.Add(reader["product_category"].ToString());
                }
            }
            return categoryList;
        }
        /// <summary>
        ///     Fecthes all the products based on the given paramref name="category"
        /// </summary>
        /// <param name="category">
        ///     Passes a category string value
        /// </param>
        /// <returns>
        ///     <para>Returns a list of products with the category selected</para>
        ///     <para>Type: List<ProductModel></para>
        /// </returns>
        public async Task<List<ProductModel>> FetchOnCategoryAsync(string category)
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
        /// <summary>
        ///     Searches products from the database with the similar searched paramref name="search"
        /// </summary>
        /// <param name="search">
        ///     Passes a product paramref name="search"
        /// </param>
        /// <returns>
        ///     <para>Returns a list of products that are similar to the searched name</para>
        ///     <para>Type: List<ProductModel></para>
        /// </returns>
        public async Task<List<ProductModel>> FetchOnSearchAsync(string search)
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
        /// <summary>
        ///     Fetches the product details of a certain product given a product id
        /// </summary>
        /// <param name="productID">
        ///     Passes a product id as parameters
        /// </param>
        /// <returns>
        ///     <para>Returns product details for the product</para>
        ///     <para>Type: ProductModel</para>
        /// </returns>
        public async Task<ProductModel> FetchProductDetailsAsync(string productID)
        {
            ProductModel productModel = null;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM products_table WHERE product_id='" + productID + "'";
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
        /// <summary>
        ///     Deletes a product inside the database given a product id
        /// </summary>
        /// <param name="productID">
        ///     Passes a product id as parameter
        /// </param>
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
        /// <summary>
        ///     Fetches a product for a given product id
        /// </summary>
        /// <param name="productID">
        ///     Passes a product is as parameter
        /// </param>
        public async Task<ProductModel> GetProductsAsync(int productID)
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
        /// <summary>
        ///     This will update the product stocks given the stocks to be subtracted and the product id
        /// </summary>
        /// <param name="stockSold">
        ///     Number of stocks to be subtracted to a certain product inside the database
        /// </param>
        /// <param name="productID">
        ///     The product id that should have their product stocks subtracted 
        /// </param>
        public async Task UpdateProductStocksAsync(int stockSold, int productID)
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