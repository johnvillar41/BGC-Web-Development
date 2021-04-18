﻿using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Repository
{
    public class ReportsRepository
    {
        //fetch total sales
        //fetch total inventory
        //fetch total # products


        public int FetchTotalSales()
        {
            int total_sales=0;
            int total_sales2 = 0;

            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                connection.Open();
                string queryString = "SELECT SUM(order_total_price) as total_sales FROM customer_orders_table";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    total_sales = int.Parse(reader["total_sales"].ToString());
                }

            }

            using (MySqlConnection connection2 = new MySqlConnection(DbConnString.DBCONN_STRING))

                
            {
                connection2.Open();
                string queryString2 = "SELECT SUM(total_sale) as total_sales2 FROM onsite_transaction_table";
                MySqlCommand command2 = new MySqlCommand(queryString2, connection2);
                MySqlDataReader reader2 = command2.ExecuteReader();

                if (reader2.Read())
                {
                    total_sales2 = int.Parse(reader2["total_sales2"].ToString());
                }

            }
            return total_sales + total_sales2;

        }

        public int FetchTotalInventory()    
        {
            int total_inventory=0;

            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                connection.Open();
                string queryString = "SELECT SUM(product_stocks) as total_inventory FROM products_table";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    total_inventory = int.Parse(reader["total_inventory"].ToString());
                }


            }

            return total_inventory;
        }

        public int FetchTotalProducts()
        {
            int total_products=0;

            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                connection.Open();
                string queryString = "SELECT COUNT(product_id) as products FROM products_table";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    total_products = int.Parse(reader["products"].ToString());
                }

                

            }

            return total_products;
        }

    }
}