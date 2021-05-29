<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true"  CodeBehind="InventoryAdd.aspx.cs" Inherits="SoftEngWebEmployee.Views.InventoryAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="../Content/LoginCss.css" />

    <!-- Script: Check if price+stocks input is number -->
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if ((charCode > 31 && charCode < 48) || charCode > 57) {
                return false;
            }
            return true;
        }
    </script>

    <style>

    </style>
    
    <p class="fs-4"><b>Add Product</b></p>
    <hr />    
    
    <div class="container-fluid px-1 mx-auto">
        
        <div class="card card0">
            <p class="fs-5 ml-2">Product Details</p>
            <div class="row d-flex">
                <div class="col-xl-8 col-lg-8 col-md-6 col-12">
                    <div class="card1 px-2 py-2">
                        <!-- Product Name -->
                        <div class="input-group mb-3">
                            <span class="input-group-text" id="addLabelName"><i>Name</i></span>
                            <asp:TextBox ID="addProductName" runat="server" placeholder="Enter product name here" CssClass="form-control text-area col-12 my-0" maxlength="255" aria-describedby="addLabelName"></asp:TextBox>             
                        </div> 
            
                        <div class="row mb-3">
                            <div class="col-xl-6 col-lg-6 col-12">
                                <!-- Product Category -->
                                <div class="input-group mb-3">                
                                    <span class="input-group-text" id="addLabelCategory"><i>Category</i></span>
                                    <asp:UpdatePanel ID="UpdatePanel_AddCategory" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="addProductCategory" runat="server" placeholder="Enter product category here" CssClass="form-control text-area col-12 my-0" maxlength="255" aria-describedby="addLabelCategory"></asp:TextBox>
                                            <div class="btn-group dropdown">
                                                <asp:Button ID="addProductDropdown" CssClass="btn btn-warning dropdown-toggle dropdown-toggle-split col-12 my-0" data-bs-toggle="dropdown" aria-expanded="false" runat="server" Text="Select existing category &#x25BC;" />
                                                <ul class="dropdown-menu dropdown-menu-dark" aria-labelledby="addProductDropdown">
                                                    <asp:Repeater ID="AddCategoryRepeater" OnItemCreated="AddCategoryRepeater_ItemCreated" runat="server">
                                                        <ItemTemplate>
                                                            <a runat="server" class="dropdown-item" id="categorySelected">
                                                                <li><asp:Button ID="category" runat="server" CssClass="dropdown-item" Text='<%#Container.DataItem%>' OnClick="AddCategory_Click" UseSubmitBehavior="false"/></li>                               
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="addProductDropdown" EventName="Click" />
                                        </Triggers>                                  
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div class="col-xl-6 col-lg-6 col-12">
                                <!-- Product Price -->
                                <div class="input-group mb-1">
                                    <span class="input-group-text" id="addLabelPrice"><i>Price</i></span>
                                    <asp:TextBox ID="addProductPrice" runat="server" placeholder="Enter product name here" onkeypress="return isNumber(event)" onpaste="return false;"
                                        CssClass="form-control text-field col-12 my-0" maxlength="255" aria-describedby="addLabelPrice"></asp:TextBox> 
                                </div>

                                <!-- Product Stocks -->
                                <div class="input-group">
                                    <span class="input-group-text" id="addLabelStocks"><i>Stocks</i></span>
                                    <asp:TextBox ID="addProductStocks" runat="server" placeholder="Enter stock amount here" onkeypress="return isNumber(event)" onpaste="return false;"
                                        CssClass="form-control text-field col-12 my-0" maxlength="255" aria-describedby="addLabelStocks"></asp:TextBox> 
                                </div>
                            </div>
                        </div>

                        <!-- Product Description -->
                        <div class="input-group mb-3">
                            <span class="input-group-text col-12 my-0" id="addLabelDesc"><i>Description</i></span>
                            <asp:TextBox TextMode="MultiLine" ID="addProductDescription" CssClass="form-control col-12 my-0" runat="server"
                                placeholder="Enter description here" maxlength="1000" rows="4" aria-describedby="addLabelDesc"></asp:TextBox>
                        </div>
                    </div>
                    
                </div>

                <div class="col-xl-4 col-lg-4 col-md-6 col-12">
                    <div class="card2 card border-0 px-2 py-2">
                        <!-- Product Picture -->
                        <div class="justify-content-center">
                            <div class="row my-3">
                                <img alt="" class="rounded-circle" width="200" height="200" src="data:image/png;base64,<%=ImageString%>" />
                            </div>            
                            <div class="row">
                                <div class="input-group mb-3">
                                    <asp:FileUpload type="file" ID="addProductPicture" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                        </div>                    
                    </div>
                </div>
        
            </div>

            <div class="bg-dark py-4">                
                <asp:Button ID="btnAddProduct" CssClass="btn btn-warning ml-1" Text="Add Product" OnClick="btnAddProduct_Click" runat="server" />
            </div>

        </div>

    </div>
       
</asp:Content>


