<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="DisplaySales.aspx.cs" Inherits="SoftEngWebEmployee.Views.DisplaySales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%if (SpecificOrdersList != null)%>
    <%{ %>
    <%foreach (var product in SpecificOrdersList) %>
    <%{ %>
    <p><%=product.ProductsModel.ProductName%></p>
    <p><%=product.ProductsModel.ProductCategory%></p>
    <p><%=product.OrdersID%></p>
    <br />
    <%} %>
    <%} %>
</asp:Content>
