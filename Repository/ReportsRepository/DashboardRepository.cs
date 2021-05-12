using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoftEngWebEmployee.Repository.ReportsRepository
{
    public class DashboardRepository
    {
        //fetch total sales
        //fetch total inventory
        //fetch total # products

        private static DashboardRepository instance = null;
        public static DashboardRepository SingleInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DashboardRepository();
                }                    
                return instance;
            }           
        }
        private DashboardRepository()
        {

        }


        public async Task<int> FetchTotalSales()
        {
            int total_sales = 0;
            int total_order_sale = 0;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT SUM(onsite_transaction_table.total_sale) as TotalSale FROM onsite_transaction_table";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    if (String.IsNullOrEmpty(reader["TotalSale"].ToString()))
                    {
                        total_sales = 0;
                    }
                    else
                    {
                        total_sales = int.Parse(reader["TotalSale"].ToString());
                    }                    
                }
            }
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT SUM(customer_orders_table.order_total_price) as TotalSaleOrder FROM customer_orders_table";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    if (String.IsNullOrEmpty(reader["TotalSaleOrder"].ToString()))
                    {
                        total_order_sale = 0;
                    }
                    else
                    {
                        total_order_sale = int.Parse(reader["TotalSaleOrder"].ToString());
                    }                    
                }
            }
            return total_sales + total_order_sale;
        }

        public async Task<int> FetchTotalInventory()
        {
            int total_inventory = 0;

            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT SUM(product_stocks) as total_inventory FROM products_table";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    if (String.IsNullOrEmpty(reader["total_inventory"].ToString()))
                    {
                        return 0;
                    }
                    total_inventory = int.Parse(reader["total_inventory"].ToString());
                }


            }

            return total_inventory;
        }

        public async Task<int> FetchTotalProducts()
        {
            int total_products = 0;

            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT COUNT(product_id) as products FROM products_table";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    if (String.IsNullOrEmpty(reader["products"].ToString()))
                    {
                        return 0;
                    }
                    total_products = int.Parse(reader["products"].ToString());
                }

            }

            return total_products;
        }



    }
}