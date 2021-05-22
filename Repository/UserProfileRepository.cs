using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SoftEngWebEmployee.Repository
{
    public class UserProfileRepository
    {
        private static UserProfileRepository instance = null;
        public static UserProfileRepository SingleInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserProfileRepository();
                }
                return instance;
            }            
        }
        private UserProfileRepository()
        {

        }
        /// <summary>
        ///     This function updates the administrator's profile picture
        /// </summary>
        /// <param name="profilePicture">
        ///     Passes profilePicture as a parameter
        /// </param>

        public async Task UpdateProfilePictureAsync(Stream profilePicture)
        {           
            BinaryReader br = new BinaryReader(profilePicture);
            byte[] bytes = br.ReadBytes((int)profilePicture.Length);
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "UPDATE login_table SET user_image=@image WHERE user_username=@username";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username", UserSession.SingleInstance.GetLoggedInUser());
                command.Parameters.AddWithValue("@image", bytes);
                await command.ExecuteNonQueryAsync();
            }
        }
        /// <summary>
        ///     This function lets the administrator update his profile
        /// </summary>
        /// <param name="updateduser">
        ///     Passes updateduser as a parameter
        /// </param>

        public async Task UpdateProfileAsync(AdministratorModel updateduser,string username)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "UPDATE login_table SET " +
                    "user_username=@username," +
                    "user_name=@fullname," +
                    "user_password=@password," +
                    "email=@email WHERE user_username=@usernameSearched";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username", updateduser.Username);
                command.Parameters.AddWithValue("@usernameSearched", username);
                command.Parameters.AddWithValue("@fullname", updateduser.Fullname);
                command.Parameters.AddWithValue("@password", updateduser.Password);
                command.Parameters.AddWithValue("@email", updateduser.Email);
                await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        ///     This function retrieves all information of the admin user
        /// </summary>
        /// <returns>
        ///     <para>Returns administrator's information</para>
        ///     <para>Type: AdministratorModel</para>
        /// </returns>
        public async Task<AdministratorModel> FetchLoggedInModelAsync()
        {
            AdministratorModel administrator = null;
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM login_table WHERE user_username=@usernameLoggedIn";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@usernameLoggedIn", UserSession.SingleInstance.GetLoggedInUser());
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while(await reader.ReadAsync())
                {
                    string base64String = Convert.ToBase64String((byte[])(reader["user_image"]));
                    administrator = new AdministratorModel()
                    {
                        User_ID = int.Parse(reader["user_id"].ToString()),
                        Username = reader["user_username"].ToString(),
                        Password = reader["user_password"].ToString(),
                        Fullname = reader["user_name"].ToString(),
                        ProfilePicture = base64String,
                        Email = reader["email"].ToString()
                    };
                }                
            }
            return administrator;
        }
    }
}