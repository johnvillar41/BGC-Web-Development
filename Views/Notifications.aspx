<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="SoftEngWebEmployee.Views.Notifications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .bd-callout {
            padding: 1.25rem;
            margin-top: 1.25rem;
            margin-bottom: 1.25rem;
            border: 1px solid #e9ecef;
            border-left-width: .25rem;
            border-radius: .25rem;
        }

        .bd-callout-warning {
            border-left-color: #f0ad4e;
        }
    </style>
    <div class="container-fluid">
        <div class="row">
            <div class="col-12" style="margin: 5px">
                <div class="card bd-callout bd-callout-warning" style="border-radius: .25rem">
                    <div class="row">
                        <div class="col-3">
                            <h3 class="float-left">Business Transactions</h3>
                        </div>
                        <div class="col-6">
                            <!--Empty Div-->
                        </div>
                        <div class="col-3 ">
                            <!--Empty Div-->
                        </div>

                    </div>

                    <div class="table-bordered table-condensed table-responsive">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Activity</th>
                                    <th scope="col">Transaction</th>
                                    <th scope="col">Transacted by</th>
                                    <th scope="col">Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <th scope="row">1</th>
                                    <td>Added</td>
                                    <td>Product(s) in the Inventory</td>
                                    <td>John Doe</td>
                                    <td>April 1, 2021</td>
                                </tr>
                                <tr>
                                    <th scope="row">2</th>
                                    <td>Updated</td>
                                    <td>Product(s) in the Inventory</td>
                                    <td>John Doe</td>
                                    <td>April 1, 2021</td>
                                </tr>
                                <tr>
                                    <th scope="row">3</th>
                                    <td>Deleted</td>
                                    <td>Product(s) in the Inventory</td>
                                    <td>John Doe</td>
                                    <td>April 1, 2021</td>
                                </tr>
                                <tr>
                                    <th scope="row">4</th>
                                    <td>Added</td>
                                    <td>Sales</td>
                                    <td>John Doe</td>
                                    <td>April 1, 2021</td>
                                </tr>
                            </tbody>

                        </table>

                    </div>
                    <nav aria-label="Page navigation example">
                        <ul class="pagination">
                            <li class="page-item">
                                <a class="page-link" href="#" aria-label="Previous">
                                    <span aria-hidden="true">&laquo;</span>
                                </a>
                            </li>
                            <li class="page-item"><a class="page-link" href="#">1</a></li>
                            <li class="page-item"><a class="page-link" href="#">2</a></li>
                            <li class="page-item"><a class="page-link" href="#">3</a></li>
                            <li class="page-item">
                                <a class="page-link" href="#" aria-label="Next">
                                    <span aria-hidden="true">&raquo;</span>
                                </a>
                            </li>
                        </ul>
                    </nav>
                </div>
            </div>

        </div>
</asp:Content>
