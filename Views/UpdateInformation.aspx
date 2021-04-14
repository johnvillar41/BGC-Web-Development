<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateInformation.aspx.cs" Async="true" Inherits="SoftEngWebEmployee.Views.AddInformation" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Import Namespace="SoftEngWebEmployee.Repository" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .rounded-lg{
            background: #f8e7d1;

        }
    </style>


    <div class="col-lg-12">
        <div class="row">
            <div class="col-lg-6">
                <%if (DisplayProduct() != null) %>
                <%{ %>
                <!--<h3><b>Update Information for: <%=DisplayProduct().ProductName.ToString() %></b></h3>-->
                <h3><b>Update Information</b></h3>
                <%} %>
            </div>
        </div>
        <hr />

        <div class="container rounded-lg" style="border: 5px solid orange">
            <div class="row">
                <div class="col-md-4">
                    <div class="mb-5">
                        <!--
                    <label for="exampleFormControlInput1" class="form-label">Product ID</label>
                    <asp:TextBox ID="ProductIDTextBox" runat="server" CssClass="form-control" ReadOnly></asp:TextBox>
                    -->

                        <center><img class="rounded-circle mt-5" src="..." width="200" height="200"></center>
                        <div>
                            <br />
                        </div>

                        <fieldset disabled>
                            <div class="mb-3">
                                <center><label for="disabledTextInput" class="form-label"><b>Product Name</b></label></center>
                                <center><input type="text" id="disabledTextInput" class="form-control" placeholder="Product Name"></center>
                            </div>
                        </fieldset>
                    </div>

                </div>

                <div class="col-md-4">
                    <div class="mb-5">
                    </div>

                    <label for="exampleFormControlInput1" class="form-label"><b>Product Information</b></label>
                    <asp:TextBox ID="InformationTextBox" runat="server" CssClass="form-control" Rows="7" TextMode="MultiLine"></asp:TextBox>
                    <div>
                        <br />
                    </div>
                    <asp:Button ID="BtnSubmitInformation" OnClick="BtnSubmitInformation_Click" runat="server" Text="Save" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
