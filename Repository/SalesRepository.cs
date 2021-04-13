using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Repository
{
    public class SalesRepository
    {
        private static SalesRepository instance = null;
        private SalesRepository()
        {

        }
        public static SalesRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new SalesRepository();
            }
            return instance;
        }

        public async Task InsertNewSale(SalesModel newSale)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "INSERT INTO sales_table(" +
                    "user_username," +                    
                    "sale_type," +                    
                    "date," +                   
                    "order_id)" +
                    "VALUES(@username,@saleType,@date,@orderID)";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username", newSale.Administrator.Username);                
                command.Parameters.AddWithValue("@saleType", newSale.SalesType.ToString());               
                command.Parameters.AddWithValue("@date", newSale.Date);
                command.Parameters.AddWithValue("@orderID", newSale.Orders.Order_ID);               
                await command.ExecuteReaderAsync();
            }
        }
        public async Task<List<SalesModel>> FetchAllSales()
        {
            List<SalesModel> listOfSales = new List<SalesModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM sales_table";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    SalesModel sales = new SalesModel()
                    {
                        SalesID = int.Parse(reader["sales_id"].ToString()),
                        Administrator = await AdministratorRepository.GetInstance().FindAdministrator(reader["user_username"].ToString()),
                        Date = DateTime.Parse(reader["date"].ToString()),
                        SalesType = GenerateSaleType(reader["sale_type"].ToString()),
                        Orders = await OrdersRepository.GetInstance().FetchOrder(int.Parse(reader["order_id"].ToString()))
                    };                    
                    listOfSales.Add(sales);
                }
            }
            return listOfSales;
        }
        private SalesType GenerateSaleType(string saleType)
        {
            switch (saleType)
            {
                case "Order":
                    return SalesType.Order;
                case "OnSite":
                    return SalesType.Onsite;
            }
            return SalesType.Onsite;
        }

    }
}