using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Repository
{
    public class AdministratorRepository
    {
        private static AdministratorRepository instance = null;
        public static AdministratorRepository SingleInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdministratorRepository();
                }
                return instance;
            }            
        }

        private AdministratorRepository()
        {

        }
        /// <summary>
        ///     This functions checks whether the logged in user is an administrator
        /// </summary>
        /// <param name="username">
        ///     Passes a specific username to be checked
        /// </param>
        /// <returns>
        ///     <para>Returns whether the username is an administrator or an employee</para>
        ///     <para>Type: bool</para>
        /// </returns>
        public async Task<bool> CheckIfUserIsAdministratorAsync(string username)
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
        /// <summary>
        ///     This function will retrieve the details of a specific administrator
        /// </summary>
        /// <param name="administratorID">
        ///     Passes a string value of the username for the administrator
        /// </param>
        /// <returns>
        ///     <para>Returns Administrator Details</para>
        ///     <para>Type: AdministratorModel</para>
        /// </returns>
        public async Task<AdministratorModel> FindAdministratorAsync(int administratorID)
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
        /// <summary>
        ///     This function will retrieve the details of a specific administrator
        /// </summary>
        /// <param name="username">
        ///     Passes a string value of the username for the administrator
        /// </param>
        /// <returns>
        ///     <para>Returns Administrator Details</para>
        ///     <para>Type: AdministratorModel</para>
        /// </returns>
        public async Task<AdministratorModel> FindAdministratorAsync(string username)
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

        /// <summary>
        ///     This function will retrieve all the administrators and employees
        /// </summary>
        /// <returns>
        ///     <para>Returns Administrators and Employees Details</para>
        ///     <para>Type: Administratot Model</para>
        /// </returns>
        public async Task<IEnumerable<AdministratorModel>> FetchAdministratorsAsync()
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
        /// <summary>
        ///     Deletes an administrator or an employee given an id
        /// </summary>
        /// <param name="administratorID">
        ///     Passes an administrator id as parameter
        /// </param>
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

        /// <summary>
        ///     This function creates a new administrator
        /// </summary>
        /// <param name="administrator">
        ///     Passes an administrator as parameter
        /// </param>
        public async void CreateNewAdministrator(AdministratorModel administrator)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "INSERT INTO login_table(user_username,user_password,user_name,position,user_image)" +
                    "VALUES(@Username,@Passowrd,@name,@position,@user_image)";
                MySqlCommand command = new MySqlCommand(queryString, connection);                
                command.Parameters.AddWithValue("@Username", administrator.Username);
                command.Parameters.AddWithValue("@Passowrd", administrator.Password);
                command.Parameters.AddWithValue("@name", administrator.Fullname);                
                command.Parameters.AddWithValue("@position", administrator.EmployeeType.ToString());             
                command.Parameters.Add("@user_image", MySqlDbType.MediumBlob).Value = administrator.ProfilePictureUpload;                
               
                await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        ///     This function updates an administrator details
        /// </summary>
        /// <param name="administrator">
        ///     Passes an administrator parameter
        /// </param>
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