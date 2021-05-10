﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeBehind="Reports.aspx.cs" Inherits="SoftEngWebEmployee.Views.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p><b><u>REPORTS</u></b></p>
    <div class="row">
        <div class="col-lg-2 col-md-12 col-sm-12 mb-3">
            <div class="list-group" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                <button class="list-group-item list-group-item-action active" id="v-pills-dashboard-tab" data-bs-toggle="pill" data-bs-target="#v-pills-dashboard" type="button" role="tab" aria-controls="v-pills-dashboard" aria-selected="true">Dashboard</button>
                <button class="list-group-item list-group-item-action" id="v-pills-products-tab" data-bs-toggle="pill" data-bs-target="#v-pills-products" type="button" role="tab" aria-controls="v-pills-products" aria-selected="false">Product Sales Report</button>
                <button class="list-group-item list-group-item-action" id="v-pills-sales-tab" data-bs-toggle="pill" data-bs-target="#v-pills-sales" type="button" role="tab" aria-controls="v-pills-sales" aria-selected="false">Sales Income Report</button>
            </div>
        </div>

        <div class="col-lg-10 col-md-12 col-sm-12">
            <div class="tab-content" id="v-pills-tabContent">
                <!--Dashboard-->
                <div class="tab-pane fade show active" id="v-pills-dashboard" role="tabpanel" aria-labelledby="v-pills-dashboard-tab">
                    <div class="row">
                        <div class="col-lg-4 col-md-6 col-sm-12 mb-1">
                            <div class="card">
                                <div class="card-body">

                                    <center><asp:Label ID="total_sales" runat="server" Text="Total Sales" CssClass="card-title" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                                    <br />

                                    <h5 class="card-subtitle mb-2 text-muted">Total Sales</h5>
                                    <img src="/Images/growth.PNG" height="150" width="150" class="img-thumbnail" alt="..."></center>


                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6 col-sm-12 mb-1">
                            <div class="card">
                                <div class="card-body">

                                    <center><asp:Label ID="total_inventory" runat="server" Text="Total Inventory" CssClass="card-title" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                                    <h5 class="card-subtitle mb-2 text-muted">Total Inventory</h5>
                                    <img src="/Images/invoice.PNG" height="150" width="150" class="img-thumbnail" alt="..."></center>

                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-12 col-sm-12 mb-1">
                            <div class="card" style="min-width: 16rem;">
                                <div class="card-body">

                                    <center><asp:Label ID="total_products" runat="server" Text="Total Products" CssClass="card-title" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                                    <h5 class="card-subtitle mb-2 text-muted">Total Products</h5>
                                    <img src="/Images/plant.PNG" height="150" width="150" class="img-thumbnail" alt="..."></center>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="tab-pane fade" id="v-pills-sales" role="tabpanel" aria-labelledby="v-pills-sales-tab">
                    <!--Sales Income-->

                    <div class="row">
                        <!--1st card-->
                        <div class="col-lg-4 col-md-6 col-sm-12">
                            <div class="card border-success mb-3" style="min-height: 16rem;">
                                <div class="card-header"><b>Date Today: <%=DateTime.Now.ToShortDateString() %></b></div>
                                <div class="card-body text-success">
                                    <h5 class="card-title"><b>Total Sales Today:</b></h5>
                                    <p>
                                        <asp:Label ID="TotalSale" runat="server" Text="None" Font-Size="X-Large"></asp:Label>
                                    </p>

                                    <a class="btn btn-primary" data-bs-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">See More
                                    </a>
                                    <!--See More-->
                                    <div class="collapse mt-3" id="collapseExample">
                                        <div class="card card-body">
                                            <asp:Label ID="Label1" runat="server" Text="Onsite Orders"></asp:Label>
                                            <div class="row">
                                                <p><%=TotalSaleOnsite.ToString() %></p>
                                            </div>
                                            <br />
                                            <asp:Label ID="Label2" runat="server" Text="Online Orders"></asp:Label>
                                            <div class="row">
                                                <p><%=TotalSaleOrder.ToString() %></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end 1st card-->

                        </div>

                        <div class="col-lg-4 col-md-6 col-sm-12">
                            <div class="card border-success mb-3" style="min-height: 16rem;">
                                <div class="card-header">
                                    <label for="date"><b>Select Date:</b></label>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <asp:TextBox ID="Date" type="date" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Button ID="FindDate" runat="server" Text="Find" CssClass="btn btn-info" OnClick="FindDate_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="card-body text-success">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <h5 class="card-title"><b>Total Sales:</b></h5>
                                            <p>
                                                <asp:Label ID="TotalSaleGivenDate" runat="server" Text="0" Font-Size="X-Large"></asp:Label>
                                            </p>
                                        </ContentTemplate>
                                        <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="FindDate" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <a class="btn btn-primary" data-bs-toggle="collapse" href="#collapseExample2" role="button" aria-expanded="false" aria-controls="collapseExample">See More
                                    </a>
                                    <!--See More-->
                                    <div class="collapse mt-3" id="collapseExample2">
                                        <div class="card card-body">
                                            <asp:Label ID="Label5" runat="server" Text="Onsite Orders"></asp:Label>
                                            <div class="progress">
                                                <div class="progress-bar" role="progressbar" aria-valuenow="70"
                                                    aria-valuemin="0" aria-valuemax="100" style="width: 70%">
                                                    256
                                                </div>
                                            </div>
                                            <br />
                                            <asp:Label ID="Label6" runat="server" Text="Online Orders"></asp:Label>
                                            <div class="progress">
                                                <div class="progress-bar" role="progressbar" aria-valuenow="70"
                                                    aria-valuemin="0" aria-valuemax="100" style="width: 20%">
                                                    24
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>                                
                        </div>
                        <div class="col-lg-4 col-md-12 col-sm-12">
                            <div class="card border-success mb-3" style="min-height: 16rem;">
                                <div class="card-header"><b>Average Sales</b></div>
                                <div class="card-body text-success">
                                    <label for="term"><b>Term: &nbsp;</b></label>
                                    <select name="term" id="term">
                                        <option value="daily">Daily</option>
                                        <option value="weekly">Weekly</option>
                                        <option value="monthly">Monthly</option>
                                        <option value="annually">Annually</option>
                                    </select>
                                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <%if (SalesIncomeDisplay == null) return; %>
                            <%foreach (var sales in SalesIncomeDisplay) %>
                            <%{ %>
                            <div class="card mb-2">
                                <center><img src="data:image/jpeg;base64,<%=sales.Administrator.ProfilePicture %>" class="card-img-top" style="height:200px; width:200px"></center>
                                <hr />
                                <div class="card-body">
                                    <h5 class="card-title">Username: <%=sales.Administrator.Username %></h5>
                                    <h5 class="card-title">FullName: <%=sales.Administrator.Fullname %></h5>
                                    <p class="card-text">Total Sale: <%=sales.TotalSale %></p>
                                    <p class="card-text">Total Sale On Site: <%=sales.TotalSaleOnsite %></p>
                                    <p class="card-text">Total Sale Orders: <%=sales.TotalSaleOrders %></p>
                                    <a href="#" class="btn btn-primary">Go somewhere</a>
                                </div>
                            </div>
                            <%} %>
                        </div>
                    </div>



                </div>
                <div class="tab-pane fade" id="v-pills-products" role="tabpanel" aria-labelledby="v-pills-products-tab">
                    <!-- Products -->
                    <div class="row row-cols-lg-3 row-cols-md-2 row-cols-sm-1 row-cols-1 g-4">
                        <%if (ProductSalesListDisplay == null) return; %>
                        <%foreach (var productSales in ProductSalesListDisplay) %>
                        <%{ %>

                        <div class="col">

                            <div class="card">
                                <img src="data:image/jpeg;base64,<%=productSales.ProductReport.Product.ProductPicture%>" class="card-img-top img-thumbnail" style="min-width: 200px; height: 300px">
                                <div class="card-body">
                                    <h5 class="card-title">Product ID: <%=productSales.ProductReport.Product.Product_ID%></h5>
                                    <p class="card-text"><b><%=productSales.ProductReport.Product.ProductName %></b></p>
                                    <ul class="list-group list-group-flush">
                                        <li class="list-group-item">Unit Price:
                                        <label><%=productSales.ProductReport.Product.ProductPrice%></label>
                                        </li>
                                        <li class="list-group-item">Product Revenue:
                                         <label><%=productSales.ProductReport.ProductRevenue %></label>

                                        </li>
                                        <!--See More-->
                                        <li class="list-group-item">Quantity Sold:                                       
                                       <label><%=productSales.ProductReport.QuantitySold %></label>
                                            <a class="btn btn-primary" data-bs-toggle="collapse" href="#collapse<%=productSales.ProductReport.Product.Product_ID %>" role="button" aria-expanded="false" aria-controls="collapseExample">See More
                                            </a>
                                            <div class="collapse mt-2" id="collapse<%=productSales.ProductReport.Product.Product_ID %>">
                                                <div class="card card-body table table-striped table-hover table-responsive">
                                                    <table>
                                                        <thead>
                                                            <tr>
                                                                <th scope="col">ProductName</th>
                                                                <th scope="col">ProductPrice</th>
                                                                <th scope="col">ProductID</th>
                                                                <th scope="col">SaleType</th>
                                                                <th scope="col">Date</th>
                                                                <th scope="col">Administrator</th>
                                                                <th scope="col">TotalSale</th>
                                                                <th scope="col">ProductCount</th>
                                                            </tr>
                                                        </thead>
                                                        <%foreach (var quantitySold in productSales.QuantitySold) %>
                                                        <%{ %>
                                                        <tbody>
                                                            <tr>
                                                                <td><%=quantitySold.ProductName %></td>
                                                                <td><%=quantitySold.ProductPrice %></td>
                                                                <td><%=quantitySold.ProductID %></td>
                                                                <td><%=quantitySold.SaleType %></td>
                                                                <td><%=quantitySold.Date %></td>
                                                                <td><%=quantitySold.Administrator %></td>
                                                                <td><%=quantitySold.TotalSale %></td>
                                                                <td><%=quantitySold.ProductCount %></td>
                                                            </tr>
                                                        </tbody>
                                                        <%} %>
                                                    </table>
                                                </div>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <%} %>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
