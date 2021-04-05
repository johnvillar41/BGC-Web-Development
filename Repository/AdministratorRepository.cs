using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoftEngWebEmployee.Repository.AdministratorRepository
{
    
    public class AdministratorRepository
    {
        private static AdministratorRepository instance = null;
        public static AdministratorRepository GetInstance()
        {
            if(instance == null)
            {
                instance = new AdministratorRepository();
            }
            return instance;
        }

        private AdministratorRepository()
        {

        }
        public async Task<IEnumerable<AdministratorModel>> FetchAdministrators()
        {
            List<AdministratorModel> Admins = new List<AdministratorModel>();
            AdministratorModel administrator;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM login_table";
                MySqlCommand command = new MySqlCommand(queryString,connection);
                MySqlDataReader reader = command.ExecuteReader();
                while(await reader.ReadAsync())
                {
                    Admins.Add(
                            administrator = new AdministratorModel()
                            {
                                User_ID = int.Parse(reader["user_id"].ToString()),
                                User_Username = reader["user_username"].ToString(),
                                User_Password = reader["user_password"].ToString(),
                                User_Name = reader["user_name"].ToString(),
                                User_Image = reader["user_image"].ToString()
                            }
                        );
                }
            }
            return Admins;
        }
    }
}