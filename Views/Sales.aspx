<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="SoftEngWebEmployee.Views.Sales" %>

<%@ Import Namespace="SoftEngWebEmployee.Helpers" %>
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
                            <td><%# DataBinder.Eval(Container.DataItem, "SalesType") %></td>
                            <td><%# DataBinder.Eval(Container.DataItem, "Date") %></td>
                            <td>                                
                                <asp:Button  OnClick="IDS_Click" ID="IDS" CommandName ="SalesCommand" CommandArgument='<%# Eval("Orders.Order_ID") +";"+Eval("OnsiteTransaction.TransactionID") %>' CssClass="btn btn-primary" runat="server" Text="View All Details" />                               
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
