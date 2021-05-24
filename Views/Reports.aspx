<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeBehind="Reports.aspx.cs" Inherits="SoftEngWebEmployee.Views.Reports" %>

<%@ Import Namespace="SoftEngWebEmployee.Helpers" %>
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
                    <%if (UserSession.SingleInstance.IsAdministrator()) %>
                    <%{ %>
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
                    <%} %>
                    <%else %>
                    <%{ %>
                    <center><lottie-player src="https://assets1.lottiefiles.com/packages/lf20_LlRvIg.json" background="transparent" speed="1" style="width: 400px; height: 400px;" loop autoplay></lottie-player></center>
                    <center><h3><b>Sorry you are not allowed to view this.</b></h3></center>
                    <%} %>
                </div>

                <div class="tab-pane fade" id="v-pills-sales" role="tabpanel" aria-labelledby="v-pills-sales-tab">
                    <!--Sales Income-->
                    <%if (UserSession.SingleInstance.IsAdministrator()) %>
                    <%{ %>
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
                                            <a class="btn btn-primary" data-bs-toggle="collapse" href="#collapseExample2" role="button" aria-expanded="false" aria-controls="collapseExample">See More
                                            </a>
                                            <!--See More-->
                                            <div class="collapse mt-3" id="collapseExample2">
                                                <div class="card card-body">
                                                    <asp:Label ID="Label5" runat="server" Text="Onsite Orders"></asp:Label>
                                                    <div class="row">
                                                        <p><%=TotalSaleOnsite_GivenDate.ToString() %></p>
                                                    </div>
                                                    <br />
                                                    <asp:Label ID="Label3" runat="server" Text="Online Orders"></asp:Label>
                                                    <div class="row">
                                                        <p><%=TotalSaleOrder_GivenDate.ToString() %></p>
                                                    </div>
                                                </div>
                                            </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="FindDate" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
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
                                        <p class="card-text">
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="row row-cols-1 row-cols-md-3 g-4">
                                <%if (SalesIncomeDisplay == null) return; %>
                                <%foreach (var sales in SalesIncomeDisplay) %>
                                <%{ %>
                                <div class="col">
                                    <div class="card mb-2">
                                        <center><img src="data:image/jpeg;base64,<%=sales.Administrator.ProfilePicture %>" class="card-img-top" style="height:200px;"></center>
                                        <hr />
                                        <div class="card-body">
                                            <h5 class="card-title">Username: <%=sales.Administrator.Username %></h5>
                                            <h5 class="card-title">FullName: <%=sales.Administrator.Fullname %></h5>
                                            <p class="card-text">Total Sale: <%=sales.TotalSale %></p>
                                            <p class="card-text">Total Sale On Site: <%=sales.TotalSaleOnsite %></p>
                                            <p class="card-text">Total Sale Orders: <%=sales.TotalSaleOrders %></p>
                                            <a href="#" class="btn btn-primary">See more</a>
                                        </div>
                                    </div>
                                </div>
                                <%} %>
                            </div>
                        </div>



                    </div>

                    <div class="tab-pane fade" id="v-pills-products" role="tabpanel" aria-labelledby="v-pills-products-tab">
                        <!-- Product sales report -->
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
                                                <br />
                                                <a class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modal<%=productSales.ProductReport.Product.Product_ID %>" role="button">See More
                                                </a>
                                                <!-- Modals -->
                                                <div class="modal fade" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true" id="modal<%=productSales.ProductReport.Product.Product_ID %>">


                                                    <div class="modal-dialog modal-xl">

                                                        <div class="modal-content">

                                                            <div class="modal-header">
                                                                <h5 class="modal-title" id="exampleModalLabel">List of product transactions</h5>
                                                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                                            </div>
                                                            <ul class="nav nav-tabs" id="trans" role="tablist">
                                                                <li class="nav-item">
                                                                    <a class="nav-link" id="view-orders" data-toggle="tab" href="#order<%=productSales.ProductReport.Product.Product_ID%>" role="tab" aria-controls="orders" aria-selected="false">View Orders List</a>
                                                                </li>
                                                                <li class="nav-item">
                                                                    <a class="nav-link active" id="view-onsites" data-toggle="tab" href="#onsite<%=productSales.ProductReport.Product.Product_ID%>" role="tab" aria-controls="onsites" aria-selected="true">View Onsites List</a>
                                                                </li>
                                                            </ul>
                                                            <div class="modal-body">
                                                                <div class="tab-content" id="myTabContent">
                                                                    <div class="tab-pane fade show active" id="onsite<%=productSales.ProductReport.Product.Product_ID%>" role="tabpanel" aria-labelledby="view-onsites">
                                                                        <div class="card card-body table table-striped table-hover table-responsive" style="height: 600px">
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
                                                                                <%foreach (var quantitySold in productSales.QuantitySold_Onsite) %>
                                                                                <%{ %>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td><%=quantitySold.ProductName %></td>
                                                                                        <td><%=quantitySold.ProductPrice %></td>
                                                                                        <td><%=quantitySold.ProductID %></td>
                                                                                        <td><%=quantitySold.SaleType %></td>
                                                                                        <td><%=quantitySold.Date %></td>
                                                                                        <td><%=quantitySold.Administrator %></td>
                                                                                        <td><%=quantitySold.ProductCount * quantitySold.ProductPrice %></td>
                                                                                        <td><%=quantitySold.ProductCount %></td>
                                                                                    </tr>
                                                                                </tbody>
                                                                                <%} %>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                    <div class="tab-pane fade" id="order<%=productSales.ProductReport.Product.Product_ID%>" role="tabpanel" aria-labelledby="view-onsites">
                                                                        <div class="card card-body table table-striped table-hover table-responsive" style="height: 600px">
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
                                                                                <%foreach (var quantitySold_Order in productSales.QuantitySold_Order) %>
                                                                                <%{ %>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td><%=quantitySold_Order.ProductName %></td>
                                                                                        <td><%=quantitySold_Order.ProductPrice %></td>
                                                                                        <td><%=quantitySold_Order.ProductID %></td>
                                                                                        <td><%=quantitySold_Order.SaleType %></td>
                                                                                        <td><%=quantitySold_Order.Date %></td>
                                                                                        <td><%=quantitySold_Order.Administrator %></td>
                                                                                        <td><%=quantitySold_Order.ProductCount * quantitySold_Order.ProductPrice %></td>
                                                                                        <td><%=quantitySold_Order.ProductCount %></td>
                                                                                    </tr>
                                                                                </tbody>
                                                                                <%} %>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                                            </div>
                                                        </div>

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
                    <%} %>
                    <%else %>
                    <%{ %>
                    <center><lottie-player src="https://assets1.lottiefiles.com/packages/lf20_LlRvIg.json" background="transparent" speed="1" style="width: 400px; height: 400px;" loop autoplay></lottie-player></center>
                    <center><h3><b>Sorry you are not allowed to view this.</b></h3></center>
                    <%} %>
                </div>
            </div>
        </div>
</asp:Content>
