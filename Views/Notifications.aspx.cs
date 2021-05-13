using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;

namespace SoftEngWebEmployee.Views
{
    public partial class Notifications : System.Web.UI.Page
    {
        private List<NotificationsModel> NotificationsList = new List<NotificationsModel>();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadNotifications();
        }
        private async void LoadNotifications()
        {
            var notifications = await NotificationRepository.SingleInstance.FetchNotificationsAsync();
            NotificationsList = notifications;
        }
        public List<NotificationsModel> DisplayNotifications()
        {            
            return NotificationsList;
        }

        protected async void FindDate_Click(object sender, EventArgs e)
        {            
            var notifications = await NotificationRepository.SingleInstance.FetchNotificationsGivenDateAsync(DateText.Text);
            NotificationsList = notifications;
        }
    }
}