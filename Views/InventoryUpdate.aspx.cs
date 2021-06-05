using SoftEngWebEmployee.Helpers;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Views
{
    public partial class InventoryUpdate : System.Web.UI.Page
    {
        public string ProductID { get; set; }
        public string ImageString { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductID = Request.QueryString["id"];
            if (ProductID == null)
            {
                Response.Redirect("InventoryAdd", false);
                return;
            }
            if (!IsPostBack)
            {
                LoadCategories();
                LoadProductData();
            }
        }

        protected void UpdateCategoryRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
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
            ProductDropdown.Text = category + " " + caret;
            ProductCategory.Text = category;
        }

        protected async void BtnUpdateProduct_Click(object sender, EventArgs e)
        {
            try
            {
                var isValid = ValidateFields();
                if (isValid)
                {
                    if (ProductID != null)
                    {
                        Stream fs = ProductPicture.PostedFile.InputStream;
                        var product = new ProductModel
                        {
                            ProductName = ProductName.Text,
                            ProductCategory = ProductCategory.Text,
                            ProductPrice = int.Parse(ProductPrice.Text),
                            ProductStocks = int.Parse(ProductStocks.Text),
                            ProductDescription = ProductDescription.Text,
                            ProductPicture_Upload = fs
                        };
                        var generatedNotification = await NotificationRepository
                                .SingleInstance
                                .GenerateNotification(NotificationType.UpdatedProduct, product.ProductName);
                        await NotificationRepository.SingleInstance
                           .InsertNewNotificationAsync(generatedNotification);
                        await ProductRepository.SingleInstance.UpdateProductAsync(product, int.Parse(ProductID));
                        Response.Redirect("Inventory.aspx", false);
                    }
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
                if (int.Parse(ProductPrice.Text) <= 0)
                {
                    BuildSweetAlert("Price Error!", "Price cannot be below zero!", Constants.AlertStatus.warning);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(ProductName.Text))
                {
                    BuildSweetAlert("Empty Field!", "Empty Product Name", Constants.AlertStatus.warning);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(ProductCategory.Text))
                {
                    BuildSweetAlert("Empty Field!", "Empty Product Category", Constants.AlertStatus.warning);
                    return false;
                }
                if (string.IsNullOrWhiteSpace(ProductDescription.Text))
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
        private async void LoadCategories()
        {
            var categories = await ProductRepository.SingleInstance.FetchAllCategoriesAsync();
            AddCategoryRepeater.DataSource = categories;
            AddCategoryRepeater.DataBind();
        }
        private async void LoadProductData()
        {
            var productModel = await ProductRepository.SingleInstance.FetchProductDetailsAsync(ProductID);
            if (productModel != null)
            {
                ProductName.Text = productModel.ProductName;
                ProductCategory.Text = productModel.ProductCategory;
                ProductPrice.Text = productModel.ProductPrice.ToString();
                ProductStocks.Text = productModel.ProductStocks.ToString();
                ProductDescription.Text = productModel.ProductDescription.ToString();
                ImageString = productModel.ProductPicture;
            }
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