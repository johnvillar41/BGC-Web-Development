using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            var notifications = await NotificationRepository.GetInstance().FetchNotifications();
            NotificationsList = notifications;
        }
        public List<NotificationsModel> DisplayNotifications()
        {
            return NotificationsList;
        }

    }
}