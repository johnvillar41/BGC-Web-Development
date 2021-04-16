<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateInformation.aspx.cs" Async="true" Inherits="SoftEngWebEmployee.Views.AddInformation" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Import Namespace="SoftEngWebEmployee.Repository" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .rounded-lg {
            background: #f8e7d1;
        }

        .form-control{           
           width:80%;
           margin-right:10%;
           margin-left:10%;           
        }

        .button {
            background: #eba800;
            color: black;
            box-shadow: none;
            border: initial;
            width: 100%;
        }
    </style>


    <div class="row">

        <h3><b>Update Information</b></h3>

        <hr />


        <div class="container rounded-lg" style="border: 5px solid orange">
            <div class="row">
                <div class="col-lg-4 col-sm-12">
                    <div class="mb-5">
                        <!--
                    <label for="exampleFormControlInput1" class="form-label">Product ID</label>
                    <asp:TextBox ID="ProductIDTextBox" runat="server" CssClass="form-control" ReadOnly></asp:TextBox>
                    -->
                        <%if (DisplayProduct() != null) %>
                        <%{ %>
                        <center><img class="rounded-circle mt-5" src="data:image/png;base64,<%=DisplayProduct().ProductPicture%>" width="200" height="200"></center>
                        <%} %>
                        <div>
                            <br />
                        </div>

                        <fieldset disabled>
                            <div class="mb-3">
                                <center><label for="disabledTextInput" class="form-label"><b>Product Name</b></label></center>
                                <%if (DisplayProduct() != null) %>
                                <%{ %>
                                <center><input type="text" id="disabledTextInput" class="form-control" placeholder="<%=DisplayProduct().ProductName %>"></center>
                                <%} %>
                            </div>
                        </fieldset>
                    </div>

                </div>

                <div class="col-lg-8 col-sm-12">

                    <div class="mt-5">
                        <div class="row">
                            <label for="InformationTextBox" class="form-label" style="margin-right: 10%; margin-left: 10%;">
                                <b>Product Information</b></label>
                            <asp:TextBox ID="InformationTextBox" runat="server" CssClass="form-control" Height="200px" TextMode="MultiLine"></asp:TextBox>
                        </div>

                        <div>
                            <br />
                        </div>
                        <div class="row">
                            <center><asp:Button ID="BtnSubmitInformation" style="width:50%; background-color:darkorange;" CssClass="btn" OnClick="BtnSubmitInformation_Click" runat="server" Text="Save" /></center>
                        </div>

                    </div>


                    <div class="mb-5">
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
