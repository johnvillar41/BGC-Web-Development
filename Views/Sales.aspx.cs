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
    public partial class Sales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSales();
                LoadProducts();
                LoadCategories();
                LoadCart();
                LoadEmployeesOnDropDown();
            }
        }
        protected void IDS_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] arguments = btn.CommandArgument.Split(';');
            string orderID = arguments[0];
            string onsiteID = arguments[1];
            if (orderID.Length != 0)
            {
                Session["orderID"] = orderID;
            }
            else
            {
                Session["onsiteID"] = onsiteID;
            }
            Response.Redirect("DisplaySales", false);
        }
        protected async void CategoryBtn_Click(object sender, EventArgs e)
        {
            string category = (sender as Button).Text.ToString();

            var newSearch = await ProductRepository.SingleInstance.FetchOnCategoryAsync(category);
            ProductsRepeater.DataSource = newSearch;
            ProductsRepeater.DataBind();

        }
        protected void CategoryRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            Button button = e.Item.FindControl("CategoryBtn") as Button;

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
        protected void ProductsRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            Button button = e.Item.FindControl("BtnAddToCart") as Button;

            ScriptManager current = ScriptManager.GetCurrent(Page);
            if (current != null)
                current.RegisterAsyncPostBackControl(button);
        }
        protected async void CategoryBtnAllProducts_Click(object sender, EventArgs e)
        {
            var newSearch = await ProductRepository.SingleInstance.FetchAllProductsAsync();
            ProductsRepeater.DataSource = newSearch;
            ProductsRepeater.DataBind();
        }
        protected void EmployeeFullnameCategory_Click(object sender, EventArgs e)
        {
            UpdateProgress2.Visible = true;
            Thread.Sleep(2000);
            var employee = (sender as Button).Text.ToString();
            char caret = Convert.ToChar(0x000025BC);
            dropdownMenuReference1.Text = employee + " " + caret;
            if (employee == "All Employee")
            {
                LoadSales();
            }
            else
            {
                if (!UserSession.SingleInstance.IsAdministrator())
                {
                    LoadSalesByEmployee(UserSession.SingleInstance.GetLoggedInUser());
                    UpdateProgress2.Visible = false;
                    return;
                }
                LoadSalesByEmployee(employee);
            }
            UpdateProgress2.Visible = false;
        }
        protected async void BtnAddToCart_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var productID = button.CommandArgument.ToString();
            RepeaterItem repeaterItem = button.NamingContainer as RepeaterItem;
            var totalItem = (TextBox)repeaterItem.FindControl("TotalItems");

            ProductModel product = await ProductRepository.SingleInstance.GetProductsAsync(int.Parse(productID));
            try
            {
                product.TotalNumberOfProduct = int.Parse(totalItem.Text);
                product.SubTotalPrice = product.TotalNumberOfProduct * product.ProductPrice;
                if (await ProductRepository.SingleInstance.CheckIfProductIsEnough(product.TotalNumberOfProduct, int.Parse(productID)) == false || product.TotalNumberOfProduct == 0)
                {
                    BuildSweetAlert("#ffcccb", AlertStatus.error, "Error!", "Error adding to cart: " + product.ProductName + " due to not enough stocks!");
                    return;
                }
                BuildSweetAlert("#fff", AlertStatus.success, "Successfull", "Successfully added to cart: " + product.ProductName);
                Cart.AddCartItem(product);
            }
            catch (Exception)
            {
                BuildSweetAlert("#ffcccb", AlertStatus.error, "Error", "Error Adding to Cart: " + product.ProductName);
            }
            LoadCart();
        }
        protected void BtnRemoveCartItem_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var productID = button.CommandArgument.ToString();
            Cart.RemoveCartItem(int.Parse(productID));
            LoadCart();
        }

        protected void CartRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            Button button = e.Item.FindControl("BtnRemoveCartItem") as Button;

            ScriptManager current = ScriptManager.GetCurrent(Page);
            if (current != null)
                current.RegisterAsyncPostBackControl(button);
        }
        protected async void BtnConfirmCartOrder_Click(object sender, EventArgs e)
        {
            UpdateProgress2.Visible = true;
            if (Cart.GetCartItems().Count == 0)
            {
                BuildSweetAlert("#ffcccb", AlertStatus.error, "Error Processing Request", "Cart has no items");
                return;
            }
            var onSiteTransaction = new OnsiteTransactionModel
            {
                Customer = null,
                TotalSale = Cart.CalculateTotalSales()
            };
            var connection = await OnsiteTransactionRepository.SingleInstance.InsertNewTransaction(onSiteTransaction);
            var transactionID = await OnsiteTransactionRepository.SingleInstance.FetchLastInsertID(connection);
            var onsiteProducts = Cart.ListOfOnsiteProducts(transactionID);

            BuildSale(onsiteProducts, transactionID);
            BuildNotification(onsiteProducts);
            BuildSweetAlert("#90EE90", AlertStatus.success, "Successfully Added Sales", null);

            Cart.ClearCartItems();
            LoadCart();
            LoadSales();
            LoadProducts();
            Thread.Sleep(5000);
            UpdateProgress2.Visible = false;
        }
        private void BuildSweetAlert(string hexaBgColor, AlertStatus alertStatus, string title, string message)
        {
            SweetAlertBuilder sweetAlertBuilder = new SweetAlertBuilder
            {
                HexaBackgroundColor = hexaBgColor,
                AlertIcons = alertStatus,
                Title = title,
                Message = message,
            };
            sweetAlertBuilder.BuildSweetAlert(this);
        }
        private async void BuildSale(List<OnsiteProductsTransactionModel> onsiteProducts, int transactionID)
        {
            foreach (var onsiteModels in onsiteProducts)
            {
                await OnsiteProductsTransactionRepository.SingleInstance.InsertTransactionsAsync(onsiteModels);
            }
            var newSale = new SalesModel
            {
                SalesType = Constants.SalesType.Onsite,
                Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.SingleInstance.GetLoggedInUser()),
                Date = DateTime.Now,
                OnsiteTransaction = new OnsiteTransactionModel
                {
                    OnsiteProductTransactionList = onsiteProducts,
                    TransactionID = transactionID,
                }
            };
            foreach (var sale in newSale.OnsiteTransaction.OnsiteProductTransactionList)
            {
                await ProductRepository.SingleInstance.UpdateProductStocksAsync(sale.Product.TotalNumberOfProduct, sale.Product.Product_ID);
            }
            await SalesRepository.GetInstance().InsertNewSaleAsync(newSale);
        }
        private async void BuildNotification(List<OnsiteProductsTransactionModel> onsiteProducts)
        {
            string productListString = "";
            int counter = 0;
            foreach (var onsite in onsiteProducts)
            {
                productListString += onsite.Product.ProductName;
                counter++;
                if (counter == onsiteProducts.Count)
                {
                    break;
                }
                productListString += "|";
            }
            var notification = await NotificationRepository.SingleInstance.GenerateNotification(Constants.NotificationType.SoldItem, productListString);
            await NotificationRepository.SingleInstance.InsertNewNotificationAsync(notification);
        }
        private async void LoadSales()
        {
            if (UserSession.SingleInstance.IsAdministrator())
            {
                var salesList = await SalesRepository.GetInstance().FetchAllSalesAsync();
                SalesRepeater.DataSource = salesList;
                SalesRepeater.DataBind();
            }
        }
        private async void LoadSalesByEmployee(string employee)
        {
            var searchedEmployee = await SalesRepository.GetInstance().FetchSalesByEmployee(employee);
            SalesRepeater.DataSource = searchedEmployee;
            SalesRepeater.DataBind();
        }
        private async void LoadProducts()
        {
            var productsList = await ProductRepository.SingleInstance.FetchAllProductsAsync();
            ProductsRepeater.DataSource = productsList;
            ProductsRepeater.DataBind();
        }
        private void LoadCart()
        {
            var cartList = Cart.GetCartItems();
            CartRepeater.DataSource = cartList;
            CartRepeater.DataBind();
        }
        private async void LoadCategories()
        {
            var categories = await ProductRepository.SingleInstance.FetchAllCategoriesAsync();
            CategoryRepeater.DataSource = categories;
            CategoryRepeater.DataBind();
        }
        private async void LoadEmployeesOnDropDown()
        {
            if (UserSession.SingleInstance.IsAdministrator())
            {
                var employeeModelList = await AdministratorRepository.SingleInstance.FetchAdministratorsAsync();
                EmployeeFullnameRepeater.DataSource = employeeModelList;
                EmployeeFullnameRepeater.DataBind();
            }
        }
    }
}