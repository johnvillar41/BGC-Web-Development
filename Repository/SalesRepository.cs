using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
                string queryString = "INSERT INTO sales_table(sales_title," +                    
                    "sales_transaction_value," +                    
                    "total_number_of_products," +
                    "sales_date," +
                    "date_month," +
                    "user_username," +
                    "sale_type)" +
                    "VALUES(@salesTitle,@SalesTransaction,@TotalNumberProducts,@SalesDate,@DateMonth,@Username,@SaleType)";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@salesTitle",newSale.SalesTitle);                
                command.Parameters.AddWithValue("@SalesTransaction",newSale.SalesTransactionValue);               
                command.Parameters.AddWithValue("@TotalNumberProducts",newSale.TotalNumberOfProducts);
                command.Parameters.AddWithValue("@SalesDate",newSale.SalesDate);
                command.Parameters.AddWithValue("@DateMonth",newSale.DateMonth);
                command.Parameters.AddWithValue("@Username",newSale.Administrator.Username);
                command.Parameters.AddWithValue("@SaleType",newSale.TypeOfSale);
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
                    listOfSales.Add(
                            new SalesModel()
                            {
                                Sales_ID = int.Parse(reader["sales_id"].ToString()),
                                SalesTitle = reader["sales_title"].ToString(),                               
                                SalesTransactionValue = double.Parse(reader["sales_transaction_value"].ToString()),                                
                                TotalNumberOfProducts = int.Parse(reader["total_number_of_products"].ToString()),
                                SalesDate = DateTime.Parse(reader["sales_date"].ToString()),
                                DateMonth = int.Parse(reader["date_month"].ToString()),
                                Administrator = await AdministratorRepository.GetInstance().FindAdministrator(reader["user_username"].ToString()),
                                TypeOfSale = GenerateSaleType(reader["sale_type"].ToString())
                            }
                        );
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