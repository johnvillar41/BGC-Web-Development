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
                <button type="button" class="btn btn-warning">Select Category</button>
                <button type="button" class="btn btn-warning dropdown-toggle dropdown-toggle-split" id="dropdownMenuReference" data-bs-toggle="dropdown" aria-expanded="false" data-bs-reference="parent">
                    <span class="visually-hidden">Toggle Dropdown</span>
                </button>

                <!-- TODO: Current Problem Area (fix functionality of SearchOnCategory() method, have menu drop down to the right instead of directly down) --> 
                <ul class="dropdown-menu dropdown-menu-dark" aria-labelledby="dropdownMenuReference">
                    <a runat="server" class="dropdown-item" id="categoryAll" href="#" onclick="SearchOnCategory">
                        <i class="fa fa-shopping-cart" style="margin-right: 13px; margin-left: -5px; width: 10px; color: black"></i>
                        All Products
                    </a>
                    <li><hr class="dropdown-divider"></li>

                    <asp:Repeater ID="CategoryRepeater" runat="server" OnItemCommand="SearchOnCategory">
                        <ItemTemplate>
                            <a runat="server" class="dropdown-item" id="categorySelected" href="#">
                                <i class="fa fa-angle-right" style="margin-right: 3px; margin-left: 5px; width: 10px; color: black"></i>
                                <%# DataBinder.Eval(Container.DataItem,"ProductCategory") %>
                            </a>
                        </ItemTemplate>
                    </asp:Repeater>

                </ul>
            </div>
        </div>
    </div>

    <!-- Taken from Site.Master, functional here, works identically to taskbar version
    <div class="nav-item dropdown d-flex">
        <a class="nav-link dropdown-toggle" href="#" id="navbarDarkDropdownMenuLink" role="button" data-bs-toggle="dropdown" aria-expanded="false">Menu</a>
        <ul class="dropdown-menu dropdown-menu-dark" aria-labelledby="dropdownMenuButton1">
            <li style="text-align: right"><a runat="server" class="dropdown-item" href="~/Views/Inventory">Inventory<i class="fa fa-cubes" style="margin-left: 10px; width: 10px; color: black"></i></a></li>
            <li style="text-align: right"><a runat="server" class="dropdown-item" href="~/Views/Sales">Sales<i class="fa fa fa-dollar" style="margin-left: 10px; width: 10px; color: black"></i></a></li>
            <li style="text-align: right"><a runat="server" class="dropdown-item" href="~/Views/Notifications">Notifications<i class="fa fa-bell" style="margin-left: 10px; width: 10px; color: black"></i></a></li>
            <li style="text-align: right"><a runat="server" class="dropdown-item" href="~/Views/Information">Information<i class="fa fa-info" style="margin-left: 10px; width: 10px; color: black"></i></a></li>
            <li style="text-align: right"><a runat="server" class="dropdown-item" href="~/Views/Reports">Reports<i class="fa fa-bar-chart" style="margin-left: 10px; width: 10px; color: black"></i></a></li>
            <li style="text-align: right"><a runat="server" class="dropdown-item" href="~/Views/Administrators">Administrators<i class="fa fa-address-card-o" style="margin-left: 10px; width: 10px; color: black"></i></a></li>
            <li style="text-align: right"><a runat="server" class="dropdown-item" href="~/Views/Orders">Orders<i class="fa fa-cart-arrow-down" style="margin-left: 10px; width: 10px; color: black"></i></a></li>
            <li><hr class="dropdown-divider"></li>
            <li style="text-align: right"; margin-top: 10px"><asp:Button ID="LogoutButton" runat="server" CssClass="dropdown-item" Text="Logout" UseSubmitBehavior="false"/></li>                        
        </ul>                                                           
    </div>
    -->


    <!-- Text that disappears/changes depending on search results. This is when page is initially loaded.-->
    <p class="fs-5"><i>Use the search bar to display products.</i></p>
    <!-- In case of a search with no results, "No results found."-->
    <!-- Put a container here for displaying searched products.-->

    <!-- Repeater Cards Test -->
    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
        <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">

            <asp:Repeater ID="SearchRepeater" runat="server" OnItemCommand="InventoryRepeater_ItemCommand">

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
