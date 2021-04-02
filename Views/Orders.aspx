<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="SoftEngWebEmployee.Views.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <center>              
            <div class="row">
                <div class="col-6">
                    <!--Order List-->
                    <%for (int i = 0; i < 5; i++) %>
                    <%{%>
                    <div class="card mb-3" style="max-width: 540px;">
                        <div class="col-1">
                            <span class="badge bg-success">Finished</span>
                        </div>
                        
                        <div class="row g-0">
                            <div class="col-md-4">
                                <center><img src="/Images/logo.PNG"width="150" height="150" alt="..."></center>
                            </div>
                            <div class="col-md-8">
                                <div class="card-body">
                                    <h5 class="card-title">Card title</h5>
                                    <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
                                    <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%}%>
                </div>
                <div class="col-6">
                    <!--Order List-->
                    <%for (int i = 0; i < 5; i++) %>
                    <%{%>
                    <div class="card mb-3" style="max-width: 540px;">
                        <div class="col-1">
                            <span class="badge bg-danger">Failed</span>
                        </div>
                        <div class="row g-0">
                            <div class="col-md-4">
                                <center><img src="/Images/logo.PNG"width="150" height="150" alt="..."></center>
                            </div>
                            <div class="col-md-8">
                                <div class="card-body">
                                    <h5 class="card-title">Card title</h5>
                                    <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
                                    <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%}%>
                </div>
            </div>
        </center>
    </div>

</asp:Content>
