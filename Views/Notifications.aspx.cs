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
                LoadCategories();
            }            
        }
        protected void NotificationCategory_Click(object sender, EventArgs e)
        {
            UpdateProgress1.Visible = true;
            var category = (sender as Button).Text.ToString();
            char caret = Convert.ToChar(0x000025BC);
            DropDownEmployee.Text = category + " " + caret;
            if (category == "Select All Categories")
            {
                LoadNotifications();
            }
            else
            {
                var notificationType = NotificationRepository.SingleInstance.CategorizeNotification(category);
                LoadNotificationsByCategory((Constants.NotificationType)notificationType);
            }
            UpdateProgress1.Visible = false;
        }        
        protected void CategoryRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            Button button = e.Item.FindControl("NotificationCategory") as Button;

            ScriptManager current = ScriptManager.GetCurrent(Page);
            if (current != null)
                current.RegisterAsyncPostBackControl(button);
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
            if (UserSession.SingleInstance.IsAdministrator())
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
            if (UserSession.SingleInstance.IsAdministrator())
            {
                var dateToday = DateTime.Now.ToString("yyyy-MM-dd");
                notifications = await NotificationRepository.SingleInstance.FetchNotificationsGivenDateAsync(dateToday);
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
            var notifications = await NotificationRepository.SingleInstance.FetchNotificationsByEmployee(employee);
            NotificationsList = notifications;           
        }
        private async void LoadNotificationsByCategory(Constants.NotificationType notificationType)
        {
            var notifications = await NotificationRepository.SingleInstance.FetchNotificationsByCategory(notificationType);
            NotificationsList = notifications;
        }
        private void LoadCategories()
        {
            List<string> NotificationTypes = new List<string>
            {
                Constants.NotificationTypeDefinitions.DELETE_USER,
                Constants.NotificationTypeDefinitions.CREATED_NEW_USER,
                Constants.NotificationTypeDefinitions.UPDATED_USER,
                Constants.NotificationTypeDefinitions.CANCELLED_ORDER,
                Constants.NotificationTypeDefinitions.FINISHED_ORDER,
                Constants.NotificationTypeDefinitions.SOLD_ITEM,
                Constants.NotificationTypeDefinitions.ADDED_PRODUCT,
                Constants.NotificationTypeDefinitions.UPDATED_PRODUCT
            };
            CategoryRepeater.DataSource = NotificationTypes;
            CategoryRepeater.DataBind();
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