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
    <script type="text/javascript">     
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode > 31 && charCode < 48) || charCode > 57) {
                return false;
            }
            return true;
        }
    </script>
    <div class="container">
        <div class="row" style="margin-bottom: 5px">
            <div class="btn-group" role="group" aria-label="Basic example">
                <button type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#CancelModal">Cancel</button>
                <button type="button" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#FinishModal">Finish</button>
            </div>
        </div>

        <%foreach (var orders in DisplayOrders()) %>
        <%{%>
        <div class="row" style="margin-bottom: 5px">

            <div class="col-lg-4 col-md-6 col-sm-12">
                <div class="card text-white bg-secondary mb-3" style="max-width: 35rem; height: 298px">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-4">
                                 Order ID: <%=orders.Order_ID %>
                            </div>
                            <div class="col-5">
                            </div>
                            <div class="col-3 float-end">
                                <%if (orders.OrderStatus.Equals("Finished")) %>
                                <%{ %>
                                <span class="badge bg-success"><%=orders.OrderStatus %></span>
                                <%} %>
                                <%else %>
                                <%{ %>
                                <span class="badge bg-danger"><%=orders.OrderStatus %></span>
                                <%} %>
                            </div>
                        </div>                       
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

            <div class="col-lg-8 col-md-6 col-sm-12">
                <div class="card bd-callout bd-callout-warning" style="border-radius: .25rem">
                    <div class="row">
                        <div class="col-lg-9 col-md-6 col-sm-6">
                            <h1>Orders</h1>
                        </div>
                    </div>

                    <div class="table-bordered table-condensed table-responsive" style="height: 200px">
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
                        <%if (orders.SpecificOrdersModel.Count() == 0) %>
                        <%{ %>
                        <center><h1>Empty Orders</h1></center>
                        <%} %>
                    </div>
                </div>
            </div>
        </div>
        <%}%>
    </div>

    <!--Modal Confirmation Cancel-->
    <div class="modal fade" id="CancelModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn btn-danger">
                    <h5 class="modal-title">Update Order</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="input-group has-validation">
                        <asp:TextBox ID="OrderIDCancel" CssClass="form-control" runat="server" placeholder="Enter Order Id" onkeypress="return isNumber(event)" onpaste="return false;" required></asp:TextBox>
                    </div>

                    Are you sure you want to <span style="color: red">Cancel</span> order?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <asp:Button ID="btnCancelStatus" runat="server" CssClass="btn btn-success" Text="Confirm" OnClick="btnCancelStatus_Click" UseSubmitBehavior="false" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>
    <!--Modal Confirmation Finish-->
    <div class="modal fade" id="FinishModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn btn-success">
                    <h5 class="modal-title">Update Order</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="input-group has-validation">
                        <asp:TextBox ID="OrderIDFinish" CssClass="form-control" runat="server" placeholder="Enter Order Id" onkeypress="return isNumber(event)" onpaste="return false;" required></asp:TextBox>
                    </div>
                    Are you sure you want to <span style="color: green">Finish</span> order?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <asp:Button ID="btnFinishStatus" runat="server" CssClass="btn btn-success" Text="Confirm" OnClick="btnFinishStatus_Click" UseSubmitBehavior="false" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
