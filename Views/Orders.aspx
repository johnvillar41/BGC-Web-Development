<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="SoftEngWebEmployee.Views.Orders" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .bd-callout {
            padding: 1.25rem;
            border: 1px solid #e9ecef;
            border-left-width: .25rem;
            border-radius: .25rem;
        }

        .bd-callout-warning {
            border-left-color: #f0ad4e;
        }
    </style>
    <div class="container">
        <%foreach (var orders in DisplayOrders()) %>
        <%{%>
        <div class="blog-nav d-flex flex-column flex-sm-row" style="margin-bottom: 5px">

            <div class="col-lg-3 col-md-6 col-sm-12">
                <div class="card text-white bg-secondary mb-3" style="max-width: 18rem;">
                    <%if (orders.OrderStatus.Equals("Finished")) %>
                    <%{ %>
                    <span class="badge bg-success"><%=orders.OrderStatus %></span>
                    <%} %>
                    <%else %>
                    <%{ %>
                    <span class="badge bg-danger"><%=orders.OrderStatus %></span>
                    <%} %>
                    <div class="card-header">
                        Order ID: <%=orders.Order_ID %>
                    </div>

                    <div class="card-body">
                        <p class="card-text">Customer Name: <%=orders.CustomerName %></p>
                        <p class="card-text">Customer Email: <%=orders.CustomerEmail %></p>
                        <p class="card-text">Order Date: <%=orders.OrderDate %></p>
                        <p class="card-text">Total Number Of Orders: <%=orders.TotalNumberOfOrders %></p>
                    </div>
                    <div class="card-footer">Total Price: <%=orders.OrderTotalPrice %></div>
                </div>
            </div>

            <div class="col-lg-9 col-md-6 col-sm-12">
                <div class="card bd-callout bd-callout-warning" style="border-radius: .25rem">

                    <h1>Orders</h1>
                    <div class="table-bordered table-condensed table-responsive">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th scope="col">Order ID</th>
                                    <th scope="col">Product ID</th>
                                    <th scope="col">Product Name</th>
                                    <th scope="col">Product Price</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%for (int i = 0; i < orders.SpecificOrdersModel.Count(); i++) %>
                                <%{ %>
                                <tr>
                                    <th scope="row"><%=orders.SpecificOrdersModel[i].OrdersID %></th>
                                    <td><%=orders.SpecificOrdersModel[i].ProductID %></td>
                                    <td><%=orders.SpecificOrdersModel[i].ProductsModel.ProductName %></td>
                                    <td><%=orders.SpecificOrdersModel[i].ProductsModel.ProductPrice %></td>
                                </tr>
                                <%} %>
                            </tbody>

                        </table>
                        <nav aria-label="Page navigation example">
                            <ul class="pagination">
                                <li class="page-item"><a class="page-link" href="#">Previous</a></li>                                
                                <li class="page-item"><a class="page-link" href="#">Next</a></li>
                            </ul>
                        </nav>
                    </div>
                </div>
            </div>
        </div>
        <%}%>
    </div>

</asp:Content>
