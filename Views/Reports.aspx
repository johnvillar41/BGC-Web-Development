<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="SoftEngWebEmployee.Views.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p><b>REPORTS</b></p>

    <ul class="nav nav-tabs" id="reports" role="tablist">
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
    </div>






</asp:Content>
