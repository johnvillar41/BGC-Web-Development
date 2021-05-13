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
                string loginString = "SELECT * FROM login_table WHERE user_username=@Username AND user_password=@Password";
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
    }
}