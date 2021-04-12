<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateInformation.aspx.cs" Async="true" Inherits="SoftEngWebEmployee.Views.AddInformation" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Import Namespace="SoftEngWebEmployee.Repository" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="col-lg-12">
        <div class="row">
            <div class="col-lg-6">
                <%if (DisplayProduct() != null) %>
                <%{ %>
                <h3><b>Update Information for: <%=DisplayProduct().ProductName.ToString() %></b></h3>
                <%} %>
            </div>

        </div>
        <hr />
        <div class="row">
            <div class="col-lg-6">
                <div class="mb-3 ">
                    <label for="exampleFormControlInput1" class="form-label">Product ID</label>
                    <asp:TextBox ID="ProductIDTextBox" runat="server" CssClass="form-control" ReadOnly></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label for="exampleFormControlInput1" class="form-label">Product Information</label>
                    <asp:TextBox ID="InformationTextBox" runat="server" CssClass="form-control" Rows="3" TextMode="MultiLine" required></asp:TextBox>
                </div>
                <asp:Button ID="BtnSubmitInformation" OnClick="BtnSubmitInformation_Click" runat="server" Text="Button" />
            </div>
            <div class="col-lg-3">
            </div>

            <div class="col-lg-3">
            </div>

        </div>
    </div>


</asp:Content>
