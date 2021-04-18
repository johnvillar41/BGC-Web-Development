using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
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
                StatusPanel.Visible = false;
                UnsucessPanel.Visible = false;
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
            Response.Redirect("DisplaySales");
        }
        protected async void CategoryBtn_Click(object sender, EventArgs e)
        {
            string category = (sender as Button).Text.ToString();

            var newSearch = await ProductRepository.GetInstance().FetchOnCategory(category);
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
            var newSearch = await ProductRepository.GetInstance().FetchAllProducts();
            ProductsRepeater.DataSource = newSearch;
            ProductsRepeater.DataBind();
        }
        protected async void BtnAddToCart_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var productID = button.CommandArgument.ToString();
            RepeaterItem repeaterItem = button.NamingContainer as RepeaterItem;
            var totalItem = (TextBox)repeaterItem.FindControl("TotalItems");

            ProductModel product = await ProductRepository.GetInstance().GetProducts(int.Parse(productID));
            product.TotalNumberOfCartItems = int.Parse(totalItem.Text);
            Cart.AddCartItem(product);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "Swal.fire('Successfully Added Product')", true);
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
            if(Cart.GetCartItems().Count == 0)
            {
                UnsucessPanel.Visible = true;
                return;
            }
            var onSiteTransaction = new OnsiteTransactionModel
            {
                Customer = null,
                TotalSale = Cart.CalculateTotalSales()
            };
            var connection = await OnsiteTransactionRepository.GetInstance().InsertNewTransaction(onSiteTransaction);
            var transactionID = await OnsiteTransactionRepository.GetInstance().FetchLastInsertID(connection);
            var onsiteProducts = Cart.ListOfOnsiteProducts(transactionID);
            foreach (var onsiteModels in onsiteProducts)
            {
                await OnsiteProductsTransactionRepository.GetInstance().InsertTransactions(onsiteModels);
            }
            var newSale = new SalesModel
            {
                SalesType = Constants.SalesType.Onsite,
                Administrator = await AdministratorRepository.GetInstance().FindAdministrator(UserSession.GetLoggedInUser()),
                Date = DateTime.Now,
                OnsiteTransaction = new OnsiteTransactionModel
                {
                    TransactionID = transactionID
                }
            };
            await SalesRepository.GetInstance().InsertNewSale(newSale);
            var notification = NotificationRepository.GetInstance().GenerateNotification(Constants.NotificationType.SoldItem, onsiteProducts.ToString());
            NotificationRepository.GetInstance().InsertNewNotification(notification);                 
            StatusPanel.Visible = true;
            Cart.ClearCartItems();
            LoadCart();
        }
        private async void LoadSales()
        {
            var salesList = await SalesRepository.GetInstance().FetchAllSales();            
            SalesRepeater.DataSource = salesList;
            SalesRepeater.DataBind();            
        }
        private async void LoadProducts()
        {
            var productsList = await ProductRepository.GetInstance().FetchAllProducts();
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
            var categories = await ProductRepository.GetInstance().FetchAllCategories();
            CategoryRepeater.DataSource = categories;
            CategoryRepeater.DataBind();
        }       
    }
}