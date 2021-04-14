<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="DisplaySales.aspx.cs" Inherits="SoftEngWebEmployee.Views.DisplaySales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%if (SpecificOrdersList != null)%>
    <%{ %>
    <%foreach (var product in SpecificOrdersList) %>
    <%{ %>
    <p><%=product.ProductID%></p>
    <p><%=product.SpecificOrdersId%></p>
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
</asp:Content>
