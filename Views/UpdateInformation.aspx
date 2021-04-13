<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateInformation.aspx.cs" Async="true" Inherits="SoftEngWebEmployee.Views.AddInformation" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Import Namespace="SoftEngWebEmployee.Repository" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


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
        <div class="row">
            <div class="col-md-4">
                <div class="mb-3">
                    <!--
                    <label for="exampleFormControlInput1" class="form-label">Product ID</label>
                    <asp:TextBox ID="ProductIDTextBox" runat="server" CssClass="form-control" ReadOnly></asp:TextBox>
                    -->

                    <img class="rounded-circle mt-5" src="..." width="200" height="200">
                    <div>
                        <br />
                    </div>

                    <fieldset disabled>
                        <div class="mb-3">
                            <label for="disabledTextInput" class="form-label">Product Name</label>
                            <input type="text" id="disabledTextInput" class="form-control" placeholder="Product Name">
                        </div>
                    </fieldset>


                </div>

                <div class="mb-3">
                </div>
                <div>
                    <br />
                </div>
                <asp:Button ID="BtnSubmitInformation" OnClick="BtnSubmitInformation_Click" runat="server" Text="Save" />
            </div>


            <div class="col-lg-3">
                <label for="exampleFormControlInput1" class="form-label">Product Information</label>
                    <asp:TextBox ID="InformationTextBox" runat="server" CssClass="form-control" Rows="3" TextMode="MultiLine" required></asp:TextBox>

            </div>

            <div class="col-lg-3">
         
            </div>


        </div>
    </div>


</asp:Content>
