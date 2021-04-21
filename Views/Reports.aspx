<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeBehind="Reports.aspx.cs" Inherits="SoftEngWebEmployee.Views.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p><b><u>REPORTS</u></b></p>


    <div class="d-flex align-items-start">
        <div class="col-2">


            <div class="list-group" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                <button class="list-group-item list-group-item-action active" id="v-pills-dashboard-tab" data-bs-toggle="pill" data-bs-target="#v-pills-dashboard" type="button" role="tab" aria-controls="v-pills-dashboard" aria-selected="true">Dashboard</button>
                <button class="list-group-item list-group-item-action" id="v-pills-products-tab" data-bs-toggle="pill" data-bs-target="#v-pills-products" type="button" role="tab" aria-controls="v-pills-products" aria-selected="false">Product Sales Report</button>
                <button class="list-group-item list-group-item-action" id="v-pills-sales-tab" data-bs-toggle="pill" data-bs-target="#v-pills-sales" type="button" role="tab" aria-controls="v-pills-sales" aria-selected="false">Sales Income Report</button>
                <button class="list-group-item list-group-item-action" id="v-pills-inventory-tab" data-bs-toggle="pill" data-bs-target="#v-pills-inventory" type="button" role="tab" aria-controls="v-pills-inventory" aria-selected="false">Inventory Report</button>
            </div>
        </div>
        <div class="col-10">
            <div class="tab-content" id="v-pills-tabContent">
                <!--Dashboard-->
                <div class="tab-pane fade show active" id="v-pills-dashboard" role="tabpanel" aria-labelledby="v-pills-dashboard-tab">
                    <div class="row">
                        <div class="col-4">
                            <div class="card" style="width: 18rem;">
                                <div class="card-body">
                                    <%--<h5 class="card-title">3,000,000</h5>--%>
                                    <asp:Label ID="total_sales" runat="server" Text="Total Sales" CssClass="card-title" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                                    <br />

                                    <h5 class="card-subtitle mb-2 text-muted">Total Sales</h5>
                                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                                    <a href="#" class="card-link">Card link</a>
                                    <a href="#" class="card-link">Another link</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card" style="width: 18rem;">
                                <div class="card-body">
                                    <%--<h5 class="card-title">600 stocks</h5>--%>
                                    <asp:Label ID="total_inventory" runat="server" Text="Total Inventory" CssClass="card-title" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                                    <h5 class="card-subtitle mb-2 text-muted">Total Inventory</h5>
                                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                                    <a href="#" class="card-link">Card link</a>
                                    <a href="#" class="card-link">Another link</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card" style="width: 18rem;">
                                <div class="card-body">
                                    <%--<h5 class="card-title">75</h5>--%>
                                    <asp:Label ID="total_products" runat="server" Text="Total Products" CssClass="card-title" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                                    <h5 class="card-subtitle mb-2 text-muted">Total Products</h5>
                                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                                    <a href="#" class="card-link">Card link</a>
                                    <a href="#" class="card-link">Another link</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="tab-pane fade" id="v-pills-sales" role="tabpanel" aria-labelledby="v-pills-sales-tab">
                    <!--Sales Income-->

                    <div class="row">
                        <div class="col-4">
                            <div class="card border-success mb-3" style="max-width: 18rem;">
                                <div class="card-header">Date</div>
                                <div class="card-body text-success">
                                    <h5 class="card-title">Total Sales today!</h5>
                                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                                    <a class="btn btn-primary" data-bs-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">See More
                                    </a>
                                </div>
                            </div>

                            <!--See More-->
                            <div class="collapse" id="collapseExample">
                                <div class="card card-body">
                                    Some placeholder content for the collapse component. This panel is hidden by default but revealed when the user activates the relevant trigger.


                           
                                </div>
                            </div>
                        </div>

                        <div class="col-4">
                            <div class="card border-success mb-3" style="max-width: 18rem;">
                                <div class="card-header">Header</div>
                                <div class="card-body text-success">
                                    <h5 class="card-title">Success card title</h5>
                                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card border-success mb-3" style="max-width: 18rem;">
                                <div class="card-header">Header</div>
                                <div class="card-body text-success">
                                    <h5 class="card-title">Success card title</h5>
                                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="v-pills-products" role="tabpanel" aria-labelledby="v-pills-products-tab">
                    <!-- Products -->
                    <div class="col-12">
                        <%if (ProductSalesListDisplay == null) return; %>
                        <%foreach (var productSales in ProductSalesListDisplay) %>
                        <%{ %>
                        <div class="row mb-3">
                            <div class="card text-center p-0">
                                <center><img alt="" height="250px" width="300px" style="border-radius:50%" src="data:image/jpeg;base64,<%=productSales.ProductReport.Product.ProductPicture%>" /></center>
                                <hr style="margin: 20px" />
                                <div class="card-body">
                                    <h5 class="card-title">Product ID: <%=productSales.ProductReport.Product.Product_ID%></h5>
                                    <h4 class="card-title"><b><%=productSales.ProductReport.Product.ProductName %></b></h4>
                                </div>
                                <ul class="list-group list-group-flush">
                                    <li class="list-group-item">Unit Price:

                                        <label><%=productSales.ProductReport.Product.ProductPrice%></label>
                                    </li>


                                    <li class="list-group-item">Product Revenue:
                                         <label><%=productSales.ProductReport.ProductRevenue %></label>
                                        
                                    </li>

                                    <li class="list-group-item">Quantity Sold:
                                        <label><%=productSales.ProductReport.QuantitySold %></label>                                        
                                        <hr />
                                        <table class="table table-striped table-hover">
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
                                    </li>
                                </ul>

                            </div>
                        </div>
                        <%} %>
                    </div>


                </div>
                <div class="tab-pane fade" id="v-pills-inventory" role="tabpanel" aria-labelledby="v-pills-inventory-tab">
                    <!--Inventory-->

                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Product Name</th>
                                <th scope="col">Unit Price</th>
                                <th scope="col">Quantity Sold</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">1</th>
                                <td>Mark</td>
                                <td>Otto</td>
                                <td>@mdo</td>
                            </tr>
                            <tr>
                                <th scope="row">2</th>
                                <td>Jacob</td>
                                <td>Thornton</td>
                                <td>@fat</td>
                            </tr>
                            <tr>
                                <th scope="row">3</th>
                                <td>Larry</td>
                                <td>the Bird</td>
                                <td>@twitter</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
