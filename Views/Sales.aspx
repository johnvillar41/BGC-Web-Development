<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="SoftEngWebEmployee.Views.Sales" %>

<%@ Import Namespace="SoftEngWebEmployee.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




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

            <div class="table-bordered table-condensed table-responsive" style="height: 600px">
                <asp:Repeater ID="SalesRepeater" runat="server">
                    <HeaderTemplate>
                        <div class="table-bordered table-condensed table-responsive" style="height: 500px">
                            <table border="1" class="table table-striped">
                                <tr>
                                    <th scope="col">Sales ID</th>
                                    <th scope="col">Username</th>
                                    <th scope="col">Type Of Sale</th>
                                    <th scope="col">Date</th>
                                    <th scope="col">Details</th>
                                </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# DataBinder.Eval(Container.DataItem, "SalesID") %></td>
                            <td><%# DataBinder.Eval(Container.DataItem, "Administrator.Username") %></td>
                            <td><span class="badge bg-dark"><%# Eval("SalesType")%></span> </td>
                            <td><%# DataBinder.Eval(Container.DataItem, "Date") %></td>
                            <td>
                                <asp:Button OnClick="IDS_Click" ID="IDS" CommandName="SalesCommand" CommandArgument='<%# Eval("Orders.Order_ID") +";"+Eval("OnsiteTransaction.TransactionID") %>' CssClass="btn btn-primary" runat="server" Text="View All Details" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                         </div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

        <!-- Create Transaction tab -->
        <div class="tab-pane fade" id="create" role="tabpanel" aria-labelledby="create-transactions">
            <div class="col-12">
                <div class="row">
                    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
                        <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">

                            <asp:Repeater ID="ProductsRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="col-lg-3 col-md-4 col-sm-5 col-6 my-2">
                                        <div class="card">
                                            <!-- Possible change: modify size of picture space -->
                                            <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                                            <div class="card-body">
                                                <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></h5>
                                                <p class="card-text"><%# DataBinder.Eval(Container.DataItem,"ProductDescription") %></p>
                                                <a class="btn btn-primary" data-bs-toggle="modal" href="#cart" role="button">Add to Cart</a>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>

                            </asp:Repeater>

                        </div>
                    </div>
                </div>
            </div>
           
            <!-- Cart here -->
        </div>
    </div>
</asp:Content>
