using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoftEngWebEmployee.Repository
{
    public class InformationRepository
    {
        private static InformationRepository instance = null;
        private InformationRepository()
        {

        }
        public static InformationRepository SingleInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InformationRepository();
                }
                return instance;
            }            
        }
        public async Task UpdateInformationAsync(InformationModel information)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "UPDATE information_table SET product_information=@productInfo WHERE product_id=@productID";
                MySqlCommand command = new MySqlCommand(queryString, connection);                
                command.Parameters.AddWithValue("@productInfo", information.ProductInformation);
                command.Parameters.AddWithValue("@productID", information.Product.Product_ID);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<InformationModel> FetchInformationAsync(int id)
        {
            InformationModel information = null;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM information_table WHERE product_id = @id";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    information = new InformationModel() 
                    { 
                        ProductInformation = reader["product_information"].ToString() 
                    };
                }
            }
            return information;
        }
        public async Task<List<InformationModel>> FetchInformationsAsync()
        {
            List<InformationModel> informations = new List<InformationModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM information_table";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    informations.Add(
                            new InformationModel()
                            {
                                Product = await ProductRepository.SingleInstance.GetProductsAsync(int.Parse(reader["product_id"].ToString())),
                                ProductInformation = reader["product_information"].ToString()
                            }
                        );
                }
            }
            return informations;
        }
    }
}