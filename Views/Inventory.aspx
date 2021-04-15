<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="SoftEngWebEmployee.Views.Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .scrolling-wrapper {
            overflow-x: auto;
        }

    </style>

    <p class="fs-2"><b>Inventory</b></p>

    <a class="btn btn-primary float-end" data-bs-toggle="modal" href="#addProduct">Add Product</a>

    <p class="fs-4"><b>Search Inventory</b></p>

    <div class="row">
        <div class="col-8 col-xl-4 col-lg-4 col-md-6 col-sm-7">
            <div class="input-group">
                <div class="form-outline">
                    <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
                </div>
                <button class="btn btn-outline-success" type="submit">Search</button>
            </div>
        </div>
       
        <div class="col-4 col-xl-4 col-lg-4 col-md-6 col-sm-5">
            <div class="btn-group">
                <button type="button" class="btn btn-warning dropdown-toggle dropdown-toggle-split" id="dropdownMenuReference" data-bs-toggle="dropdown" aria-expanded="false">
                    Select Category
                </button>
                <ul class="dropdown-menu dropdown-menu-dark" aria-labelledby="dropdownMenuReference">
                    <li><asp:Button runat="server" CssClass="dropdown-item" Text="All Products" OnClick="category_Click" UseSubmitBehavior="false"/></li>
                    <li><hr class="dropdown-divider"></li>

                    <asp:Repeater ID="CategoryRepeater" runat="server">
                        <ItemTemplate>
                            <a runat="server" class="dropdown-item" id="categorySelected">
                                <li><asp:Button ID="category" runat="server" CssClass="dropdown-item" Text='<%# DataBinder.Eval(Container.DataItem,"ProductCategory") %>' OnClick="category_Click" UseSubmitBehavior="false"/></li>                               
                            </a>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>


    <!-- Text that disappears/changes depending on search results. This is when page is initially loaded.-->
    <p class="fs-5"><i>Use the search bar to display products.</i></p>
    <!-- In case of a search with no results, "No results found."-->
    <!-- Put a container here for displaying searched products.-->

    <!-- Search Repeater -->
    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
        <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">

            <asp:Repeater ID="SearchRepeater" runat="server">
                <ItemTemplate>
                    <div class="col-lg-3 col-md-4 col-sm-5 col-6 my-2">
                        <div class="card">
                            <!-- Possible change: modify size of picture space -->
                            <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                            <div class="card-body">
                                <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></h5>
                                <p class="card-text"><%# DataBinder.Eval(Container.DataItem,"ProductDescription") %></p>
                                <a class="btn btn-secondary" data-bs-toggle="modal" href="#updateProduct">Details</a>
                                <a class="btn btn-danger float-right" data-bs-toggle="modal" href="#deleteProduct">Delete</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </div>


    <!-- Greenhouse Repeater -->
    <hr />
    <p class="fs-4"><b>Greenhouse</b></p>
    <!-- Guide Source: https://codepen.io/Temmio/pen/gKGEYV -->
    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
        <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">

            <asp:Repeater ID="GHRepeater" runat="server">
                <ItemTemplate>
                    <div class="col-lg-3 col-md-4 col-sm-5 col-6 my-2">
                        <div class="card">
                            <!-- Possible change: modify size of picture space -->
                            <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                            <div class="card-body">
                                <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></h5>
                                <p class="card-text"><%# DataBinder.Eval(Container.DataItem,"ProductDescription") %></p>
                                <a class="btn btn-secondary" data-bs-toggle="modal" href="#updateProduct">Details</a>
                                <a class="btn btn-danger float-right" data-bs-toggle="modal" href="#deleteProduct">Delete</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </div>


    <!-- Hydroponics Repeater -->
    <hr />
    <p class="fs-4"><b>Hydroponics</b></p>
    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
        <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">

            <asp:Repeater ID="HPRepeater" runat="server">
                <ItemTemplate>
                    <div class="col-lg-3 col-md-4 col-sm-5 col-6 my-2">
                        <div class="card">
                            <!-- Possible change: modify size of picture space -->
                            <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                            <div class="card-body">
                                <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></h5>
                                <p class="card-text"><%# DataBinder.Eval(Container.DataItem,"ProductDescription") %></p>
                                <a class="btn btn-secondary" data-bs-toggle="modal" href="#updateProduct">Details</a>
                                <a class="btn btn-danger float-right" data-bs-toggle="modal" href="#deleteProduct">Delete</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </div>


    <!-- Modals -->

    <!-- Update Product -->
    <div class="modal fade" id="updateProduct" aria-hidden="true" aria-labelledby="..." tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-xl">
            <div class="modal-content">
                update product details here
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary">Back</button>
                    <button type="button" class="btn btn-primary">Update Details</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Add Product -->
    <div class="modal fade" id="addProduct" aria-hidden="true" aria-labelledby="..." tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-xl">
            <div class="modal-content">
                add product details here
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary">Back</button>
                    <button type="button" class="btn btn-primary">Add Product</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Delete Confirm -->
    <div class="modal fade" id="deleteProduct" aria-hidden="true" aria-labelledby="..." tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-sm">
            <div class="modal-content">
                Are you sure you want to delete this product?
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary">No</button>
                    <button type="button" class="btn btn-danger">Yes</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
