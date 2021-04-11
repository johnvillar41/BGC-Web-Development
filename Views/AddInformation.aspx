<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddInformation.aspx.cs" Async="true" Inherits="SoftEngWebEmployee.Views.AddInformation" %>
<%@ Import Namespace="SoftEngWebEmployee.Repository" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>

    <div class="container">
        <div class="col-12">
            <div class="row">
                <div class="col-6">
                    <div class="card" style="width: 18rem;">
                        <center><img src="/Images/logo.PNG" class="card-img-top" style="height:200px; width:200px;"></center>
                        <hr />
                        <div class="card-body">        
                            <%if (DisplayProduct() != null) %>
                            <%{ %>
                            <h5 class="card-title"><%=DisplayProduct().ProductName.ToString() %></h5>
                            <p class="card-text"><%=DisplayInformation() %></p>
                            <%} %>                           
                            
                            <a href="#" class="btn btn-primary">Go somewhere</a>
                        </div>
                    </div>
                </div>
                <div class="col-6">
                </div>
            </div>
        </div>
    </div>




</asp:Content>
