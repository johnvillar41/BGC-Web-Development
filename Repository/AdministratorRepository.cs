using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Repository
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
        public async Task<bool> CheckIfUserIsAdministrator(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT position FROM login_table WHERE user_username=@username";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username",username);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    if (reader["position"].Equals("Administrator"))
                        return true;
                }
            }
            return false;
        }
        public async Task<AdministratorModel> FindAdministrator(int administratorID)
        {
            AdministratorModel administrator = null;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM login_table WHERE user_id=@administratorID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@administratorID", administratorID);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if(await reader.ReadAsync())
                {
                    administrator = new AdministratorModel()
                    {                        
                        Username = reader["user_username"].ToString(),
                        Password = reader["user_password"].ToString(),
                        Fullname = reader["user_name"].ToString()                        
                    };
                }
            }
            return administrator;
        }
        public async Task<AdministratorModel> FindAdministrator(string username)
        {
            AdministratorModel administrator = null;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM login_table WHERE user_username=@username";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username", username);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    administrator = new AdministratorModel()
                    {
                        Username = reader["user_username"].ToString(),
                        Password = reader["user_password"].ToString(),
                        Fullname = reader["user_name"].ToString()
                    };
                }
            }
            return administrator;
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
                    string base64String = Convert.ToBase64String((byte[])(reader["user_image"]));
                    if (reader["position"].ToString().Equals("Employee"))
                    {
                        administrator = new AdministratorModel()
                        {
                            User_ID = int.Parse(reader["user_id"].ToString()),
                            Username = reader["user_username"].ToString(),
                            Password = reader["user_password"].ToString(),
                            Fullname = reader["user_name"].ToString(),
                            ProfilePicture = base64String
                        };
                        administrator.EmployeeType = EmployeeType.Employee;
                    }
                    else
                    {
                        administrator = new AdministratorModel()
                        {
                            User_ID = int.Parse(reader["user_id"].ToString()),
                            Username = reader["user_username"].ToString(),
                            Password = reader["user_password"].ToString(),
                            Fullname = reader["user_name"].ToString(),
                            ProfilePicture = base64String
                        };
                        administrator.EmployeeType = EmployeeType.Administrator;
                    }
                    Admins.Add(administrator);
                }
            }
            return Admins;
        }
        public async void DeleteAdministrator(int administratorID)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "DELETE FROM login_table WHERE user_id=@administratorID AND position='Employee'";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@administratorID",administratorID);
                await command.ExecuteNonQueryAsync();
            }
        }
        public async void CreateNewAdministrator(AdministratorModel administrator)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "INSERT INTO login_table(user_username,user_password,user_name,position)" +
                    "VALUES(@Username,@Passowrd,@name,@position)";
                MySqlCommand command = new MySqlCommand(queryString, connection);                
                command.Parameters.AddWithValue("@Username", administrator.Username);
                command.Parameters.AddWithValue("@Passowrd", administrator.Password);
                command.Parameters.AddWithValue("@name", administrator.Fullname);
                command.Parameters.AddWithValue("@position", administrator.EmployeeType);
                //command.Parameters.AddWithValue("@image", administrator.User_Image);
                await command.ExecuteNonQueryAsync();
            }
        }
        public async void UpdateAdministrator(AdministratorModel administrator)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "UPDATE login_table SET user_username=@username,user_password=@password,user_name=@fullname WHERE user_id=@userID";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@userID", administrator.User_ID);
                command.Parameters.AddWithValue("@username", administrator.Username);
                command.Parameters.AddWithValue("@password", administrator.Password);
                command.Parameters.AddWithValue("@fullname", administrator.Fullname);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}