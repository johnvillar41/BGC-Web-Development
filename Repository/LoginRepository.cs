using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoftEngWebEmployee.Repository.LoginRepository
{
    public class LoginRepository
    {

        private static LoginRepository instance = null;

        public static LoginRepository SingleInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoginRepository();
                }
                return instance;
            }
        }

        private LoginRepository()
        {

        }
        public bool CheckIfUserExistInDB()
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                connection.Open();
                string queryString = "SELECT * FROM login_table WHERE user_username=@username";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username", UserSession.SingleInstance.GetLoggedInUser());
                using (MySqlDataReader reader = (command.ExecuteReader()))
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        ///     This function checks if the administrator was able to log in successfully
        /// </summary>
        /// <param name="adminModel">
        ///     Passes an adminModel parameter
        /// </param>
        /// <returns>
        ///     <para>Returns whether the log in is successful or not</para>
        ///     <para>Type: bool</para>
        /// </returns>
        public async Task<bool> IsLoginSuccessfullAsync(AdministratorModel adminModel)
        {
            bool isLoginSuccessfull = false;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string loginString = "SELECT * FROM login_table WHERE user_username LIKE BINARY @Username AND user_password LIKE BINARY @Password";
                MySqlCommand command = new MySqlCommand(loginString, connection);
                command.Parameters.AddWithValue("@Username", adminModel.Username);
                command.Parameters.AddWithValue("@Password", adminModel.Password);
                MySqlDataReader reader = command.ExecuteReader();
                if (await reader.ReadAsync())
                {
                    isLoginSuccessfull = true;
                }
                else
                {
                    isLoginSuccessfull = false;
                }
            }
            return isLoginSuccessfull;
        }
        public async Task UpdateCode(string email, string code)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                var isCodeEmpty = CheckIfCodeIsEmpty(email, connection);
                string queryString = String.Empty;
                queryString = "UPDATE login_table SET user_code=@code WHERE email = @email";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@email", email);
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task UpdateNewPassword(string email, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "UPDATE login_table SET user_password=@newPassword WHERE email=@email";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@newPassword", password);
                command.Parameters.AddWithValue("@email", email);
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<bool> CheckIfCodeIsEqual(string email, string code)
        {
            bool isCodeEqual = false;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT user_code FROM login_table WHERE email=@email";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@email", email);
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        if (code.Equals(reader["user_code"]))
                        {
                            isCodeEqual = true;
                        }
                    }
                }
            }
            return isCodeEqual;
        }
        private async Task<bool> CheckIfCodeIsEmpty(string email, MySqlConnection connection)
        {
            string queryString = "SELECT user_code FROM login_table WHERE email=@email";
            MySqlCommand command = new MySqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@email", email);
            using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    var userCode = reader["user_code"].ToString();
                    if (String.IsNullOrWhiteSpace(userCode))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}