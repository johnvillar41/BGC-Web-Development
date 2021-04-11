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
        public InformationRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new InformationRepository();
            }
            return instance;
        }
        public async void UpdateInformation(InformationModel information)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "UPDATE information_table SET product_information=@productInfo";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue(queryString, information);
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<List<InformationModel>> FetchInformations()
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
                                Product = await ProductRepository.GetInstance().GetProducts(int.Parse(reader["product_id"].ToString())),
                                ProductInformation = reader["product_information"].ToString()
                            }
                        );
                }
            }
            return informations;
        }
    }
}