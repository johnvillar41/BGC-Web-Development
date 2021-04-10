﻿using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Views
{
    public partial class Orders : System.Web.UI.Page
    {
        private List<OrdersModel> OrdersList = new List<OrdersModel>();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }

        public List<OrdersModel> DisplayOrders()
        {
            return OrdersList;
        }

        private async void LoadOrders()
        {
            var listOfOrders = await OrdersRepository.GetInstance().FetchAllOrders();
            OrdersList = (List<OrdersModel>)listOfOrders;
        }

        protected async void btnCancelStatus_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(OrderIDCancel.Text) && IsAllAlphabetic(OrderIDCancel.Text))
            {
                await OrdersRepository .GetInstance().ChangeStatusOfOrderToCancelled(int.Parse(OrderIDCancel.Text));
                var generatedNotification = NotificationRepository
                    .GetInstance()
                    .GenerateNotification(NotificationType.CancelledOrder, OrderIDCancel.Text);
                NotificationRepository.GetInstance().InsertNewNotification(generatedNotification);
                Response.Redirect(Request.RawUrl,false);
            }
        }

        protected async void btnFinishStatus_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(OrderIDFinish.Text) && IsAllAlphabetic(OrderIDFinish.Text))
            {
                await OrdersRepository.GetInstance().ChangeStatusOfOrderToFinished(int.Parse(OrderIDFinish.Text));                
                var generatedNotification = NotificationRepository
                    .GetInstance()
                    .GenerateNotification(NotificationType.FinishedOrder, OrderIDFinish.Text);
                NotificationRepository.GetInstance().InsertNewNotification(generatedNotification);
                
                var orders = await OrdersRepository.GetInstance().FetchOrder(int.Parse(OrderIDFinish.Text));
                var salesModel = new SalesModel()
                {
                    SalesTitle = "Finished Order",
                    SalesTransactionValue = orders.OrderTotalPrice,
                    TotalNumberOfProducts = orders.SpecificOrdersModel.Count,
                    SalesDate = DateTime.Now,
                    DateMonth = DateTime.Now.Month,
                    Administrator = await AdministratorRepository.GetInstance().FindAdministrator(UserSession.GetLoggedInUser()),
                    TypeOfSale = SalesType.Order
                };
                await SalesRepository.GetInstance().InsertNewSale(salesModel);
                Response.Redirect(Request.RawUrl,false);
            }
        }

        protected async void BtnSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(OrderIdSearchTextbox.Text) && IsAllAlphabetic(OrderIdSearchTextbox.Text))
            {
                var order = await OrdersRepository.GetInstance().FetchOrder(int.Parse(OrderIdSearchTextbox.Text));
                if (order != null)
                {
                    OrdersList.Clear();
                    OrdersList.Add(order);
                    DisplayOrders();
                }
            }
        }

        private bool IsAllAlphabetic(string value)
        {
            foreach (char c in value)
            {
                if (char.IsLetter(c))
                    return false;
            }
            return true;
        }

    }
}