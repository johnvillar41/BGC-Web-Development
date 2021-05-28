<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="DisplaySales.aspx.cs" Inherits="SoftEngWebEmployee.Views.DisplaySales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/LoginCss.css" />
    <style>
        .vl {
            border-left: 1px solid black;
            height: 200px;
        }
    </style>
    <div class="card card0">
        <div class="row d-flex">
            <div class="col-xl-8 col-lg-8 col-md-6 col-12">
                <div class="card1 px-2 py-2">

                    <%if (SpecificOrdersList != null)%>
                    <%{ %>
                    <%foreach (var product in SpecificOrdersList) %>
                    <%{ %>
                    <b>Specific Order ID:</b> <%=product.SpecificOrdersId%>
                    <div class="card mb-3" style="max-width: 540px;">
                        <div class="row g-0">
                            <div class="col-md-4">
                                <center><img alt="" height="200px" width="200px" src="data:image/jpeg;base64,<%=product.ProductsModel.ProductPicture%>" /></td></center>
                            </div>
                            <div class="col-md-8">
                                <div class="card-body">
                                    <h5 class="card-title"><b>Product ID:</b> <%=product.ProductID%></h5>
                                    <hr />
                                    <p class="card-text"><b>Total Orders:</b> <%=String.Format("{0:n0}",product.TotalOrders)%></p>
                                    <p class="card-text"><b>Product Name:</b> <%=product.ProductsModel.ProductName%></p>
                                    <p class="card-text"><b>Category:</b> <%=product.ProductsModel.ProductCategory%></p>
                                    <p class="card-text"><b>Description:</b> <%=product.ProductsModel.ProductDescription%></p>
                                    <p class="card-text"><b>Price: </b><%=String.Format("{0:n0}",product.ProductsModel.ProductPrice)%></p>
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
                    <b>OnsiteID:</b> <%=product.OnsiteProductTransactionID%>
                    <div class="card mb-3" style="max-width: 540px;">
                        <div class="row g-0">
                            <div class="col-md-4">
                                <center><img alt="" height="200px" width="200px" src="data:image/jpeg;base64,<%=product.Product.ProductPicture%>" /></td></center>
                            </div>
                            <div class="col-md-8">
                                <div class="card-body">
                                    <h5 class="card-title"><b>Product ID:</b> <%=product.Product.Product_ID %></h5>
                                    <hr class="invisible" />
                                    <p class="card-text"><b>Total Orders:</b> <%=product.TotalProductsCount%></p>
                                    <p class="card-text"><b>Product Name: </b><%=product.Product.ProductName%></p>
                                    <p class="card-text"><b>Category: </b><%=product.Product.ProductCategory%></p>
                                    <p class="card-text"><b>Description: </b><%=product.Product.ProductDescription%></p>
                                    <p class="card-text"><b>Price: </b><%=product.Product.ProductPrice%></p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%} %>
                    <%} %>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
