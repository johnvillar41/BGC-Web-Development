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
        /// <summary>
        ///     This function checks if the order ID exists
        /// </summary>
        /// <param name="orderID">
        ///     Passes order ID as a parameter
        /// </param>
        /// <returns>
        ///     <para>Returns if the order ID exists or not</para>
        ///     <para>Type: Bool</para>
        /// </returns>
        public async Task<bool> CheckIfIdExistAsync(int orderID)
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
        /// <summary>
        ///     This function retrieves the customer orders
        /// </summary>
        /// <param name="orderId">
        ///     Passes order ID as a parameter
        /// </param>
        /// <returns>
        ///     <para>Returns customer orders</para>
        ///     <para>Type: OrdersModel</para>
        /// </returns>
        public async Task<OrdersModel> FetchOrderAsync(int orderId)
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
                        SpecificOrdersModel = await SpecificOrdersRepository.SingleInstance.FetchSpecificOrdersAsync(int.Parse(reader["order_id"].ToString()))
                    };
                }
            }
            return order;
        }
        /// <summary>
        ///     This function retrieves all customer orders and displays it
        /// </summary>
        /// <returns>
        ///     <para>Returns all customers orders</para>
        ///     <para>Type: OrdersModel</para>
        /// </returns>
        public async Task<IEnumerable<OrdersModel>> FetchAllOrdersAsync()
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
                                SpecificOrdersModel = await SpecificOrdersRepository.SingleInstance.FetchSpecificOrdersAsync(int.Parse(reader["order_id"].ToString()))
                            }
                        );
                }
            }
            return ordersList;
        }
        /// <summary>
        ///     This function changes the status of customer order to Cancelled
        /// </summary>
        /// <param name="orderID">
        ///     Passes order ID as a parameter
        /// </param>

        public async Task ChangeStatusOfOrderToCancelledAsync(int orderID)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                var isOk = await CheckOrderStatusAsync(connection);
                if (isOk)
                {
                    string queryString = "UPDATE customer_orders_table SET order_status='Cancelled' WHERE order_id=@orderID";
                    MySqlCommand command = new MySqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@orderID", orderID);
                    await command.ExecuteNonQueryAsync();
                }                
            }
        }
        /// <summary>
        ///     This function changes the status of customer order to Finished
        /// </summary>
        /// <param name="orderID">
        ///     Passes order ID as a parameter
        /// </param>

        public async Task ChangeStatusOfOrderToFinishedAsync(int orderID)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                
                await connection.OpenAsync();
                var isOk = await CheckOrderStatusAsync(connection);
                if (isOk)
                {
                    string queryString = "UPDATE customer_orders_table SET order_status='Finished' WHERE order_id=@orderID";
                    MySqlCommand command = new MySqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@orderID", orderID);
                    await command.ExecuteNonQueryAsync();
                }                
            }
        }

        /// <summary>
        ///     This function calculates the total sales orders
        /// </summary>
        /// <param name="orderID">
        ///     Passes order ID as a parameter
        /// </param>
        /// <returns>
        /// <para>Returns the totalOrderSale</para>
        /// <para>Type: Int</para>
        /// </returns>
        public async Task<int> CalculateTotalSaleOrderAsync(int orderID)
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
        
        /// <summary>
        ///     This function checks the status of customer orders
        /// </summary>
        /// <param name="connection">
        ///     Passes connection as a parameter
        /// </param>
        /// <returns>
        /// <para>Returns the status of customer orders</para>
        /// <para>Type: Bool</para>
        /// </returns>
        private async Task<bool> CheckOrderStatusAsync(MySqlConnection connection)
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