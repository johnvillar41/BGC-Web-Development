<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="SoftEngWebEmployee.Views.Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .scrolling-wrapper {
            overflow-x: auto;
        }

        .modal-body {
            word-wrap: break-word;
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

    <!-- Inventory Title + Add Product Button -->
    <div class="row pt-4">
        <div class="col-6">
            <p class="fs-2"><b>Inventory</b></p>
        </div>
        <div class="col-6">
            <button class="btn btn-success float-end" data-bs-toggle="modal" href="#addProduct">
                <i class="fa fa-plus-circle"></i>Add Product</button>
        </div>
    </div>

    <div class="row">
        <!-- Search Bar -->
        <div class="col-12 col-xl-3 col-lg-4 col-md-5 col-sm-8">
            <div class="float-left" style="margin-right: 5px">
                <div class="input-group">
                    <div class="form-outline">
                        <asp:TextBox ID="searchBox" placeholder="Search" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:LinkButton ID="searchButton" runat="server" OnClick="SearchButton_Click" CssClass="btn btn-small btn-primary"><i class="fa fa-search"></i></asp:LinkButton>
                </div>
            </div>
        </div>

        <!-- Category Dropdown -->
        <div class="col-12 col-xl-3 col-lg-4 col-md-5 col-sm-4">
            <div class="float-xl-start float-lg-start float-md-start float-sm-end">
                <div class="btn-group">
                    <asp:UpdatePanel ID="UpdatePanel_Dropdown" runat="server">
                        <ContentTemplate>
                            <!-- Dropdown Button -->
                            <asp:Button ID="dropdownMenuReference1" CssClass="btn btn-warning dropdown-toggle dropdown-toggle-split" data-bs-toggle="dropdown" aria-expanded="false" runat="server" Text="Select Category &#x25BC;" />
                            <!-- Dropdown List -->
                            <ul class="dropdown-menu dropdown-menu-dark" aria-labelledby="dropdownMenuReference1">
                                <li>
                                    <asp:Button ID="btnCategoryAll" runat="server" CssClass="dropdown-item" Text="All Products" OnClick="Category_Click" UseSubmitBehavior="false" /></li>
                                <li>
                                    <hr class="dropdown-divider">
                                </li>

                                <asp:Repeater ID="CategoryRepeater" OnItemCreated="CategoryRepeater_ItemCreated" runat="server">
                                    <ItemTemplate>
                                        <a runat="server" class="dropdown-item" id="categorySelected">
                                            <li>
                                                <asp:Button ID="category" runat="server" CssClass="dropdown-item" Text='<%#Container.DataItem%>' OnClick="Category_Click" UseSubmitBehavior="false" /></li>
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
            </div>
        </div>
    </div>

    <br>

    <!-- Search Repeater -->
    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
        <asp:UpdatePanel ID="UpdatePanel_SearchRepeater" runat="server">
            <ContentTemplate>
                <%if (listSearchRepeater != null)%>
                <%{ %>
                <%if (listSearchRepeater.Count == 0)%>
                <%{ %>
                <br>
                <br>
                <center><h3 style="color:white">No Items Found</h3></center>
                <center><lottie-player src="https://assets4.lottiefiles.com/temp/lf20_Celp8h.json" background="transparent"  speed="1"  style="width: 150px; height: 150px;"loop autoplay></lottie-player></center>
                <%} %>
                <%} %>
                <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">
                    <asp:Repeater ID="SearchRepeater" runat="server">
                        <ItemTemplate>
                            <div class="col-xl-3 col-lg-3 col-md-4 col-sm-5 col-9">
                                <div class="card">
                                    <img class="card-img-top" src="data:image/jpeg;base64,<%# Eval("ProductPicture") %>" height="150px" width="75px" alt="Product picture here">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></h5>
                                        <p class="card-text">Stocks: <%# DataBinder.Eval(Container.DataItem,"ProductStocks") %></p>
                                        <asp:Button ID="detailsButton" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-secondary" Text="Details" data-bs-toggle="modal" href="#detailsModal" OnClick="RetrieveDetails" runat="server" />
                                        <asp:Button ID="deleteProduct" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-danger float-right" Text="Delete" data-bs-toggle="modal" href="#deleteModal" OnClick="RetrieveDetails" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCategoryAll" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <!-- Greenhouse Repeater -->
    <hr />
    <p class="fs-4">Greenhouse</p>
    <!-- Guide Source: https://codepen.io/Temmio/pen/gKGEYV -->
    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
        <asp:UpdatePanel ID="UpdatePanel_GHRepeater" runat="server">
            <ContentTemplate>
                <%if (listGHRepeater != null)%>
                <%{ %>
                <%if (listGHRepeater.Count == 0) %>
                <%{ %>
                <br>
                <br>
                <center><h3 style="color:white">No Items Found</h3></center>
                <center><lottie-player src="https://assets4.lottiefiles.com/temp/lf20_Celp8h.json" background="transparent"  speed="1"  style="width: 150px; height: 150px;"loop autoplay></lottie-player></center>
                <%} %>
                <%} %>
                <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">
                    <asp:Repeater ID="GHRepeater" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-3 col-md-4 col-sm-5 col-6">
                                <div class="card">
                                    <!-- Possible change: modify size of picture space -->
                                    <img class="card-img-top" src="data:image/jpeg;base64,<%# Eval("ProductPicture") %>" height="150px" width="75px" alt="Product picture here">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></h5>
                                        <p class="card-text">Stocks: <%# DataBinder.Eval(Container.DataItem,"ProductStocks") %></p>
                                        <asp:Button ID="detailsButton" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-secondary" Text="Details" data-bs-toggle="modal" href="#detailsModal" OnClick="RetrieveDetails" runat="server" />
                                        <asp:Button ID="deleteProduct" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-danger float-right" Text="Delete" data-bs-toggle="modal" href="#deleteModal" OnClick="RetrieveDetails" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Hydroponics Repeater -->
    <hr />
    <p class="fs-4">Hydroponics</p>
    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
        <asp:UpdatePanel ID="UpdatePanel_HPRepeater" runat="server">
            <ContentTemplate>
                <%if (listHPRepeater != null)%>
                <%{ %>
                <%if (listHPRepeater.Count == 0) %>
                <%{ %>
                <br>
                <br>
                <center><h3 style="color:white">No Items Found</h3></center>
                <center><lottie-player src="https://assets4.lottiefiles.com/temp/lf20_Celp8h.json" background="transparent"  speed="1"  style="width: 150px; height: 150px;"loop autoplay></lottie-player></center>
                <%} %>
                <%} %>
                <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">
                    <asp:Repeater ID="HPRepeater" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-3 col-md-4 col-sm-5 col-6">
                                <div class="card">
                                    <!-- Possible change: modify size of picture space -->
                                    <img class="card-img-top" src="data:image/jpeg;base64,<%# Eval("ProductPicture") %>" height="150px" width="75px" alt="Product picture here">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></h5>
                                        <p class="card-text">Stocks: <%# DataBinder.Eval(Container.DataItem,"ProductStocks") %></p>
                                        <asp:Button ID="detailsButton" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-secondary" Text="Details" data-bs-toggle="modal" href="#detailsModal" OnClick="RetrieveDetails" runat="server" />
                                        <asp:Button ID="deleteProduct" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-danger float-right" Text="Delete" data-bs-toggle="modal" href="#deleteModal" OnClick="RetrieveDetails" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <!-- Modals -->

    <!-- Details Modal / Update Product -->
    <div class="modal fade" id="detailsModal" aria-hidden="true" aria-labelledby="..." tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-md">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Product Details</h5>
                    <button type="button" class="close" onclick="$('#detailsModal').modal('hide');" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div id="overlayDiv">
                            <lottie-player src="https://assets8.lottiefiles.com/packages/lf20_LqA9yY.json" background="transparent" speed="1" style="width: 400px; height: 400px;" loop autoplay></lottie-player>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel_DetailsModal" runat="server">
                    <ContentTemplate>
                        <asp:Repeater ID="DetailsRepeater" runat="server">
                            <ItemTemplate>
                                <div class="modal-body" style="word-wrap">
                                    <div class="row">
                                        <div class="col-4">
                                            <img src="data:image/jpeg;base64,<%# Eval("ProductPicture") %>" class="img-fluid"></img>
                                        </div>
                                        <div class="col-8">
                                            <b style="font-size: 20px"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></b><br />
                                            <br />
                                            <b style="font-size: 15px">Price: </b>Php <%# DataBinder.Eval(Container.DataItem,"ProductPrice") %><br />
                                            <b style="font-size: 15px">Stocks: </b><%# DataBinder.Eval(Container.DataItem,"ProductStocks") %><br />
                                            <b style="font-size: 15px">Category: </b><%# DataBinder.Eval(Container.DataItem,"ProductCategory") %><br />
                                            <b style="font-size: 15px">ID: </b><%# DataBinder.Eval(Container.DataItem,"Product_ID") %>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="card">
                                        <div class="card-header" id="descriptionHeading">
                                            <h5 class="my-0">
                                                <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#descriptionCollapse" aria-expanded="false" aria-controls="descriptionCollapse">
                                                    <u style="font-size: 15px">View Description</u>
                                                </button>
                                            </h5>
                                        </div>
                                        <div id="descriptionCollapse" class="collapse" aria-labelledby="descriptionHeading" data-parent="#detailsModal">
                                            <div class="card-body">
                                                <i style="font-size: 15px"><%# DataBinder.Eval(Container.DataItem,"ProductDescription") %></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" onclick="$('#detailsModal').modal('hide');">Back</button>
                                    <asp:Button ID="UpdateButton" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-primary float-right" Text="Update" OnClick="UpdateButton_Click" runat="server" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>

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
    <div class="modal fade" id="deleteModal" aria-hidden="true" aria-labelledby="..." tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-md">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Delete Product</h5>
                    <button type="button" class="close" onclick="$('#deleteModal').modal('hide');" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <asp:UpdatePanel ID="UpdatePanel_DeleteModal" runat="server">
                    <ContentTemplate>
                        <asp:Repeater ID="DeleteRepeater" runat="server">
                            <ItemTemplate>
                                <div class="modal-body" style="word-wrap">
                                    <p>Are you sure you want to delete this product? </p>
                                    <div class="row">
                                        <div class="col-4">
                                            <img src="data:image/jpeg;base64,<%# Eval("ProductPicture") %>" class="img-fluid"></img>
                                        </div>
                                        <div class="col-8">
                                            <b style="font-size: 20px"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></b><br />
                                            <br />
                                            <b style="font-size: 15px">Price: </b>Php <%# DataBinder.Eval(Container.DataItem,"ProductPrice") %><br />
                                            <b style="font-size: 15px">Stocks: </b><%# DataBinder.Eval(Container.DataItem,"ProductStocks") %>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="card">
                                        <div class="card-header" id="deleteHeading">
                                            <h5 class="my-0">
                                                <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#deleteCollapse" aria-expanded="false" aria-controls="deleteCollapse">
                                                    <u style="font-size: 15px">View Details</u>
                                                </button>
                                            </h5>
                                        </div>
                                        <div id="deleteCollapse" class="collapse" aria-labelledby="deleteHeading" data-parent="#deleteModal">
                                            <div class="card-body">
                                                <b style="font-size: 15px">Category: </b><%# DataBinder.Eval(Container.DataItem,"ProductCategory") %><br />
                                                <b style="font-size: 15px">ID: </b><%# DataBinder.Eval(Container.DataItem,"Product_ID") %><br />
                                                <b style="font-size: 15px">Description: </b>
                                                <br />
                                                <i style="font-size: 15px"><%# DataBinder.Eval(Container.DataItem,"ProductDescription") %></i>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" onclick="$('#deleteModal').modal('hide');">No</button>
                                    <asp:Button ID="DeleteButton" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-danger float-right" Text="Yes" OnClick="DeleteButton_Click" runat="server" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>

</asp:Content>
