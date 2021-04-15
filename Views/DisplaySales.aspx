<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="DisplaySales.aspx.cs" Inherits="SoftEngWebEmployee.Views.DisplaySales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%if (SpecificOrdersList != null)%>
    <%{ %>
    <%foreach (var product in SpecificOrdersList) %>
    <%{ %>
    <p>Order ID: <%=product.SpecificOrdersId%></p>
    <p><%=product.ProductID%></p>    
    <p><%=product.TotalOrders%></p>
    <hr />
    <p><%=product.ProductsModel.ProductName%></p>
    <p><%=product.ProductsModel.ProductCategory%></p>
    <p><%=product.ProductsModel.ProductDescription%></p>
    <p><%=product.ProductsModel.ProductPrice%></p>
    <p><%=product.ProductsModel.ProductPicture%></p>
    <br />
    <%} %>
    <%} %>
    <%else if (OnSiteProducts != null) %>
    <%{ %>
    <%foreach (var product in OnSiteProducts) %>
    <%{ %>
    <p>OnsiteID: <%=product.OnsiteProductTransactionID%></p>
    <p><%=product.TotalProductsCount%></p>
    <p><%=product.TransactionID%></p>
    <hr />
    <p><%=product.Product.ProductName%></p>
    <p><%=product.Product.ProductCategory%></p>
    <p><%=product.Product.ProductDescription%></p>
    <p><%=product.Product.ProductPrice%></p>
    <p><%=product.Product.ProductPicture%></p>
    <br />
    <%} %>
    <%} %>
</asp:Content>
