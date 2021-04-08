<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="SoftEngWebEmployee.Views.Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p class="fs-2"><b>Inventory</b></p>

    <!--
    <nav class="navbar navbar-light bg-light">
        <form class="form-inline">
            <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
            <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
        </form>
    </nav>
    
    <div class="input-group">
        <div class="form-outline">
            <input type="search" id="form1" class="form-control" />
            <label class="form-label" for="form1">Search</label>
        </div>
        <button type="button" class="btn btn-primary">
            <i class="fas fa-search"></i>
        </button>
    </div>
    

    <div class="input-group">
        <div class="form-outline">
            <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
        </div>
        <button class="btn btn-outline-success my-2 my-sm-0" type="submit">Search</button>
    </div>
    -->



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

    <!-- Will images have standard size? Or will this be coded to adjust image size automatically? -->

    <div class="container">
        <div class="row justify-content-start my-2">
            <div class="col-md-6 col-lg-3">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div> 
            <div class="col-md-6 col-lg-3 col-sm-12">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-lg-3 col-sm-12">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-lg-3 col-sm-12">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row justify-content-start">
            <div class="col-3">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
            <div class="col-3">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
            <div class="col-3">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
            <div class="col-3">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <hr />
    <p class="fs-4"><b>Hydroponics</b></p>

    <div class="container">
        <div class="row justify-content-start my-2">
            <div class="col-3">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
            <div class="col-3">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
            <div class="col-3">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
            <div class="col-3">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row justify-content-start">
            <div class="col-3">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
            <div class="col-3">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
            <div class="col-3">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
            <div class="col-3">
                <div class="card">
                    <img class="card-img-top" src="/Images/logo.PNG" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">Product Name</h5>
                        <p class="card-text">Insert product details and remaining number of stocks here.</p>
                        <a href="#" class="btn btn-primary">View Details</a>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
        <button type="button" class="btn btn-secondary">Greenhouse</button>
        <button type="button" class="btn btn-secondary">Hydroponics</button>
        <button type="button" class="btn btn-secondary">Seeds</button>
    </div>

    <div class="card" style="width: 18rem;">
        <img class="card-img-top" src="..." alt="Card image cap">
        <div class="card-body">
            <h5 class="card-title">Product Name</h5>
            <p class="card-text">Insert product details and remaining number of stocks here.</p>
            <a href="#" class="btn btn-primary">View Details</a>
        </div>
    </div>
    </div>
</asp:Content>
