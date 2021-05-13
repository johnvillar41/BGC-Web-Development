using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                SweetAlertBuilder sweetAlert = new SweetAlertBuilder
                {
                    HexaBackgroundColor = "#90EE90",
                    AlertIcons = Constants.AlertStatus.success,
                    Title = "Successfull",
                    Message = "Successfully added to cart: " + product.ProductName,
                };
                sweetAlert.BuildSweetAlert(this);
                Cart.AddCartItem(product);
            }
            catch (Exception)
            {
                SweetAlertBuilder sweetAlert = new SweetAlertBuilder
                {
                    HexaBackgroundColor = "#ffcccb",
                    AlertIcons = Constants.AlertStatus.error,
                    Title = "Error",
                    Message = "Error Adding to Cart: " + product.ProductName,
                };
                sweetAlert.BuildSweetAlert(this);
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
            SweetAlertBuilder sweetAlert;
            if (Cart.GetCartItems().Count == 0)
            {
                sweetAlert = new SweetAlertBuilder
                {
                    HexaBackgroundColor = "#ffcccb",
                    AlertIcons = Constants.AlertStatus.error,
                    Title = "Error Processing Request",
                    Message = "Cart has no items"
                };
                sweetAlert.BuildSweetAlert(this);
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

            sweetAlert = new SweetAlertBuilder
            {
                HexaBackgroundColor = "#ffcccb",
                AlertIcons = Constants.AlertStatus.success,
                Title = "Successfully Added Sales",
            };
            Cart.ClearCartItems();
            sweetAlert.BuildSweetAlert(this);
            LoadCart();
            LoadSales();
            LoadProducts();
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
                Administrator = await AdministratorRepository.SingleInstance.FindAdministratorAsync(UserSession.GetLoggedInUser()),
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
            var notification = NotificationRepository.SingleInstance.GenerateNotification(Constants.NotificationType.SoldItem, productListString);
            await NotificationRepository.SingleInstance.InsertNewNotificationAsync(notification);
        }
        private async void LoadSales()
        {
            var salesList = await SalesRepository.GetInstance().FetchAllSalesAsync();
            SalesRepeater.DataSource = salesList;
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
    }
}