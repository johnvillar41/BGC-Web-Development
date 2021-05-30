<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="SoftEngWebEmployee.Views.Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .scrolling-wrapper {
            overflow-y: auto;
        }

        .modal-body {
            word-wrap: break-word;
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

    <!-- Inventory Title + Add Product Button -->
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="overlayDiv">
                <lottie-player src="https://assets8.lottiefiles.com/packages/lf20_LqA9yY.json" background="transparent" speed="1" style="width: 400px; height: 400px;" loop autoplay></lottie-player>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="card card0 border-0 bg-dark">
        <div class="row d-flex">

            <div class="row pt-4 m-1">
                <div class="col-6">
                    <p class="fs-2 text-warning"><b>Inventory</b></p>
                </div>
                <div class="col-6">
                    <button runat="server" id="btnInventoryAdd" onserverclick="btnInventoryAdd_ServerClick" class="btn btn-primary float-end mr-3" title="Add Product">
                        <i class="fa fa-plus-circle"></i>Add Product</button>
                </div>
            </div>

            <div class="row mb-3 ml-3">
                <!-- Search Bar -->
                <div class="col-12 col-xl-3 col-lg-4 col-md-5 col-sm-8">
                    <div class="float-left" style="margin-right: 5px">
                        <div class="input-group">
                            <div class="form-outline">
                                <asp:TextBox ID="searchBox" placeholder="Search" runat="server" CssClass="form-control" MaxLength="255"></asp:TextBox>
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
        </div>
    </div>


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
                <div class="row row-cols-2 row-cols-md-3 row-cols-xl-5 row-cols-sm-2 row-cols-lg-4 scrolling-wrapper p-1" style="height: 600px;">
                    <asp:Repeater ID="SearchRepeater" runat="server">
                        <ItemTemplate>
                            <div class="card" style="height: 400px;">
                                <img class="card-img-top" src="data:image/jpeg;base64,<%# Eval("ProductPicture") %>" height="150px" width="75px" alt="Product picture here">
                                <div class="card-body">
                                    <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></h5>
                                    <asp:Label ID="StocksLabel" CssClass="card-text" runat="server" Text="" OnDataBinding="StocksLabel_DataBinding"></asp:Label>
                                </div>
                                <div class="card-footer">
                                    <asp:Button ID="detailsButton" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-dark" Text="Details" data-bs-toggle="modal" href="#detailsModal" OnClick="RetrieveDetails" runat="server" />
                                    <asp:Button ID="UpdateButton" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-warning float-right" Text="Update" data-bs-toggle="modal" OnClick="UpdateButton_Click" runat="server" />
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
                                    <button type="button" class="btn btn-dark" onclick="$('#detailsModal').modal('hide');">Back</button>
                                    <asp:Button ID="UpdateButton" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-warning float-right" Text="Update" OnClick="UpdateButton_Click" runat="server" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>


</asp:Content>
