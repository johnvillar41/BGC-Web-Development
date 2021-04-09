<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="SoftEngWebEmployee.Views.Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .scrolling-wrapper{
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

    <!-- Text that disappears/changes depending on search results. This is when page is initially loaded.-->
    <p class="fs-5"><i>Use the search bar to display products.</i></p>
    <!-- In case of a search with no results, "No results found."-->
    <!-- Take note to modify so that the code generates a card for every result, 4 cards per row, then top to bottom-->

    <hr />
    <p class="fs-4"><b>Greenhouse</b></p>

    <!-- Guide Source: https://codepen.io/Temmio/pen/gKGEYV -->
    <div class="container-fluid" style="background-color:#44433C; border:2px solid #cecece;">
        <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">

            <%for (int i = 0; i < 25; i++) %>
            <%{ %>
                <div class="col-lg-3 col-md-4 col-sm-5 col-6 my-2">
                    <div class="card">
                        <!-- Possible change: modify size of picture space -->
                        <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                        <div class="card-body">
                            <h5 class="card-title">Product Name</h5>
                            <p class="card-text">Insert product details and remaining number of stocks here.</p>
                            <a class="btn btn-primary" data-bs-toggle="modal" href="#updateProduct">View Details</a>
                        </div>
                    </div>
                </div>               
            <%} %>

        </div>
    </div>


    <hr />
    <p class="fs-4"><b>Hydroponics</b></p>

    <div class="container-fluid" style="background-color:#44433C; border:2px solid #cecece;">
        <div class="scrolling-wrapper row flex-row flex-nowrap mt-4 pb-4 pt-2">

            <%for (int i = 0; i < 25; i++) %>
            <%{ %>
                <div class="col-lg-3 col-md-4 col-sm-5 col-6 my-2">
                    <div class="card">
                        <!-- Possible change: modify size of picture space -->
                        <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                        <div class="card-body">
                            <h5 class="card-title">Product Name</h5>
                            <p class="card-text">Insert product details and remaining number of stocks here.</p>
                            <a class="btn btn-primary" data-bs-toggle="modal" href="#updateProduct">View Details</a>
                        </div>
                    </div>
                </div>               
            <%} %>

        </div>
    </div>

    <!-- Modal: Update Product-->
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

    <!-- Modal: Add Product-->
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

</asp:Content>
