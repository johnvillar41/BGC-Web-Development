using MySql.Data.MySqlClient;
using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Repository
{
    public class NotificationRepository
    {
        private static NotificationRepository instance = null;
        public static NotificationRepository SingleInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NotificationRepository();
                }
                return instance;
            }

        }
        private NotificationRepository()
        {

        }
        //TODO to be used on Notifications settings
        public async Task<List<NotificationsModel>> FetchNotificationsByEmployee(string employee)
        {
            List<NotificationsModel> notificationsList = new List<NotificationsModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM notifications_table WHERE user_name=@employee";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@employee", employee);
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        notificationsList.Add(
                           new NotificationsModel
                           {
                               Notifications_ID = int.Parse(reader["notif_id"].ToString()),
                               NotificationTitle = reader["notif_title"].ToString(),
                               NotificationContent = reader["notif_content"].ToString(),
                               NotificationDate = DateTime.Parse(reader["notif_date"].ToString()),
                               Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(reader["user_name"].ToString()),
                               TypeOfNotification = CategorizeNotification(reader["notif_title"].ToString())
                           }
                       );
                    }                   
                }
            }
            return notificationsList;
        }
        /// <summary>
        ///     Fetches a list of notifications on a certain date
        /// </summary>
        /// <param name="date">
        ///     Passes a date as paramter
        /// </param>
        /// <returns>
        ///     <para>Returns a list of all the notifications for a certain date</para>
        ///     <para>Type: List<NotificationsModel></para>
        /// </returns>
        public async Task<List<NotificationsModel>> FetchNotificationsGivenDateAsync(string date)
        {
            List<NotificationsModel> notificationsList = new List<NotificationsModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM notifications_table WHERE notif_date LIKE  @date ";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@date", "%" + date + "%");
                MySqlDataReader reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    NotificationsModel notifications = new NotificationsModel()
                    {
                        Notifications_ID = int.Parse(reader["notif_id"].ToString()),
                        NotificationTitle = reader["notif_title"].ToString(),
                        NotificationContent = reader["notif_content"].ToString(),
                        NotificationDate = DateTime.Parse(reader["notif_date"].ToString()),
                        Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(reader["user_name"].ToString()),
                        TypeOfNotification = CategorizeNotification(reader["notif_title"].ToString())
                    };
                    notificationsList.Add(notifications);
                }
            }
            notificationsList.Reverse();
            return notificationsList;
        }       
        /// <summary>
        ///     Generates a notification for each database transaction done inside the program
        /// </summary>
        /// <param name="notificationType">
        ///     Passes an enum of what type the notification is
        /// </param>
        /// <param name="itemAction">
        ///     Passes the description content of the notification
        /// </param>
        /// <returns>
        ///     <para>Returns a new generated notification</para>
        ///     <para>Type: NotificationsModel</para>
        /// </returns>
        public async Task<NotificationsModel> GenerateNotification(NotificationType notificationType, string itemAction)
        {
            NotificationsModel newNotification = null;
            switch (notificationType)
            {
                case NotificationType.DeleteUser:
                    newNotification = new NotificationsModel()
                    {
                        NotificationTitle = "Deleted User",
                        NotificationContent = "Deleted User: " + itemAction,
                        NotificationDate = DateTime.Today,                        
                        TypeOfNotification = NotificationType.DeleteUser
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.GetLoggedInUser().ToString());
                    
                    break;
                case NotificationType.CreateUser:
                    newNotification = new NotificationsModel()
                    {
                        NotificationTitle = "Created New User",
                        NotificationContent = "Created User: " + itemAction,
                        NotificationDate = DateTime.Today,                        
                        TypeOfNotification = NotificationType.CreateUser
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.GetLoggedInUser().ToString());
                    break;
                case NotificationType.UpdateUser:
                    newNotification = new NotificationsModel()
                    {
                        NotificationTitle = "Updated User",
                        NotificationContent = "Updated User: " + itemAction,
                        NotificationDate = DateTime.Today,                        
                        TypeOfNotification = NotificationType.UpdateUser
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.GetLoggedInUser().ToString());
                    break;
                case NotificationType.CancelledOrder:
                    newNotification = new NotificationsModel()
                    {
                        NotificationTitle = "Cancelled Order",
                        NotificationContent = "Cancelled Order for OrderID: " + itemAction,
                        NotificationDate = DateTime.Today,                        
                        TypeOfNotification = NotificationType.CancelledOrder
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.GetLoggedInUser().ToString());
                    break;
                case NotificationType.FinishedOrder:
                    newNotification = new NotificationsModel()
                    {
                        NotificationTitle = "Finished Order",
                        NotificationContent = "Finished Order for Order ID: " + itemAction,
                        NotificationDate = DateTime.Today,                        
                        TypeOfNotification = NotificationType.FinishedOrder
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.GetLoggedInUser().ToString());
                    break;
                case NotificationType.SoldItem:
                    newNotification = new NotificationsModel
                    {
                        NotificationTitle = "Sold Item",
                        NotificationContent = "Sold Item: " + itemAction,
                        NotificationDate = DateTime.Today,
                        
                        TypeOfNotification = NotificationType.SoldItem
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.GetLoggedInUser().ToString());
                    break;
            }
            return newNotification;
        }
        /// <summary>
        ///     Inserts a new notification inside the database
        /// </summary>
        /// <param name="notification">
        ///     Passes a notification object as paramater
        /// </param>
        public async Task InsertNewNotificationAsync(NotificationsModel notification)
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
                command.Parameters.AddWithValue("@username", notification.Administrator.Username);
                await command.ExecuteNonQueryAsync();
            }
        }
        /// <summary>
        ///     Fetches all the notifications inside the database
        /// </summary>
        /// <returns>
        ///     <para>Returns a list of all the notifications</para>
        ///     <para>Type: List<NotificationsModel></para>
        /// </returns>
        public async Task<List<NotificationsModel>> FetchNotificationsAsync()
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
                        Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(reader["user_name"].ToString()),
                        TypeOfNotification = CategorizeNotification(reader["notif_title"].ToString())
                    };

                    notificationsList.Add(notifications);
                }
            }
            notificationsList.Reverse();
            return notificationsList;
        }
        /// <summary>
        ///     Generates an enum for the notification
        /// </summary>
        /// <param name="notification">
        ///     Passes a string value of notification to be converted into an enum
        /// </param>
        /// <returns>
        ///     <para>Returns a notification enum</para>
        ///     <para>Type: NotificationType</para>
        /// </returns>
        private NotificationType CategorizeNotification(string notification)
        {
            switch (notification)
            {
                case "Created New User":
                    return NotificationType.CreateUser;
                case "Updated User":
                    return NotificationType.UpdateUser;
                case "Deleted User":
                    return NotificationType.DeleteUser;
                case "Cancelled Order":
                    return NotificationType.CancelledOrder;
                case "Finished Order":
                    return NotificationType.FinishedOrder;
            }
            return NotificationType.FinishedOrder;
        }
    }
}