using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftEngWebEmployee.Views
{
    public partial class Notifications : System.Web.UI.Page
    {
        public List<NotificationsModel> NotificationsList { get; set; }        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNotifications();
                LoadEmployeesOnDropDown();
            }            
        }
        protected void EmployeeFullnameRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            Button button = e.Item.FindControl("EmployeeFullnameCategory") as Button;

            ScriptManager current = ScriptManager.GetCurrent(Page);
            if (current != null)
                current.RegisterAsyncPostBackControl(button);
        }
        protected void EmployeeFullnameCategory_Click(object sender, EventArgs e)
        {
            UpdateProgress1.Visible = true;            
            var employee = (sender as Button).Text.ToString();
            char caret = Convert.ToChar(0x000025BC);
            DropDownEmployee.Text = employee + " " + caret;
            if (employee == "All Employee")
            {
                LoadNotifications();
            }
            else
            {
                LoadNotificationByEmployee(employee);
            }
            UpdateProgress1.Visible = false;
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
        protected void EmployeeFullnameRepeater_ItemCreated1(object sender, RepeaterItemEventArgs e)
        {
            Button button = e.Item.FindControl("EmployeeFullnameCategory") as Button;

            ScriptManager current = ScriptManager.GetCurrent(Page);
            if (current != null)
                current.RegisterAsyncPostBackControl(button);
        }
        private async void LoadNotificationByEmployee(string employee)
        {
            UpdateProgress1.Visible = true;
            var notifications = await NotificationRepository.SingleInstance.FetchNotificationsByEmployee(employee);
            NotificationsList = notifications;
            UpdateProgress1.Visible = false;
        }
        private async void LoadEmployeesOnDropDown()
        {
            var employeeModelList = await AdministratorRepository.SingleInstance.FetchAdministratorsAsync();
            EmployeeFullnameRepeater.DataSource = employeeModelList;
            EmployeeFullnameRepeater.DataBind();
        }

        protected void BtnEmployeeAll_Click(object sender, EventArgs e)
        {
            UpdateProgress1.Visible = true;
            LoadNotifications();
            UpdateProgress1.Visible = false;
        }
    }
}