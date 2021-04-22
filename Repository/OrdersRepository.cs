﻿using MySql.Data.MySqlClient;
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

        public static OrdersRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new OrdersRepository();
            }
            return instance;
        }
        private OrdersRepository()
        {

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
                if(await reader.ReadAsync())
                {
                    order = new OrdersModel()
                    {
                        Order_ID = int.Parse(reader["order_id"].ToString()),
                        CustomerID = int.Parse(reader["user_id"].ToString()),
                        OrderTotalPrice = int.Parse(reader["order_total_price"].ToString()),
                        OrderStatus = reader["order_status"].ToString(),
                        OrderDate = reader["order_date"].ToString(),
                        TotalNumberOfOrders = int.Parse(reader["total_number_of_orders"].ToString()),
                        SpecificOrdersModel = await SpecificOrdersRepository.GetInstance().FetchSpecificOrders(int.Parse(reader["order_id"].ToString()))
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
                                SpecificOrdersModel = await SpecificOrdersRepository.GetInstance().FetchSpecificOrders(int.Parse(reader["order_id"].ToString()))
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
                string queryString = "UPDATE customer_orders_table SET order_status='Cancelled' WHERE order_id=@orderID";
                MySqlCommand command = new MySqlCommand(queryString,connection);
                command.Parameters.AddWithValue("@orderID", orderID);
                await command.ExecuteNonQueryAsync();                
            }
        }
        public async Task ChangeStatusOfOrderToFinished(int orderID)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "UPDATE customer_orders_table SET order_status='Finished',administrator_username=@administrator WHERE order_id=@orderID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@orderID", orderID);
                command.Parameters.AddWithValue("@administrator", UserSession.GetLoggedInUser());
                await command.ExecuteNonQueryAsync();
            }
        }

    }
}