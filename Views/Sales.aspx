<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" ClientIDMode="AutoID" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="SoftEngWebEmployee.Views.Sales" %>

<%@ Import Namespace="SoftEngWebEmployee.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .scrolling-wrapper {
            overflow-x: auto;
        }
    </style>


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
            <div class="col-4 col-xl-4 col-lg-4 col-md-6 col-sm-5">
                <div class="row">
                    <div class="col-lg-6 col-sm-12">
                        <div class="btn-group" style="margin-bottom: 10px; margin-top: 10px">
                        <button type="button" class="btn btn-warning dropdown-toggle dropdown-toggle-split" id="dropdownMenuReference" data-bs-toggle="dropdown" aria-expanded="false">
                            Select Category
                        </button>
                        <ul class="dropdown-menu dropdown-menu-dark" aria-labelledby="dropdownMenuReference">
                            <li><asp:Button ID="CategoryBtnAllProducts" runat="server" CssClass="dropdown-item" Text="All Products" OnClick="CategoryBtnAllProducts_Click" /></li>
                            <li>
                                <hr class="dropdown-divider">
                            </li>
                            <asp:Repeater ID="CategoryRepeater" runat="server" OnItemCreated="CategoryRepeater_ItemCreated">
                                <ItemTemplate>
                                    <a runat="server" class="dropdown-item" id="categorySelected">                                        
                                        <li>
                                            <asp:Button ID="CategoryBtn" runat="server" CssClass="dropdown-item" Text='<%# DataBinder.Eval(Container.DataItem,"ProductCategory") %>' OnClick="CategoryBtn_Click" />
                                        </li>
                                    </a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    </div>                   
                </div>

            </div>
            <div class="col-12">
                <div class="row">
                    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">
                                    <asp:Repeater ID="ProductsRepeater" runat="server">
                                        <ItemTemplate>
                                            <div class="col-lg-3 col-md-4 col-sm-5 col-6 my-2">
                                                <div class="card" style="max-width: 35rem; height: 420px">
                                                    <img class="card-img-top" alt="Card image cap" height="200px" width="100px" src="data:image/jpeg;base64,<%# Eval("ProductPicture") %>" />
                                                    <div class="card-body">
                                                        <h5 class="card-title"><b>Product Name: </b><%# DataBinder.Eval(Container.DataItem,"ProductName") %></h5>
                                                        <p class="card-text"><b>Description: </b><%# DataBinder.Eval(Container.DataItem,"ProductDescription") %></p>

                                                    </div>
                                                    <div class="card-footer">
                                                        <a class="btn btn-primary" data-bs-toggle="modal" href="#cart" role="button">Add to Cart</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="CategoryBtnAllProducts" EventName="Click" />
                            </Triggers>

                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <!-- Cart here -->
        </div>
    </div>
</asp:Content>
