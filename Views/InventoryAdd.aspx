<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true"  CodeBehind="InventoryAdd.aspx.cs" Inherits="SoftEngWebEmployee.Views.InventoryAdd" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <p class="fs-4"><b>Add Product</b></p>
    <br>
    <br>
    <p class="fs-5">Product Details</p>

    <div class="row">
        <div class="col-8">
            <!-- Product Name -->
            <div class="row mb-3">
                <label><i>Name</i></label>
                <asp:TextBox ID="addProductName" runat="server" placeholder="Enter product name here" CssClass="form-control text-area" Height="35"></asp:TextBox>
            </div> 
            <!-- Product Category -->
            <div class="row mb-3">
                <label><i>Category</i></label>
                <div class="col-12">
                    <div class="row">
                        <div class="col-8">
                            <div class="row">
                                <asp:TextBox ID="addProductCategory" runat="server" placeholder="Enter product category here" CssClass="form-control text-area" Height="35"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-4">
                            <asp:Button ID="addProductDropdown" CssClass="btn btn-warning dropdown-toggle dropdown-toggle-split" data-bs-toggle="dropdown" aria-expanded="false" runat="server" Text="Select existing category &#x25BC;" />
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
                    </div>
                </div>
                
                            
            </div>
            <!-- Product Price+Stocks -->
            <div class="row mb-3">
                <div class="col-6">
                    <div class="row">
                        <label><i>Price</i></label>
                        <asp:TextBox ID="addProductPrice" runat="server" placeholder="Enter product price here" CssClass="form-control text-area" Height="35"></asp:TextBox>
                    </div>                    
                </div>
                <div class="col-6">
                    <div class="row">
                        <label><i>Stocks</i></label>
                        <asp:TextBox ID="addProductStocks" runat="server" placeholder="Enter stock amount here" CssClass="form-control text-area" Height="35"></asp:TextBox>
                    </div>
                </div>
            </div>
            <!-- Product Description -->
            <div class="row mb-3">
                <div class="form-group">
                    <div class="row">
                     <label for="addProductDescription"><i>Description</i></label>
                        <textarea class="form-control" rows="3" id="addProductDescription" placeholder="Enter description here"></textarea>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-4">

        </div>
    </div>

</asp:Content>
