<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="SoftEngWebEmployee.Views.Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>Sales Here</p>

    <!-- First modal dialog -->
    <div class="modal fade" id="modal" aria-hidden="true" aria-labelledby="..." tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-xl">
            <div class="modal-content">
                Modal 1 Text
      <div class="modal-footer">
          <!-- Toogle to second dialog -->
          <button class="btn btn-primary" data-bs-target="#modal2" data-bs-toggle="modal" data-bs-dismiss="modal">Open #modal2</button>
      </div>
            </div>
        </div>
    </div>
    <!-- Second modal dialog -->
    <div class="modal fade" id="modal2" aria-hidden="true" aria-labelledby="..." tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                ...
      <div class="modal-footer">
          <!-- Toogle to first dialog, `data-bs-dismiss` attribute can be omitted - clicking on link will close dialog anyway -->
          <!-- <a class="btn btn-primary" href="#modal" data-bs-toggle="modal" role="button">Open #modal</a> -->
          <a class="btn btn-primary" href="#modal" data-bs-toggle="modal" data-bs-dismiss="modal" role="button">Open #modal</a>
      </div>
            </div>
        </div>
    </div>
    <!-- Open first dialog -->
    <a class="btn btn-primary" data-bs-toggle="modal" href="#modal" role="button">View Transactions List</a>
    <a class="btn btn-primary" data-bs-toggle="modal" href="#modal" role="button">Create Transaction</a>

    

    <ul class="nav nav-tabs" id="trans" role="tablist">
        <li class="nav-item">
            <a class="nav-link active" id="view-transactions" data-toggle="tab" href="#view" role="tab" aria-controls="home" aria-selected="true">View Transactions List</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" id="create-transactions" data-toggle="tab" href="#create" role="tab" aria-controls="profile" aria-selected="false">Create Transaction</a>
        </li>
    </ul>

    <div class="tab-content" id="myTabContent">
        <!-- View All Transactions tab -->
        <div class="tab-pane fade show active" id="view" role="tabpanel" aria-labelledby="view-transactions">
            <div>
                <div class="table-bordered table-condensed table-responsive" style="height: 600px">
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">ID</th>
                                <th scope="col">Product</th>
                                <th scope="col">Quantity</th>
                                <th scope="col">Date</th>
                                <th scope="col">Details</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%for (int i = 0; i < 100; i++) %>
                            <%{ %>
                            <tr>
                                <th scope="row">1</th>
                                <td>Added</td>
                                <td>Product(s) in the Inventory</td>
                                <td>John Doe</td>
                                <td>
                                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">View All Details</button>
                                    <!-- Code for Modal, feel free to change or relocate-->
                                    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                        <div class="modal-dialog" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title" id="exampleModalLabel">New message</h5>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    <form>
                                                        <div class="form-group">
                                                            <label for="recipient-name" class="col-form-label">Recipient:</label>
                                                            <input type="text" class="form-control" id="recipient-name">
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="message-text" class="col-form-label">Message:</label>
                                                            <textarea class="form-control" id="message-text"></textarea>
                                                        </div>
                                                    </form>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                    <button type="button" class="btn btn-primary">Send message</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
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
                        <%} %>
                    </table>
                </div>
            </div>
        </div>

        <!-- Create Transaction tab -->
        <div class="tab-pane fade" id="create" role="tabpanel" aria-labelledby="create-transactions">
            <!-- First modal dialog -->
            <div class="modal fade" id="cart" aria-hidden="true" aria-labelledby="..." tabindex="-1">
                <div class="modal-dialog modal-dialog-centered modal-xl">
                    <div class="modal-content">
                        buy something
                    <div class="modal-footer">
                    <!-- Toogle to second dialog -->
                        <button type="button" class="btn btn-primary">Buy</button>
                    </div>
                    </div>
                </div>
            </div>
            <a class="btn btn-primary" data-bs-toggle="modal" href="#cart" role="button">Add to Cart</a>
        </div>
    </div>


</asp:Content>
