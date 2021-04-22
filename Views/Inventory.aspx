<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="SoftEngWebEmployee.Views.Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .scrolling-wrapper {
            overflow-x: auto;
        }
        .modal-body {
            word-wrap: break-word;
        }
    </style>

    <p class="fs-2"><b>Inventory</b></p>
    <a class="btn btn-primary float-end" data-bs-toggle="modal" href="#addProduct">Add Product</a>
    <p class="fs-4"><b>Search Inventory</b></p>

    <!-- Search Bar -->
    <div class="float-left" style="margin-right:5px">
        <div class="input-group">
            <div class="form-outline">                   
                <asp:TextBox ID="searchBox" placeholder="Search" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:LinkButton ID="searchButton" runat="server" OnClick="SearchButton_Click" CssClass="btn btn-small btn-primary"><i class="fa fa-search"></i></asp:LinkButton>
        </div>
    </div>
              
    <!-- Category Dropdown -->
    <div class="float-xl-start float-lg-start float-md-start float-end">
        <div class="btn-group">
            <asp:UpdatePanel ID="UpdatePanel_Dropdown" runat="server">
                <ContentTemplate>
                    <!-- Dropdown Button -->
                    <asp:Button ID="dropdownMenuReference1" CssClass="btn btn-warning dropdown-toggle dropdown-toggle-split" data-bs-toggle="dropdown" aria-expanded="false" runat="server" Text="Select Category &#x25BC;" />
                    <!-- Dropdown List -->
                    <ul class="dropdown-menu dropdown-menu-dark" aria-labelledby="dropdownMenuReference1">
                        <li><asp:Button ID="btnCategoryAll" runat="server" CssClass="dropdown-item" Text="All Products" OnClick="Category_Click" UseSubmitBehavior="false"/></li>
                        <li><hr class="dropdown-divider"></li>

                        <asp:Repeater ID="CategoryRepeater" OnItemCreated="CategoryRepeater_ItemCreated" runat="server">
                            <ItemTemplate>
                                <a runat="server" class="dropdown-item" id="categorySelected">
                                    <li><asp:Button ID="category" runat="server" CssClass="dropdown-item" Text='<%# DataBinder.Eval(Container.DataItem,"ProductCategory") %>' OnClick="Category_Click" UseSubmitBehavior="false"/></li>                               
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
    
    <br>
    <br>

    <!-- Search Repeater -->
    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
        <asp:UpdatePanel ID="UpdatePanel_SearchRepeater" runat="server">
            <ContentTemplate>
                <%if (listSearchRepeater.Count == 0) %>
                <%{ %>
                <br>
                <br>
                <center><h3 style="color:white">No Items Found</h3></center>
                <center><lottie-player src="https://assets4.lottiefiles.com/temp/lf20_Celp8h.json" background="transparent"  speed="1"  style="width: 300px; height: 300px;"loop autoplay></lottie-player></center>
                <%} %>
                <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">
                    <asp:Repeater ID="SearchRepeater" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-3 col-md-4 col-sm-5 col-6 my-2">
                                <div class="card">
                                    <!-- Possible change: modify size of picture space -->
                                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></h5>
                                        <p class="card-text">Number of Stocks: <%# DataBinder.Eval(Container.DataItem,"ProductStocks") %></p>
                                        <asp:Button ID="detailsButton" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-secondary" Text="Details" data-bs-toggle="modal" href="#detailsModal" OnClick="RetrieveDetails" runat="server"/>
                                        <asp:Button ID="deleteProduct" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-danger float-right" Text="Delete" data-bs-toggle="modal" href="#deleteModal" OnClick="RetrieveDetails" runat="server"/>                                       
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
    <p class="fs-4"><b>Greenhouse</b></p>
    <!-- Guide Source: https://codepen.io/Temmio/pen/gKGEYV -->
    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
        <asp:UpdatePanel ID="UpdatePanel_GHRepeater" runat="server">
            <ContentTemplate>
                <%if (listGHRepeater.Count == 0) %>
                <%{ %>
                <br>
                <br>
                <center><h3 style="color:white">No Items Found</h3></center>
                <center><lottie-player src="https://assets4.lottiefiles.com/temp/lf20_Celp8h.json" background="transparent"  speed="1"  style="width: 300px; height: 300px;"loop autoplay></lottie-player></center>
                <%} %>
                <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">
                    <asp:Repeater ID="GHRepeater" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-3 col-md-4 col-sm-5 col-6 my-2">
                                <div class="card">
                                    <!-- Possible change: modify size of picture space -->
                                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></h5>
                                        <p class="card-text">Number of Stocks: <%# DataBinder.Eval(Container.DataItem,"ProductStocks") %></p>
                                        <asp:Button ID="detailsButton" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-secondary" Text="Details" data-bs-toggle="modal" href="#detailsModal" OnClick="RetrieveDetails" runat="server"/>
                                        <asp:Button ID="deleteProduct" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-danger float-right" Text="Delete" data-bs-toggle="modal" href="#deleteModal" OnClick="RetrieveDetails" runat="server"/>                                       
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
    <p class="fs-4"><b>Hydroponics</b></p>
    <div class="container-fluid" style="background-color: #44433C; border: 2px solid #000000;">
        <asp:UpdatePanel ID="UpdatePanel_HPRepeater" runat="server">
            <ContentTemplate>
                <%if (listHPRepeater.Count == 0) %>
                <%{ %>
                <br>
                <br>
                <center><h3 style="color:white">No Items Found</h3></center>
                <center><lottie-player src="https://assets4.lottiefiles.com/temp/lf20_Celp8h.json" background="transparent"  speed="1"  style="width: 300px; height: 300px;"loop autoplay></lottie-player></center>
                <%} %>
                <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">
                    <asp:Repeater ID="HPRepeater" runat="server">
                        <ItemTemplate>
                            <div class="col-lg-3 col-md-4 col-sm-5 col-6 my-2">
                                <div class="card">
                                    <!-- Possible change: modify size of picture space -->
                                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></h5>
                                        <p class="card-text">Number of Stocks: <%# DataBinder.Eval(Container.DataItem,"ProductStocks") %></p>
                                        <asp:Button ID="detailsButton" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-secondary" Text="Details" data-bs-toggle="modal" href="#detailsModal" OnClick="RetrieveDetails" runat="server"/>
                                        <asp:Button ID="deleteProduct" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-danger float-right" Text="Delete" data-bs-toggle="modal" href="#deleteModal" OnClick="RetrieveDetails" runat="server"/>                                       
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

                <asp:UpdatePanel ID="UpdatePanel_DetailsModal" runat="server">
                    <ContentTemplate>                                                                                                                                         
                        <asp:Repeater ID="DetailsRepeater" runat="server">
                            <ItemTemplate>
                                <div class="modal-body" style="word-wrap"> 
                                    <p> Name: <%# DataBinder.Eval(Container.DataItem,"ProductName") %> </p>
                                    <p> ID: <%# DataBinder.Eval(Container.DataItem,"Product_ID") %> </p>
                                    <p> Description: <%# DataBinder.Eval(Container.DataItem,"ProductDescription") %> </p>
                                    <p> Category: <%# DataBinder.Eval(Container.DataItem,"ProductCategory") %> </p>
                                    <p> Image: <%# DataBinder.Eval(Container.DataItem,"ProductPicture") %> </p>
                                    <p> Number of Stocks: <%# DataBinder.Eval(Container.DataItem,"ProductStocks") %> </p>
                                    <p> Price: Php <%# DataBinder.Eval(Container.DataItem,"ProductPrice") %> </p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" onclick="$('#detailsModal').modal('hide');">Back</button> 
                                    <asp:Button ID="UpdateButton" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-primary float-right" Text="Update" OnClick="UpdateButton_Click" runat="server"/>                   
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
                                    <p> Are you sure you want to delete this product? </p>
                                    <p> Image: <%# DataBinder.Eval(Container.DataItem,"ProductPicture") %> </p>
                                    <p> Name: <%# DataBinder.Eval(Container.DataItem,"ProductName") %> </p>
                                    <p> Category: <%# DataBinder.Eval(Container.DataItem,"ProductCategory") %> </p>

                                    <!-- Collapsible Card -->
                                    <div class="card">
                                        <div class="card-header" id="detailsHeading">
                                            <h2 class="mb-0">
                                            <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#detailsCollapse" aria-expanded="false" aria-controls="detailsCollapse">
                                                Details
                                            </button>
                                            </h2>
                                        </div>
                                        <div id="detailsCollapse" class="collapse" aria-labelledby="detailsHeading" data-parent="#deleteModal">
                                            <div class="card-body">
                                                <li> ID: <%# DataBinder.Eval(Container.DataItem,"Product_ID") %> </li>
                                                <li> Description: <%# DataBinder.Eval(Container.DataItem,"ProductDescription") %> </li>
                                                <li> Number of Stocks: <%# DataBinder.Eval(Container.DataItem,"ProductStocks") %> </li>
                                                <li> Price: Php <%# DataBinder.Eval(Container.DataItem,"ProductPrice") %> </li>
                                                <li> Image: <%# DataBinder.Eval(Container.DataItem,"ProductPicture") %> </li>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" onclick="$('#deleteModal').modal('hide');">No</button> 
                                    <asp:Button ID="DeleteButton" CommandArgument='<%# Eval("Product_ID") %>' CssClass="btn btn-danger float-right" Text="Yes" OnClick="DeleteButton_Click" runat="server"/>                   
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>                            
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
        </div>
    </div>
                       
</asp:Content>
