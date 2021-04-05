using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoftEngWebEmployee.Repository
{
    public class SpecificOrdersRepository
    {
        private static SpecificOrdersRepository instance = null;
        private SpecificOrdersRepository()
        {

        }

        public static SpecificOrdersRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new SpecificOrdersRepository();
            }
            return instance;
        }

        public async Task<List<SpecificOrdersModel>> FetchSpecificOrders(int orderID)
        {
            List<SpecificOrdersModel> specificOrdersModels = new List<SpecificOrdersModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM specific_orders_table WHERE order_id=@orderID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@orderID", orderID);
                MySqlDataReader reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    SpecificOrdersModel specificOrdersModel = new SpecificOrdersModel()
                    {
                        SpecificOrdersId = int.Parse(reader["specific_orders_id"].ToString()),
                        OrdersID = int.Parse(reader["order_id"].ToString()),
                        ProductID = int.Parse(reader["product_id"].ToString()),
                        TotalOrders = int.Parse(reader["total_orders"].ToString()),
                        ProductsModel = await ProductRepository.GetInstance().GetProducts(int.Parse(reader["product_id"].ToString()))
                    };
                    specificOrdersModels.Add(specificOrdersModel);
                }
            }
            return specificOrdersModels;
        }

    }
}
