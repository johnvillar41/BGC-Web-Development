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

    <div class="input-group">
        <div class="form-outline">
            <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
        </div>
        <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
    </div>

    <!-- Split dropdown button -->
    <div class="btn-group">
        <button type="button" class="btn btn-warning">All Products</button>
        <button type="button" class="btn btn-secondary dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <span class="sr-only">Toggle Dropdown</span>
        </button>
        <div class="dropdown-menu">
            <a class="dropdown-item" href="#">All Products</a>
            <a class="dropdown-item" href="#">Another action</a>
            <a class="dropdown-item" href="#">Something else here</a>
            <div class="dropdown-divider"></div>
            <a class="dropdown-item" href="#">Separated link</a>
        </div>
    </div>



    <!-- Text that disappears/changes depending on search results. This is when page is initially loaded.-->
    <p class="fs-5"><i>Use the search bar to display products.</i></p>
    <!-- In case of a search with no results, "No results found."-->
    <!-- Put a container here for displaying searched products.-->

    <!-- Repeater Cards Test -->
    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
        <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">

            <asp:Repeater ID="InventoryRepeater" runat="server" OnItemCommand="InventoryRepeater_ItemCommand">

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


    <!-- Greenhouse Section -->
    <hr />
    <p class="fs-4"><b>Greenhouse</b></p>
    <!-- Guide Source: https://codepen.io/Temmio/pen/gKGEYV -->
    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
        <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">

            <asp:Repeater ID="GHRepeater" runat="server" OnItemCommand="InventoryRepeater_ItemCommand">

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


    <!-- Hydroponics Section -->
    <hr />
    <p class="fs-4"><b>Hydroponics</b></p>
    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
        <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">

            <asp:Repeater ID="HPRepeater" runat="server" OnItemCommand="InventoryRepeater_ItemCommand">

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
