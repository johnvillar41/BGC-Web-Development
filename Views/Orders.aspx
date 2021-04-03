<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="SoftEngWebEmployee.Views.Orders" Async="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <center>              
            <div class="row">
                <div class="col-6">
                    <!--Order List-->                   
                    <%for (int i = 0; i < DisplayOrders().Count(); i++) %>
                    <%{%>
                   
                       
                           <%if (DisplayOrders()[i].OrderStatus.Equals("Finished")) %>
                           <%{%>
                                <div class="card text-white bg-success mb-3" style="max-width: 18rem;">
                                <span class="badge badge-pill badge-success">Success</span>
                           <%}%>
                           <%else %>
                           <%{ %>
                                <div class="card text-white bg-danger mb-3" style="max-width: 18rem;">
                                <span class="badge badge-pill badge-warning">Failed</span>
                           <%} %>                          
                          <div class="card-header">Order ID: <%=DisplayOrders()[i].Order_ID %></div>
                          <div class="card-body">
                              <div class="container">
                                  <div class="row">
                                       <div class="col-6">
                                          <h5 class="card-title">Total Price</h5>
                                      </div>
                                      <div class="col-6">
                                          <p class="card-title"><%=DisplayOrders()[i].OrderTotalPrice %></p>
                                      </div>
                                  </div>
                                  <div class="row">
                                      <hr class="dropdown-divider">
                                  </div>
                                   <div class="row">
                                       <div class="col-6">
                                          <h5 class="card-title">Customer Name</h5>
                                      </div>
                                      <div class="col-6">
                                          <p class="card-title"><%=DisplayOrders()[i].CustomerName %></p>
                                      </div>
                                  </div>
                                   <div class="row">
                                      <hr class="dropdown-divider">
                                  </div>
                                   <div class="row">
                                       <div class="col-6">
                                          <h5 class="card-title">Customer Email</h5>
                                      </div>
                                      <div class="col-6">
                                          <p class="card-title"><%=DisplayOrders()[i].CustomerEmail %></p>
                                      </div>
                                  </div>
                                   <div class="row">
                                      <hr class="dropdown-divider">
                                  </div>
                                   <div class="row">
                                       <div class="col-6">
                                          <h5 class="card-title">Order Date</h5>
                                      </div>
                                      <div class="col-6">
                                          <p class="card-title"><%=DisplayOrders()[i].OrderDate %></p>
                                      </div>
                                  </div>
                                   <div class="row">
                                      <hr class="dropdown-divider">
                                  </div>
                                   <div class="row">
                                       <div class="col-6">
                                          <h5 class="card-title">Total Number Of Orders</h5>
                                      </div>
                                      <div class="col-6">
                                          <p class="card-title"><%=DisplayOrders()[i].TotalNumberOfOrders %></p>
                                      </div>
                                  </div>
                                  <div class="row">                                    
                                      <div class="col-12">
                                          <button class="btn btn-info form-control">Check Orders</button>
                                      </div>
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
