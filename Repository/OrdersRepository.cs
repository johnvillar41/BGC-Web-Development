using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SoftEngWebEmployee.Repository
{
    public class OrdersRepository
    {
        private static OrdersRepository instance = null;

        public static OrdersRepository SingleInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrdersRepository();
                }
                return instance;
            }            
        }
        private OrdersRepository()
        {

        }
        public async Task<bool> CheckIfIdExist(int orderID)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryCheckId = "SELECT order_id FROM customer_orders_table WHERE order_id=@orderID";
                MySqlCommand command = new MySqlCommand(queryCheckId,connection);
                command.Parameters.AddWithValue("@orderID",orderID);
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public async Task<OrdersModel> FetchOrder(int orderId)
        {
            OrdersModel order = null;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM customer_orders_table WHERE order_id=@orderID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@orderID", orderId);
                MySqlDataReader reader = command.ExecuteReader();
                if (await reader.ReadAsync())
                {
                    order = new OrdersModel()
                    {
                        Order_ID = int.Parse(reader["order_id"].ToString()),
                        CustomerID = int.Parse(reader["user_id"].ToString()),
                        OrderTotalPrice = int.Parse(reader["order_total_price"].ToString()),
                        OrderStatus = reader["order_status"].ToString(),
                        OrderDate = reader["order_date"].ToString(),
                        TotalNumberOfOrders = int.Parse(reader["total_number_of_orders"].ToString()),
                        SpecificOrdersModel = await SpecificOrdersRepository.SingleInstance.FetchSpecificOrders(int.Parse(reader["order_id"].ToString()))
                    };
                }
            }
            return order;
        }

        public async Task<IEnumerable<OrdersModel>> FetchAllOrders()
        {
            List<OrdersModel> ordersList = new List<OrdersModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM customer_orders_table";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    ordersList.Add(
                            new OrdersModel()
                            {
                                Order_ID = int.Parse(reader["order_id"].ToString()),
                                CustomerID = int.Parse(reader["user_id"].ToString()),
                                OrderTotalPrice = int.Parse(reader["order_total_price"].ToString()),
                                OrderStatus = reader["order_status"].ToString(),
                                OrderDate = reader["order_date"].ToString(),
                                TotalNumberOfOrders = int.Parse(reader["total_number_of_orders"].ToString()),
                                SpecificOrdersModel = await SpecificOrdersRepository.SingleInstance.FetchSpecificOrders(int.Parse(reader["order_id"].ToString()))
                            }
                        );
                }
            }
            return ordersList;
        }

        public async Task ChangeStatusOfOrderToCancelled(int orderID)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                var isOk = await CheckOrderStatus(connection);
                if (isOk)
                {
                    string queryString = "UPDATE customer_orders_table SET order_status='Cancelled' WHERE order_id=@orderID";
                    MySqlCommand command = new MySqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@orderID", orderID);
                    await command.ExecuteNonQueryAsync();
                }                
            }
        }
        public async Task ChangeStatusOfOrderToFinished(int orderID)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                
                await connection.OpenAsync();
                var isOk = await CheckOrderStatus(connection);
                if (isOk)
                {
                    string queryString = "UPDATE customer_orders_table SET order_status='Finished' WHERE order_id=@orderID";
                    MySqlCommand command = new MySqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@orderID", orderID);
                    await command.ExecuteNonQueryAsync();
                }                
            }
        }
        public async Task<int> CalculateTotalSaleOrder(int orderID)
        {
            int totalOrderSale = 0;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT SUM(order_total_price) as TotalSale FROM customer_orders_table WHERE order_id = @orderID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@orderID", orderID);
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        if(reader["TotalSale"] != DBNull.Value)
                        {
                            totalOrderSale = int.Parse(reader["TotalSale"].ToString());
                        }
                    }
                }
            }
            return totalOrderSale;
        }        
        private async Task<bool> CheckOrderStatus(MySqlConnection connection)
        {
            bool isOk = false;
            string queryCheck = "SELECT order_status FROM customer_orders_table";
            MySqlCommand commandCheck = new MySqlCommand(queryCheck, connection);
            using (MySqlDataReader reader = (MySqlDataReader)await commandCheck.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    var orderStatus = reader["order_status"].ToString();
                    if (!orderStatus.Equals("Finished") || !orderStatus.Equals("Cancelled"))
                    {
                        isOk = true;
                    }
                }
            }
            return isOk;
        }
    }
}