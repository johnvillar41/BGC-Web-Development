﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using SoftEngWebEmployee.Helpers;
using System.IO;

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

        protected async void UploadImage_Click(object sender, EventArgs e)
        {
            Stream fs = ProfileFileUpload.PostedFile.InputStream;
            await ProductRepository.SingleInstance.UpdateProductPictureAsync(fs);
            Response.Redirect(Request.RawUrl, false);
        }
    }
}