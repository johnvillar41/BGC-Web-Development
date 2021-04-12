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
            <div class="row">
                <div class="col-lg-4">
                    <img src="/Images/logo.PNG" width="35" height="35" class="d-inline-block align-top" alt="" />
                </div>
                <div class="col-lg-4">
                    <!--Empty Div-->
                </div>
                <div class="col-lg-4">
                    <button class="btn btn-info" style="float:right">Back</button>
                </div>
                <hr />
            </div>
        </div>
    

</asp:Content>
