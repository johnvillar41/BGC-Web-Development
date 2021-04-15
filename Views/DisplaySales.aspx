<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="DisplaySales.aspx.cs" Inherits="SoftEngWebEmployee.Views.DisplaySales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%if (SpecificOrdersList != null)%>
    <%{ %>
    <%foreach (var product in SpecificOrdersList) %>
    <%{ %>
    <div class="card">
        <div class="card-header">
            Specific Order ID: <%=product.SpecificOrdersId%>
        </div>
        <div class="card-body">
            <div class="col-12">
                <div class="row">
                    <div class="col-3">
                        <center><img alt="" height="100px" width="100px" src="data:image/jpeg;base64,<%=product.ProductsModel.ProductPicture%>" /></td></center>
                    </div>
                    <div class="col-9">
                        <h5 class="card-title">Product ID: <%=product.ProductID%></h5>
                        <p class="card-text">Total Orders: <%=product.TotalOrders%></p>
                        <p class="card-text"><%=product.ProductsModel.ProductName%></p>
                        <p class="card-text"><%=product.ProductsModel.ProductCategory%></p>
                        <p class="card-text"><%=product.ProductsModel.ProductDescription%></p>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <%} %>
    <%} %>
    <%else if (OnSiteProducts != null) %>
    <%{ %>
    <%foreach (var product in OnSiteProducts) %>
    <%{ %>
    <div class="card">
        <div class="card-header">
           OnsiteID: <%=product.OnsiteProductTransactionID%>
        </div>
        <div class="card-body">
            <div class="col-12">
                <div class="row">
                    <div class="col-3">
                        <center><img alt="" height="100px" width="100px" src="data:image/jpeg;base64,<%=product.Product.ProductPicture%>" /></td></center>
                    </div>
                    <div class="col-9">
                        <h5 class="card-title">Product ID: <%=product.Product.Product_ID %></h5>
                        <p class="card-text">Total Orders: <%=product.TotalProductsCount%></p>
                        <p class="card-text"><%=product.Product.ProductName%></p>
                        <p class="card-text"><%=product.Product.ProductCategory%></p>
                        <p class="card-text"><%=product.Product.ProductDescription%></p>
                        <p class="card-text"><%=product.Product.ProductPrice%></p>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <%} %>
    <%} %>
</asp:Content>
