<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true"  CodeBehind="InventoryAdd.aspx.cs" Inherits="SoftEngWebEmployee.Views.InventoryAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        #count_message {
          background-color: #4b4444;
          color: white;
          margin-top: -20px;
          margin-right: 5px;
          font-family: Arial, Helvetica, sans-serif;
          border-radius: 7px;
        }
    </style>
    
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

        var text_max = 200;
        $('#count_message').html('0 / ' + text_max);
        function charCount() {            
            $('#textCount').keyup(function () {
                var text_length = $('#textCount').val().length;
                var text_remaining = text_max - text_length;

                $('#count_message').html(text_length + ' / ' + text_max);
            });
        }
        
    </script>

    <!-- Script: Character counter -->

    <p class="fs-4"><b>Add Product</b></p>
    <br>
    <p class="fs-5">Product Details</p>
    

    <!-- Possible sources for character counter
        https://www.codeply.com/go/s0F9Iz38yn/bootstrap-textarea-with-character-count-_-bootstrap-3
        https://jsfiddle.net/djibe89/knv43w6t
        https://stackoverflow.com/questions/5371089/count-characters-in-textarea
        https://dev.to/websolutionstuff/character-count-in-textarea-48p3

    -->

        <textarea class="form-control" id="textCount" name="textCount" 
      	    maxlength="200" placeholder="Type in your message" rows="5" onkeypress="return charCount()">            
        </textarea>
        <label class="pull-right label label-default" id="count_message" for="textCount"></label>
            

    

    <div class="row">
        <div class="col-8">
            <!-- Product Name -->
            <div class="row mb-3">
                <label><i>Name</i></label>
                <asp:TextBox ID="addProductName" runat="server" placeholder="Enter product name here" CssClass="form-control text-area" Height="35" maxlength="255"></asp:TextBox>               
            </div> 

            <!-- Product Category -->
            <div class="row mb-3">
                <label><i>Category</i></label>
                <div class="col-12">
                    <div class="row">
                        <div class="col-8">
                            <div class="row">
                                <asp:TextBox ID="addProductCategory" runat="server" placeholder="Enter product category here" CssClass="form-control text-area" Height="35" maxlength="255"></asp:TextBox>
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
                        <input type="text" class="textfield" value="" id="addProductPrice" name="addProductPrice" placeholder="Enter product price here" maxlength="255" onkeypress="return isNumber(event)" onpaste="return false;"/>
                    </div>                    
                </div>
                <div class="col-6">
                    <div class="row">
                        <label><i>Stocks</i></label>
                        <input type="text" class="textfield" value="" id="addProductStocks" name="addProductStocks" placeholder="Enter stock amount here" maxlength="255" onkeypress="return isNumber(event)" onpaste="return false;"/>
                    </div>
                </div>
            </div>

            <!-- Product Description -->
            <div class="row mb-3">
                <div class="form-group">
                    <div class="row">
                     <label for="addProductDescription"><i>Description</i></label>
                        <textarea class="form-control" rows="3" id="addProductDescription" placeholder="Enter description here" maxlength="1000"></textarea>
                    </div>
                </div>
            </div>
        
        </div>
    
        <div class="col-4">

        </div>
    </div>
    
</asp:Content>
