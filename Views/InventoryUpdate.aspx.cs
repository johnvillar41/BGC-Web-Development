﻿using SoftEngWebEmployee.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftEngWebEmployee.Views
{
    public partial class InventoryUpdate : System.Web.UI.Page
    {
        public string ProductID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ProductID = Request.QueryString["id"];
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

        protected void BtnUpdateProduct_Click(object sender, EventArgs e)
        {

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
            }            
        }
    }
}