using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoftEngWebEmployee.Repository.LoginRepository
{
    public class LoginRepository : ILoginRepository
    {

        private static LoginRepository instance = null;        

        public static LoginRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new LoginRepository();
            }
            return instance;
        }

        private LoginRepository()
        {

        }

        public async Task<bool> IsLoginSuccessfull(AdministratorModel adminModel)
        {
            bool isLoginSuccessfull = false;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string loginString = "SELECT * FROM login_table WHERE user_username=@Username AND user_password=@Password";
                MySqlCommand command = new MySqlCommand(loginString, connection);
                command.Parameters.AddWithValue("@Username", adminModel.Username);
                command.Parameters.AddWithValue("@Password", adminModel.User_Password);
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