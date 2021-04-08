﻿using MySql.Data.MySqlClient;
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

        public async Task<ProductsModel> GetProducts(int productID)
        {
            ProductsModel productModel = null;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM products_table WHERE product_id=@productID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@productID", productID);
                MySqlDataReader reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    productModel = new ProductsModel()
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