using MySql.Data.MySqlClient;
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
        private const string DBCONN_STRING = "server=localhost;user=admin;database=agt_db_relations;port=3306;password=admin";

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

        public async Task<bool> IsLoginSuccessfull(string username, string password)
        {
            bool isLoginSuccessfull = false;
            using (MySqlConnection connection = new MySqlConnection(DBCONN_STRING))
            {
                await connection.OpenAsync();
                string loginString = "SELECT * FROM login_table WHERE user_username=@Username AND user_password=@Password";
                MySqlCommand command = new MySqlCommand(loginString, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
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