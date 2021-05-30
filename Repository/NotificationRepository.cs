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
                    while (await reader.ReadAsync())
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
            notificationsList.Reverse();
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
        public async Task<List<NotificationsModel>> FetchNotificationsGivenDateAsyncByLoggedInEmployee(string date)
        {
            List<NotificationsModel> notificationsList = new List<NotificationsModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM notifications_table WHERE notif_date LIKE  @date AND user_name=@username";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@date", "%" + date + "%");
                command.Parameters.AddWithValue("@username", UserSession.SingleInstance.GetLoggedInUser());
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

        public async Task<List<NotificationsModel>> FetchNotificationsByCategory(NotificationType notificationType)
        {
            List<NotificationsModel> NotifList = new List<NotificationsModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM notifications_table WHERE notif_title=@notifType";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                var notification = await GenerateNotification(notificationType, null);
                command.Parameters.AddWithValue("@notifType", notification.NotificationTitle);                
                using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        NotifList.Add(
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
                NotifList.Reverse();
                return NotifList;
            }
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
                        NotificationTitle = Constants.NotificationTypeDefinitions.DELETE_USER,
                        NotificationContent = "Deleted User: " + itemAction,
                        NotificationDate = DateTime.Today,
                        TypeOfNotification = NotificationType.DeleteUser
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.SingleInstance.GetLoggedInUser().ToString());

                    break;
                case NotificationType.CreateUser:
                    newNotification = new NotificationsModel()
                    {
                        NotificationTitle = Constants.NotificationTypeDefinitions.CREATED_NEW_USER,
                        NotificationContent = "Created User: " + itemAction,
                        NotificationDate = DateTime.Now,
                        TypeOfNotification = NotificationType.CreateUser
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.SingleInstance.GetLoggedInUser().ToString());
                    break;
                case NotificationType.UpdateUser:
                    newNotification = new NotificationsModel()
                    {
                        NotificationTitle = Constants.NotificationTypeDefinitions.UPDATED_USER,
                        NotificationContent = "Updated User: " + itemAction,
                        NotificationDate = DateTime.Now,
                        TypeOfNotification = NotificationType.UpdateUser
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.SingleInstance.GetLoggedInUser().ToString());
                    break;
                case NotificationType.CancelledOrder:
                    newNotification = new NotificationsModel()
                    {
                        NotificationTitle = Constants.NotificationTypeDefinitions.CANCELLED_ORDER,
                        NotificationContent = "Cancelled Order for OrderID: " + itemAction,
                        NotificationDate = DateTime.Now,
                        TypeOfNotification = NotificationType.CancelledOrder
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.SingleInstance.GetLoggedInUser().ToString());
                    break;
                case NotificationType.FinishedOrder:
                    newNotification = new NotificationsModel()
                    {
                        NotificationTitle = Constants.NotificationTypeDefinitions.FINISHED_ORDER,
                        NotificationContent = "Finished Order for Order ID: " + itemAction,
                        NotificationDate = DateTime.Now,
                        TypeOfNotification = NotificationType.FinishedOrder
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.SingleInstance.GetLoggedInUser().ToString());
                    break;
                case NotificationType.SoldItem:
                    newNotification = new NotificationsModel
                    {
                        NotificationTitle = Constants.NotificationTypeDefinitions.SOLD_ITEM,
                        NotificationContent = "Sold Item: " + itemAction,
                        NotificationDate = DateTime.Now,
                        TypeOfNotification = NotificationType.SoldItem
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.SingleInstance.GetLoggedInUser().ToString());
                    break;
                case NotificationType.AddedProduct:
                    newNotification = new NotificationsModel
                    {
                        NotificationTitle = Constants.NotificationTypeDefinitions.ADDED_PRODUCT,
                        NotificationContent = "Added new Product: " + itemAction,
                        NotificationDate = DateTime.Now,
                        TypeOfNotification = NotificationType.AddedProduct
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.SingleInstance.GetLoggedInUser().ToString());
                    break;
                case NotificationType.UpdatedProduct:
                    newNotification = new NotificationsModel
                    {
                        NotificationTitle = Constants.NotificationTypeDefinitions.UPDATED_PRODUCT,
                        NotificationContent = "Updated Product: " + itemAction,
                        NotificationDate = DateTime.Now,
                        TypeOfNotification = NotificationType.UpdatedProduct
                    };
                    newNotification.Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.SingleInstance.GetLoggedInUser().ToString());
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
        public async Task<List<NotificationsModel>> FetchEmployeeLoggedInNotifications()
        {
            List<NotificationsModel> notificationsList = new List<NotificationsModel>();
            using (MySqlConnection connection = new MySqlConnection(DbConnString.DBCONN_STRING))
            {
                await connection.OpenAsync();
                string queryString = "SELECT * FROM notifications_table WHERE user_name=@username";
                MySqlCommand command = new MySqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@username", UserSession.SingleInstance.GetLoggedInUser());
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
        public NotificationType CategorizeNotification(string notification)
        {
            switch (notification)
            {
                case Constants.NotificationTypeDefinitions.CREATED_NEW_USER:
                    return NotificationType.CreateUser;
                case Constants.NotificationTypeDefinitions.UPDATED_USER:
                    return NotificationType.UpdateUser;
                case Constants.NotificationTypeDefinitions.DELETE_USER:
                    return NotificationType.DeleteUser;
                case Constants.NotificationTypeDefinitions.CANCELLED_ORDER:
                    return NotificationType.CancelledOrder;
                case Constants.NotificationTypeDefinitions.FINISHED_ORDER:
                    return NotificationType.FinishedOrder;
                case Constants.NotificationTypeDefinitions.SOLD_ITEM:
                    return NotificationType.SoldItem;
                case Constants.NotificationTypeDefinitions.ADDED_PRODUCT:
                    return NotificationType.AddedProduct;
                case Constants.NotificationTypeDefinitions.UPDATED_PRODUCT:
                    return NotificationType.UpdatedProduct;
            }
            return NotificationType.FinishedOrder;
        }
    }
}