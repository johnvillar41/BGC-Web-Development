using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using SoftEngWebEmployee.Helpers;
using System.IO;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Views
{
    public partial class InventoryAdd : System.Web.UI.Page
    {
        public string ImageString { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
            }
        }

        private async void LoadCategories()
        {
            var categories = await ProductRepository.SingleInstance.FetchAllCategoriesAsync();
            AddCategoryRepeater.DataSource = categories;
            AddCategoryRepeater.DataBind();
        }

        protected void AddCategoryRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            Button button = e.Item.FindControl("category") as Button;
            ScriptManager current = ScriptManager.GetCurrent(Page);
            if (current != null)
                current.RegisterAsyncPostBackControl(button);
        }

        protected void AddCategory_Click(object sender, EventArgs e)
        {
            string category = (sender as Button).Text.ToString();
            char caret = Convert.ToChar(0x000025BC);
            addProductDropdown.Text = category + " " + caret;
            addProductCategory.Text = category;
        }

        protected async void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                var isValid = ValidateFields();
                if (isValid)
                {
                    Stream fs = addProductPicture.PostedFile.InputStream;
                    ProductModel addProductInfo = new ProductModel
                    {
                        ProductName = addProductName.Text,
                        ProductCategory = addProductCategory.Text,
                        ProductDescription = addProductDescription.Text,
                        ProductPrice = int.Parse(addProductPrice.Text),
                        ProductStocks = int.Parse(addProductStocks.Text),
                        ProductPicture_Upload = fs
                    };
                    await ProductRepository.SingleInstance.AddNewProductAsync(addProductInfo);
                    var generatedNotification = await NotificationRepository
                        .SingleInstance
                        .GenerateNotification(NotificationType.AddedProduct, addProductInfo.ProductName);
                    await NotificationRepository.SingleInstance
                       .InsertNewNotificationAsync(generatedNotification);
                    Response.Redirect("InventoryAdd.aspx", false);
                    BuildSweetAlert("Product Successfully Added!", $"Product {addProductInfo.ProductName} has been added!", Constants.AlertStatus.success);

                }
            }
            catch (System.FormatException)
            {
                BuildSweetAlert("Missing Fields!", "Some fields are missing, please fill them up!", Constants.AlertStatus.warning);
                return;
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                BuildSweetAlert("Same Product Name!", "Please Change Product Name!", Constants.AlertStatus.warning);
                return;
            }
        }
        private bool ValidateFields()
        {
            try
            {
                if (int.Parse(addProductPrice.Text) <= 0)
                {
                    BuildSweetAlert("Price Error!", "Price cannot be below zero!", Constants.AlertStatus.warning);
                    return false;
                }
                if (int.Parse(addProductStocks.Text) <= 0)
                {
                    BuildSweetAlert("Stocks Error!", "Stocks cannot be below zero!", Constants.AlertStatus.warning);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(addProductName.Text))
                {
                    BuildSweetAlert("Empty Field!", "Empty Product Name", Constants.AlertStatus.warning);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(addProductCategory.Text))
                {
                    BuildSweetAlert("Empty Field!", "Empty Product Category", Constants.AlertStatus.warning);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(addProductDescription.Text))
                {
                    BuildSweetAlert("Empty Field!", "Empty Product Description", Constants.AlertStatus.warning);
                    return false;
                }
            }
            catch (System.OverflowException)
            {
                BuildSweetAlert("Value is too high!", "Incorrect ammount input!", Constants.AlertStatus.warning);
                return false;
            }
            return true;
        }
        private void BuildSweetAlert(string title, string message, Constants.AlertStatus alertStatus)
        {
            var sweetAlert = new SweetAlertBuilder
            {
                HexaBackgroundColor = "#fff",
                Title = title,
                Message = message,
                AlertIcons = alertStatus,
                AlertPositions = Constants.AlertPositions.CENTER
            };
            sweetAlert.BuildSweetAlert(this);
        }
    }
}