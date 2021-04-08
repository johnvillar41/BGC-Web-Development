using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoftEngWebEmployee.Repository
{
    public class NotificationRepository
    {
        private static NotificationRepository instance = null;
        public static NotificationRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new NotificationRepository();
            }
            return instance;
        }
        private NotificationRepository()
        {

        }

        public NotificationsModel GenerateNotification(NotificationsModel.NotificationType notificationType, string itemAction)
        {
            NotificationsModel newNotification = null;
            switch (notificationType)
            {
                case NotificationsModel.NotificationType.DeleteUser:
                    newNotification = new NotificationsModel()
                    {
                        NotificationTitle = "Deleted User",
                        NotificationContent = "Deleted User: " + itemAction,
                        NotificationDate = DateTime.Today,
                        Username = UserSession.GetLoggedInUser()
                    };
                    break;
                case NotificationsModel.NotificationType.CreateUser:
                    newNotification = new NotificationsModel()
                    {
                        NotificationTitle = "Created New User",
                        NotificationContent = "Created User: " + itemAction,
                        NotificationDate = DateTime.Today,
                        Username = UserSession.GetLoggedInUser()
                    };
                    break;
                case NotificationsModel.NotificationType.UpdateUser:
                    newNotification = new NotificationsModel()
                    {
                        NotificationTitle = "Updated User",
                        NotificationContent = "Updated User: " + itemAction,
                        NotificationDate = DateTime.Today,
                        Username = UserSession.GetLoggedInUser()
                    };
                    break;
            }
            return newNotification;
        }

        public async void InsertNewNotification(NotificationsModel notification)
        {
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "INSERT INTO notifications_table(notif_title,notif_content,notif_date,user_name)" +
                    "VALUES(@title,@content,@date,@username)";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@title", notification.NotificationTitle);
                command.Parameters.AddWithValue("@content", notification.NotificationContent);
                command.Parameters.AddWithValue("@date", notification.NotificationDate);
                command.Parameters.AddWithValue("@username", notification.Username);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<NotificationsModel>> FetchNotifications()
        {
            List<NotificationsModel> notificationsList = new List<NotificationsModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM notifications_table";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    NotificationsModel notifications = new NotificationsModel()
                    {
                        Notifications_ID = int.Parse(reader["notif_id"].ToString()),
                        NotificationTitle = reader["notif_title"].ToString(),
                        NotificationContent = reader["notif_content"].ToString(),
                        NotificationDate = DateTime.Parse(reader["notif_date"].ToString()),
                        Username = reader["user_name"].ToString()
                    };
                    notificationsList.Add(notifications);
                }
            }
            return notificationsList;
        }

    }
}