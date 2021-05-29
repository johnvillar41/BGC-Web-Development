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

        .card0 {
            box-shadow: 0px 4px 8px 0px #757575;
            border-radius: 5px
        }

        #overlayDiv {
            position: fixed;
            left: 50%;
            top: 50%;
            -webkit-transform: translate(-50%, -50%);
            transform: translate(-50%, -50%);
            z-index: 99;
        }
    </style>

    <div class="row" style="margin-bottom: 5px">
        <div class="col-lg-9 col-md-7"></div>
        <div class="btn-group col-lg-3 col-md-5" role="group" aria-label="Basic example">
            <button type="button" class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#CancelModal">Cancel Order</button>
            <button type="button" class="btn btn-dark" data-bs-toggle="modal" data-bs-target="#FinishModal">Finish Order</button>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-9 col-md-7"></div>
        <div class="btn-group col-lg-3 col-md-5" role="group" aria-label="Basic example">
            <asp:TextBox ID="OrderIdSearchTextbox" runat="server" CssClass="form-control" placeholder="Enter Order Id here"></asp:TextBox>
            <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-warning" Text="Search" OnClick="BtnSearch_Click" UseSubmitBehavior="false" />
        </div>

    </div>
    <hr />
    <div class="row card0 card m-1">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div id="overlayDiv">
                    <lottie-player src="https://assets8.lottiefiles.com/packages/lf20_LqA9yY.json" background="transparent" speed="1" style="width: 400px; height: 400px;" loop autoplay></lottie-player>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="row m-1">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <asp:UpdatePanel ID="UpdatePanel_Dropdown" runat="server">
                            <ContentTemplate>
                                <!-- Dropdown Button -->
                                <asp:Button ID="dropdownMenuReference1" CssClass="btn btn-warning dropdown-toggle dropdown-toggle-split" data-bs-toggle="dropdown" aria-expanded="false" runat="server" Text="Select Category &#x25BC;" />
                                <!-- Dropdown List -->
                                <ul class="dropdown-menu dropdown-menu-dark" aria-labelledby="dropdownMenuReference1">
                                    <li>
                                        <asp:Button ID="btnCategoryAll" runat="server" CssClass="dropdown-item" Text="All Orders" OnClick="Category_Click" UseSubmitBehavior="false" /></li>
                                    <li>
                                        <hr class="dropdown-divider">
                                    </li>
                                    <asp:Repeater ID="CategoryRepeater" OnItemCreated="CategoryRepeater_ItemCreated" runat="server">
                                        <ItemTemplate>
                                            <a runat="server" class="dropdown-item" id="categorySelected">
                                                <li>
                                                    <asp:Button ID="Category" runat="server" CssClass="dropdown-item" Text='<%#Container.DataItem%>' OnClick="Category_Click" UseSubmitBehavior="false" /></li>
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </ContentTemplate>

                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dropdownMenuReference1" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <br />
                    <div class="row">
                        <%if (OrdersList.Count() == 0) %>
                        <%{ %>
                        <center><lottie-player src="https://assets1.lottiefiles.com/packages/lf20_qlwqp9xi.json" background="transparent" speed="1" style="width: 400px; height: 400px;" loop autoplay></lottie-player></center>
                        <center><h4><b style="color:#000000;">No orders found!</b></h4></center>
                        <%} %>
                        <%foreach (var orders in OrdersList) %>
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
                                                <%else if (orders.OrderStatus.Equals("Cancelled"))%>
                                                <%{ %>
                                                <span class="badge bg-danger"><%=orders.OrderStatus %></span>
                                                <%} %>
                                                <%else%>
                                                <%{ %>
                                                <span class="badge bg-warning"><%=orders.OrderStatus %></span>
                                                <%} %>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="card-body">
                                        <p class="card-text">Customer ID: <%=orders.CustomerID %></p>
                                        <p class="card-text">Order Date: <%=orders.OrderDate %></p>
                                        <p class="card-text">Total Number Of Orders: <%=String.Format("{0:n0}",orders.TotalNumberOfOrders) %></p>
                                    </div>
                                    <div class="card-footer">Total Price: <%=String.Format("{0:n0}",orders.OrderTotalPrice) %></div>
                                </div>
                            </div>

                            <div class="col-lg-8 col-md-6 col-sm-12">
                                <div class="card bd-callout bd-callout-warning" style="border-radius: .25rem">
                                    <div class="row">
                                        <div class="col-lg-9 col-md-6 col-sm-6">
                                            <h1>Orders</h1>
                                        </div>
                                    </div>

                                    <div class="table-responsive table-hover" style="height: 200px">
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th scope="col">Product ID</th>
                                                    <th scope="col">Product Count</th>
                                                    <th scope="col">Product Name</th>
                                                    <th scope="col">Product Price</th>
                                                    <th scope="col">Product Picture</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <%for (int i = 0; i < orders.SpecificOrdersModel.Count(); i++) %>
                                                <%{ %>
                                                <tr>
                                                    <td><%=orders.SpecificOrdersModel[i].ProductID %></td>
                                                    <th scope="row"><%=orders.SpecificOrdersModel[i].TotalOrders %></th>
                                                    <td><%=orders.SpecificOrdersModel[i].ProductsModel.ProductName %></td>
                                                    <td><%=String.Format("{0:n0}",orders.SpecificOrdersModel[i].ProductsModel.ProductPrice) %></td>
                                                    <td>
                                                        <img alt="" height="100px" width="100px" src="data:image/png;base64,<%=orders.SpecificOrdersModel[i].ProductsModel.ProductPicture%>" /></td>
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
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BtnSearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnFinishStatus" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelStatus" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
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
                    <asp:Button ID="btnFinishStatus" runat="server" CssClass="btn btn-success" Text="Confirm" OnClick="btnFinishStatus_Click" AutoPostBack="true" UseSubmitBehavior="false" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>
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
</asp:Content>
