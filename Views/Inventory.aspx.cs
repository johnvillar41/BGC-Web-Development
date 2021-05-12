﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftEngWebEmployee.Models;
using SoftEngWebEmployee.Repository;
using SoftEngWebEmployee.Helpers;

namespace SoftEngWebEmployee.Views
{
    public partial class Inventory : System.Web.UI.Page
    {
        public List<ProductModel> listSearchRepeater {get; set;}
        public List<ProductModel> listGHRepeater { get; set; }
        public List<ProductModel> listHPRepeater { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                DisplayInventoryTables();
            }
        }

        private async void LoadCategories()
        {
            var categories = await ProductRepository.SingleInstance.FetchAllCategories();
            CategoryRepeater.DataSource = categories;
            CategoryRepeater.DataBind();
        }

        private async void DisplayInventoryTables()
        {
            var inventory = await ProductRepository.SingleInstance.FetchAllProducts();            
            SearchRepeater.DataSource = inventory;
            listSearchRepeater = inventory;
            SearchRepeater.DataBind();

            var greenhouse = await ProductRepository.SingleInstance.FetchGHProducts();
            GHRepeater.DataSource = greenhouse;
            listGHRepeater = greenhouse;
            GHRepeater.DataBind();

            var hydroponics = await ProductRepository.SingleInstance.FetchHPProducts();
            HPRepeater.DataSource = hydroponics;
            listHPRepeater = hydroponics;
            HPRepeater.DataBind();
        }

        protected async void Category_Click(object sender, EventArgs e)
        {
            string category = (sender as Button).Text.ToString();
            char caret = Convert.ToChar(0x000025BC);
            dropdownMenuReference1.Text = category+" "+caret;
            if (category=="All Products")
            {
                var newSearch = await ProductRepository.SingleInstance.FetchAllProducts();
                SearchRepeater.DataSource = newSearch;
                listSearchRepeater = newSearch;
                SearchRepeater.DataBind();
            }
            else
            {
                var newSearch = await ProductRepository.SingleInstance.FetchOnCategory(category);
                SearchRepeater.DataSource = newSearch;
                listSearchRepeater = newSearch;
                SearchRepeater.DataBind();
            }           
        }

        protected async void SearchButton_Click(object sender, EventArgs e)
        {
            string search = searchBox.Text.ToString();
            var newSearch = await ProductRepository.SingleInstance.FetchOnSearch(search);
            SearchRepeater.DataSource = newSearch;
            listSearchRepeater = newSearch;
            SearchRepeater.DataBind();
        }        

        protected void CategoryRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            Button button = e.Item.FindControl("category") as Button;

            ScriptManager current = ScriptManager.GetCurrent(Page);
            if (current != null)
                current.RegisterAsyncPostBackControl(button);
        }

        protected async void RetrieveDetails(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var productID = button.CommandArgument.ToString();
            ProductModel Details = await ProductRepository.SingleInstance.FetchProductDetails(productID);

            List<ProductModel> ProductDetail = new List<ProductModel>
            {
                Details
            };

            DetailsRepeater.DataSource = ProductDetail;
            DetailsRepeater.DataBind();
            DeleteRepeater.DataSource = ProductDetail;
            DeleteRepeater.DataBind();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var productID = button.CommandArgument.ToString();
            ProductRepository.SingleInstance.DeleteProduct(productID);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#deleteModal').modal('hide');", true);

            DisplayInventoryTables();

            SweetAlertBuilder sweetAlertBuilder = new SweetAlertBuilder
            {
                HexaBackgroundColor = "#fff",
                Title = "Product Deleted!",
                AlertIcons = Constants.AlertStatus.success,
                ShowCloseButton = true,
                AlertPositions = Constants.AlertPositions.CENTER
            };
            sweetAlertBuilder.BuildSweetAlert(this);
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {

        }

        /* C# code for displaying card details

        <p> Are you sure you want to delete this product? </p>
        <p> Name: <%if (Details != null){%> <%=Details.ProductName %> <%}%> </p>
        <asp:Label runat="server" ID="modalID"> <%if (Details != null){%> <%=Details.Product_ID %> <%}%></asp:Label>
        <p> ID: <%if (Details != null){%> <%=Details.Product_ID %> <%}%> </p>
        <p> Description: <%if (Details != null){%> <%=Details.ProductDescription %> <%}%> </p>
        <p> Category: <%if (Details != null){%> <%=Details.ProductCategory %> <%}%> </p>
        <p> Picture: <%if (Details != null){%> <%=Details.ProductPicture %> <%}%> </p>
        <p> Number of Stocks: <%if (Details != null){%> <%=Details.ProductStocks %> <%}%> </p>
        <p> Price: Php <%if (Details != null){%> <%=Details.ProductPrice %> <%}%> </p>

        Code for Lottie
        <%if (SearchRepeater.DataBind() == "") %>
                <%{ %>
                <center><h3 style="color:white">No Items Found</h3></center>
                <center><lottie-player src="https://assets4.lottiefiles.com/temp/lf20_Celp8h.json" background="transparent"  speed="1"  style="width: 300px; height: 300px;"loop autoplay></lottie-player></center>
                <%} %>
        */
    }
}