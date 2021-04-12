<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="SoftEngWebEmployee.Views.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p><b><u>REPORTS</u></b></p>
    <!-- Start comment -->
    <%--    <ul class="nav nav-tabs" id="reports" role="tablist">
        <li class="nav-item">
            <a class="nav-link active" id="sales-report" data-toggle="tab" href="#view" role="tab" aria-controls="home" aria-selected="true">Sales Report</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" id="inventory-report" data-toggle="tab" href="#create" role="tab" aria-controls="profile" aria-selected="false">Inventory Report</a>
        </li>
    </ul>

    <div class="tab-content" id="myTabContent">
        <!-- Sales -->
        <div class="tab-pane fade show active" id="view" role="tabpanel" aria-labelledby="sales-report">
            <div>

                <ul class="nav nav-tabs" id="sales" role="tablist">

                    <li class="nav-item">
                        <a class="nav-link active" id="by-product" data-toggle="tab" href="#view" role="tab" aria-controls="auto" aria-selected="undefined">By Product</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="total-income" data-toggle="tab" href="#create" role="tab" aria-controls="profile" aria-selected="undefined">Total Income</a>
                    </li>
                </ul>

                <div class="tab-content" id="secondTab">
                    <!-- Products -->
                    <div class="tab-pane fade show active" id="view2" role="tabpanel" aria-labelledby="by-product">
                        <div>
                             <%for (int i = 0; i < 100; i++) %>
                            <%{ %>
                            <div class="card" style="width: 18rem;">
                                <div class="card-body">
                                    <h5 class="card-title">Card title</h5>
                                    <h6 class="card-subtitle mb-2 text-muted">Card subtitle</h6>
                                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                                    <a href="#" class="card-link">Card link</a>
                                    <a href="#" class="card-link">Another link</a>
                                </div>
                            </div>
                            <%} %>
                        </div>
                    </div>


                    <!-- Sales Income -->
                    <div class="tab-pane fade show active" id="view3" role="tabpanel" aria-labelledby="sales-income">
                        <div>
                            <div class="btn-group" role="group" aria-label="Basic example">
                                <button type="button" class="btn btn-secondary">Left</button>
                                <button type="button" class="btn btn-secondary">Middle</button>
                                <button type="button" class="btn btn-secondary">Right</button>
                            </div>
                        </div>


                    </div>
                </div>

            </div>
        </div>

        <!-- Inventory -->
        <div class="tab-pane fade" id="create" role="tabpanel" aria-labelledby="inventory-report">
        </div>
    </div>--%>
    <!-- end comment -->

    <!--Tab Menu-->
    <div class="row">
        <div class="col-2">
            <div class="list-group" id="list-tab" role="tablist">
                <a class="list-group-item list-group-item-action active" id="list-home-list" data-toggle="list" href="#dashboard" role="tab" aria-controls="home">Dashboard</a>
                <a class="list-group-item list-group-item-action" id="list-profile-list" data-toggle="list" href="#products" role="tab" aria-controls="profile">Products</a>
                <a class="list-group-item list-group-item-action" id="list-messages-list" data-toggle="list" href="#sales-income" role="tab" aria-controls="messages">Sales Income</a>
                <a class="list-group-item list-group-item-action" id="list-settings-list" data-toggle="list" href="#inventory" role="tab" aria-controls="settings">Inventory</a>
            </div>
        </div>

        <div class="col-8">
            <div class="tab-content" id="nav-tabContent">
                <!--Dashboard-->
                <div class="tab-pane fade show active" id="dashboard" role="tabpanel" aria-labelledby="list-home-list">

                    <div class="row">
                        <div class="col-4">
                            <div class="card" style="width: 18rem;">
                                <div class="card-body">
                                    <h5 class="card-title">3,000,000</h5>
                                    <h6 class="card-subtitle mb-2 text-muted">Total Sales</h6>
                                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                                    <a href="#" class="card-link">Card link</a>
                                    <a href="#" class="card-link">Another link</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card" style="width: 18rem;">
                                <div class="card-body">
                                    <h5 class="card-title">600 stocks</h5>
                                    <h6 class="card-subtitle mb-2 text-muted">Total Inventory</h6>
                                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                                    <a href="#" class="card-link">Card link</a>
                                    <a href="#" class="card-link">Another link</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="card" style="width: 18rem;">
                                <div class="card-body">
                                    <h5 class="card-title">75</h5>
                                    <h6 class="card-subtitle mb-2 text-muted">Total Products</h6>
                                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                                    <a href="#" class="card-link">Card link</a>
                                    <a href="#" class="card-link">Another link</a>
                                </div>
                            </div>
                        </div>


                    </div>



                    <!--Product Lists-->
                    <div class="tab-pane fade" id="products" role="tabpanel" aria-labelledby="list-profile-list">
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
                    <!--Sales Income-->
                    <div class="tab-pane fade" id="sales-income" role="tabpanel" aria-labelledby="list-messages-list">

                        <div class="card" style="width: 18rem;">
                            <img src="..." class="card-img-top" alt="...">
                            <div class="card-body">
                                <h5 class="card-title">Card title</h5>
                                <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                            </div>
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item">An item</li>
                                <li class="list-group-item">A second item</li>
                                <li class="list-group-item">A third item</li>
                            </ul>
                            <div class="card-body">
                                <a href="#" class="card-link">Card link</a>
                                <a href="#" class="card-link">Another link</a>
                            </div>
                        </div>

                    </div>

                    <!--Inventory-->
                    <div class="tab-pane fade" id="inventory" role="tabpanel" aria-labelledby="list-profile-list">
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
    </div>
</asp:Content>
