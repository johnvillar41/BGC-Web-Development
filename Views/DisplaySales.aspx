<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="DisplaySales.aspx.cs" Inherits="SoftEngWebEmployee.Views.DisplaySales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .vl {
            border-left: 1px solid black;
            height: 200px;
        }
    </style>


    <%if (SpecificOrdersList != null)%>
    <%{ %>
    <%foreach (var product in SpecificOrdersList) %>
    <%{ %>
    <div class="card" style="margin-bottom: 3px">
        <div class="card-header">
            <b>Specific Order ID:</b> <%=product.SpecificOrdersId%>
        </div>
        <div class="card-body">
            <div class="col-12">
                <div class="row">
                    <div class="col-3">
                        <center><img alt="" height="200px" width="200px" src="data:image/jpeg;base64,<%=product.ProductsModel.ProductPicture%>" /></td></center>
                    </div>
                    <div class="vl col-1"></div>
                    <div class="col-8">
                        <h5 class="card-title"><b>Product ID:</b> <%=product.ProductID%></h5>
                        <hr />
                        <p class="card-text"><b>Total Orders:</b> <%=product.TotalOrders%></p>
                        <p class="card-text"><b>Product Name:</b> <%=product.ProductsModel.ProductName%></p>
                        <p class="card-text"><b>Category:</b> <%=product.ProductsModel.ProductCategory%></p>
                        <p class="card-text"><b>Description:</b> <%=product.ProductsModel.ProductDescription%></p>
                        <p class="card-text"><b>Price: </b><%=product.ProductsModel.ProductPrice%></p>
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
    <div class="card" style="margin-bottom: 3px">
        <div class="card-header">
            <b>OnsiteID:</b> <%=product.OnsiteProductTransactionID%>
        </div>
        <div class="card-body">
            <div class="col-12">
                <div class="row">
                    <div class="col-3">
                        <center><img alt="" height="200px" width="200px" src="data:image/jpeg;base64,<%=product.Product.ProductPicture%>" /></td></center>
                    </div>
                    <div class="vl col-1"></div>
                    <div class="col-8">
                        <h5 class="card-title"><b>Product ID:</b> <%=product.Product.Product_ID %></h5>
                        <hr />
                        <p class="card-text"><b>Total Orders:</b> <%=product.TotalProductsCount%></p>
                        <p class="card-text"><b>Product Name: </b><%=product.Product.ProductName%></p>
                        <p class="card-text"><b>Category: </b><%=product.Product.ProductCategory%></p>
                        <p class="card-text"><b>Description: </b><%=product.Product.ProductDescription%></p>
                        <p class="card-text"><b>Price: </b><%=product.Product.ProductPrice%></p>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <%} %>
    <%} %>
</asp:Content>
