<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrators.aspx.cs" Inherits="SoftEngWebEmployee.Views.Administrators" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="container">
        <div class="row">

            <!--Add Dynamic Data here-->
            <%for (int i = 0; i < 5; i++)%>
            <%{%>
            <div class="card" style="margin:10px">
                <h5 class="card-header">Administrator ID Here</h5>
                <div class="card-body">
                    <h5 class="card-title">Administrator Username</h5>
                    <img src="/Images/logo.PNG" class="img-thumbnail" alt="..." width="100" height="100">
                    <p class="card-text">With supporting text below as a natural lead-in to additional content.</p>
                    <a href="#" class="btn btn-danger"><i class="fa fa-exclamation-circle" style="color:black"></i>Remove</a>
                    <a href="#" class="btn btn-warning"><i class="fa fa-check-square" style="color:black"></i>Update</a>
                </div>
            </div>
            <%}%>
        </div>

    </div>

</asp:Content>
