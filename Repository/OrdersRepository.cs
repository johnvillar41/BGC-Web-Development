using MySql.Data.MySqlClient;
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
                                CustomerName = reader["customer_name"].ToString(),
                                CustomerEmail = reader["customer_email"].ToString(),
                                OrderTotalPrice = int.Parse(reader["order_total_price"].ToString()),
                                OrderStatus = reader["order_status"].ToString(),
                                OrderDate = reader["order_date"].ToString(),
                                TotalNumberOfOrders = int.Parse(reader["total_number_of_orders"].ToString())
                            }
                        );
                }
            }
            return ordersList;
        }


    }
}