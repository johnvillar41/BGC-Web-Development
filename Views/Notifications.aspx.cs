using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SoftEngWebEmployee.Views
{
    public partial class Notifications : System.Web.UI.Page
    {
        public List<NotificationsModel> NotificationsList { get; set; }        
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadNotifications();
        }
        private async void LoadNotifications()
        {
            List<NotificationsModel> notifications = null;
            if (UserSession.IsAdministrator())
            {
                notifications = await NotificationRepository.SingleInstance.FetchNotificationsAsync();
            }
            else
            {
                notifications = await NotificationRepository.SingleInstance.FetchEmployeeLoggedInNotifications();
            }            
            NotificationsList = notifications;
        }       
        protected async void FindDate_Click(object sender, EventArgs e)
        {
            UpdateProgress1.Visible = true;           
            List<NotificationsModel> notifications = null;
            if (UserSession.IsAdministrator())
            {
                notifications = await NotificationRepository.SingleInstance.FetchNotificationsGivenDateAsync(DateText.Text);
            }
            else
            {
                notifications = await NotificationRepository.SingleInstance.FetchNotificationsGivenDateAsyncByLoggedInEmployee(DateText.Text);
            }
            NotificationsList = notifications;            
            UpdateProgress1.Visible = false;
        }
    }
}