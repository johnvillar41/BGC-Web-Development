using System;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Models
{
    public class NotificationsModel
    {
        public int Notifications_ID { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationContent { get; set; }
        public DateTime NotificationDate { get; set; }
        public string Username { get; set; }
        public NotificationType TypeOfNotification { get; set; }
    }
}