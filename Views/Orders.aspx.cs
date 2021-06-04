using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Views
{
    public partial class Orders : System.Web.UI.Page
    {
        public List<OrdersModel> OrdersList = new List<OrdersModel>();
        protected void Page_Load(object sender, EventArgs e)
        {            
            LoadOrders();
            LoadCategories();
        }       

        protected void btnCancelStatus_Click(object sender, EventArgs e)
        {
            UpdateProgress1.Visible = true;
            if (!String.IsNullOrWhiteSpace(OrderIDCancel.Text) && IsAllAlphabetic(OrderIDCancel.Text))
            {
                ProcessCancelOrder();
                LoadOrders();
                BuildSweetAlert("#fff", "Cancelled Order", $"Cancelled Order for: {OrderIDCancel.Text}", Constants.AlertStatus.warning);               
            }
            UpdateProgress1.Visible = false;
        }

        protected void btnFinishStatus_Click(object sender, EventArgs e)
        {
            UpdateProgress1.Visible = true;
            if (!String.IsNullOrWhiteSpace(OrderIDFinish.Text) && IsAllAlphabetic(OrderIDFinish.Text))
            {
                ProcessFinishOrder();                
                LoadOrders();
                InsertNewSale();
                BuildSweetAlert("#fff", "Finished Order", $"Finished Order for:{OrderIDFinish.Text}", Constants.AlertStatus.success);                
            }
            UpdateProgress1.Visible = false;
        }
        protected void CategoryRepeater_ItemCreated(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            Button button = e.Item.FindControl("Category") as Button;
            ScriptManager current = ScriptManager.GetCurrent(Page);
            if (current != null)
                current.RegisterAsyncPostBackControl(button);
        }
        protected void Category_Click(object sender, EventArgs e)
        {
            string category = (sender as Button).Text.ToString();
            char caret = Convert.ToChar(0x000025BC);
            dropdownMenuReference1.Text = category + " " + caret;
            if (category == "All Orders")
            {
                LoadOrders();
            }
            else
            {                
                LoadCategorizedOrders(category);                
            }
        }
        protected async void BtnSearch_Click(object sender, EventArgs e)
        {
            UpdateProgress1.Visible = true;
            if (!String.IsNullOrWhiteSpace(OrderIdSearchTextbox.Text) && IsAllAlphabetic(OrderIdSearchTextbox.Text))
            {
                var order = await OrdersRepository.SingleInstance.FetchOrderAsync(int.Parse(OrderIdSearchTextbox.Text));
                if (order != null)
                {
                    OrdersList.Clear();
                    OrdersList.Add(order);                    
                }
            }
            UpdateProgress1.Visible = false;
        }
        private async void ProcessCancelOrder()
        {
            if (await OrdersRepository.SingleInstance.CheckIfIdExistAsync(int.Parse(OrderIDCancel.Text)) == false)
            {
                BuildSweetAlert("#fff", "ID Not Found!", $"ID not found for: {OrderIDCancel.Text}", Constants.AlertStatus.warning);
                return;
            }
            await OrdersRepository.SingleInstance.ChangeStatusOfOrderToCancelledAsync(int.Parse(OrderIDCancel.Text));
            var doesSalesExist = await SalesRepository.GetInstance().CheckIfSaleExist(int.Parse(OrderIDCancel.Text));
            if (doesSalesExist)
            {
                await SalesRepository.GetInstance().RemoveSale(int.Parse(OrderIDCancel.Text));
                var productIds = await SpecificOrdersRepository.SingleInstance.FetchProductIDsAsync(int.Parse(OrderIDCancel.Text));
                foreach (KeyValuePair<int, int> productId in productIds)
                {
                    await ProductRepository.SingleInstance.AddProductStocksAsync(productId.Value, productId.Key);                    
                }                
            }            
            var generatedNotification = await NotificationRepository
                .SingleInstance
                .GenerateNotification(NotificationType.CancelledOrder, OrderIDCancel.Text);
            await NotificationRepository.SingleInstance.InsertNewNotificationAsync(generatedNotification);
        }
        private async void ProcessFinishOrder()
        {
            if (await OrdersRepository.SingleInstance.CheckIfIdExistAsync(int.Parse(OrderIDFinish.Text)) == false)
            {
                BuildSweetAlert("#fff", "ID Not Found!", $"ID not found for: {OrderIDFinish.Text}", Constants.AlertStatus.error);
                return;
            }
            var productIds = await SpecificOrdersRepository.SingleInstance.FetchProductIDsAsync(int.Parse(OrderIDFinish.Text));
            foreach (KeyValuePair<int, int> productId in productIds)
            {
                await ProductRepository.SingleInstance.SubtractProductStocksAsync(productId.Value, productId.Key);
            }
            await OrdersRepository.SingleInstance.UpdateAdministratorStatusOnSpecificOrders(int.Parse(OrderIDFinish.Text));
            await OrdersRepository.SingleInstance.ChangeStatusOfOrderToFinishedAsync(int.Parse(OrderIDFinish.Text));
            var generatedNotification = await NotificationRepository
                .SingleInstance
                .GenerateNotification(NotificationType.FinishedOrder, OrderIDFinish.Text);
            await NotificationRepository.SingleInstance.InsertNewNotificationAsync(generatedNotification);
        }
        private async void InsertNewSale()
        {
            var salesModel = new SalesModel()
            {
                SalesType = Constants.SalesType.Order,
                Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.SingleInstance.GetLoggedInUser()),
                Date = DateTime.Now,
                Orders = await OrdersRepository.SingleInstance.FetchOrderAsync(int.Parse(OrderIDFinish.Text)),
                OnsiteTransaction = null
            };
            await SalesRepository.GetInstance().InsertNewSaleAsync(salesModel);
        }
        private void BuildSweetAlert(string hexaColor, string title, string message, Constants.AlertStatus alertStatus)
        {
            SweetAlertBuilder sweetAlert = new SweetAlertBuilder
            {
                HexaBackgroundColor = hexaColor,
                Title = title,
                Message = message,
                AlertIcons = alertStatus,
                AlertPositions = Constants.AlertPositions.TOP_END,
                ShowCloseButton = true
            };
            sweetAlert.BuildSweetAlert(this);
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
        private void LoadCategories()
        {
            List<string> CategoryList = new List<string>
                {
                    Constants.OrderStatus.Pending.ToString(),
                    Constants.OrderStatus.Cancelled.ToString(),
                    Constants.OrderStatus.Finished.ToString()
                };
            var categories = CategoryList;
            CategoryRepeater.DataSource = categories;
            CategoryRepeater.DataBind();
        }
        private async void LoadOrders()
        {
            var listOfOrders = await OrdersRepository.SingleInstance.FetchAllOrdersAsync();
            OrdersList = (List<OrdersModel>)listOfOrders;
        }
        private async void LoadCategorizedOrders(string orderStatus)
        {
            var listOfOrders = await OrdersRepository.SingleInstance.FetchCategorizedOrders(orderStatus);
            OrdersList = listOfOrders;
        }        
    }
}