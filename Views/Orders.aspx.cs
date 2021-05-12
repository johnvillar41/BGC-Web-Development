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
            var listOfOrders = await OrdersRepository.SingleInstance.FetchAllOrders();
            OrdersList = (List<OrdersModel>)listOfOrders;
        }

        protected async void btnCancelStatus_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(OrderIDCancel.Text) && IsAllAlphabetic(OrderIDCancel.Text))
            {
                if (await OrdersRepository.SingleInstance.CheckIfIdExist(int.Parse(OrderIDCancel.Text)) == false)
                {
                    SweetAlertBuilder alert = new SweetAlertBuilder
                    {
                        HexaBackgroundColor = "#fff",
                        Title = "ID Not Found!",
                        Message = "ID not found for: " + OrderIDCancel.Text,
                        AlertIcons = Constants.AlertStatus.error,
                        AlertPositions = Constants.AlertPositions.TOP_END,
                        ShowCloseButton = true
                    };
                    alert.BuildSweetAlert(this);
                    return;
                }
                await OrdersRepository .SingleInstance.ChangeStatusOfOrderToCancelled(int.Parse(OrderIDCancel.Text));
                var generatedNotification = NotificationRepository
                    .SingleInstance
                    .GenerateNotification(NotificationType.CancelledOrder, OrderIDCancel.Text);
                await NotificationRepository.SingleInstance.InsertNewNotification(generatedNotification);
                LoadOrders();
                SweetAlertBuilder sweetAlert = new SweetAlertBuilder
                {
                    HexaBackgroundColor = "#fff",
                    Title = "Cancelled Order",
                    Message = "Cancelled Order for: " + OrderIDCancel.Text,
                    AlertIcons = Constants.AlertStatus.warning,
                    AlertPositions = Constants.AlertPositions.TOP_END,
                    ShowCloseButton = true
                };
                sweetAlert.BuildSweetAlert(this);                
            }
        }

        protected async void btnFinishStatus_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(OrderIDFinish.Text) && IsAllAlphabetic(OrderIDFinish.Text))
            {
                if (await OrdersRepository.SingleInstance.CheckIfIdExist(int.Parse(OrderIDFinish.Text)) == false)
                {
                    SweetAlertBuilder alert = new SweetAlertBuilder
                    {
                        HexaBackgroundColor = "#fff",
                        Title = "ID Not Found!",
                        Message = "ID not found for: " + OrderIDFinish.Text,
                        AlertIcons = Constants.AlertStatus.error,
                        AlertPositions = Constants.AlertPositions.TOP_END,
                        ShowCloseButton = true
                    };
                    alert.BuildSweetAlert(this);
                    return;
                }
                var productIds = await SpecificOrdersRepository.SingleInstance.FetchProductIDs(int.Parse(OrderIDFinish.Text));
                foreach(KeyValuePair<int, int> productId in productIds)
                {                    
                    await ProductRepository.SingleInstance.UpdateProductStocks(productId.Value, productId.Key);
                }
                await OrdersRepository.SingleInstance.ChangeStatusOfOrderToFinished(int.Parse(OrderIDFinish.Text));
                var generatedNotification = NotificationRepository
                    .SingleInstance
                    .GenerateNotification(NotificationType.FinishedOrder, OrderIDFinish.Text);
                await NotificationRepository.SingleInstance.InsertNewNotification(generatedNotification);
                LoadOrders();
                var salesModel = new SalesModel()
                {
                    SalesType = Constants.SalesType.Order,
                    Administrator = await AdministratorRepository.SingleInstance.FindAdministrator(UserSession.GetLoggedInUser()),
                    Date = DateTime.Now,
                    Orders = await OrdersRepository.SingleInstance.FetchOrder(int.Parse(OrderIDFinish.Text)),
                    OnsiteTransaction = null
                };
                //TODO FIX THIS
                await SalesRepository.GetInstance().InsertNewSale(salesModel);
                
                SweetAlertBuilder sweetAlert = new SweetAlertBuilder
                {
                    HexaBackgroundColor = "#fff",
                    Title = "Finished Order",
                    Message = "Finished Order for: " + OrderIDFinish.Text,
                    AlertIcons = Constants.AlertStatus.success,
                    AlertPositions = Constants.AlertPositions.TOP_END,
                    ShowCloseButton = true
                };
                sweetAlert.BuildSweetAlert(this);                
            }
        }

        protected async void BtnSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(OrderIdSearchTextbox.Text) && IsAllAlphabetic(OrderIdSearchTextbox.Text))
            {
                var order = await OrdersRepository.SingleInstance.FetchOrder(int.Parse(OrderIdSearchTextbox.Text));
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