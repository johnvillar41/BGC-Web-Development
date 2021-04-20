<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Information.aspx.cs" Inherits="SoftEngWebEmployee.Views.Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .bd-callout {
            padding: 1.25rem;
            margin-top: 1.25rem;
            margin-bottom: 1.25rem;
            border: 1px solid #e9ecef;
            border-left-width: .25rem;
            border-radius: .25rem;
        }

        .bd-callout-warning {
            border-left-color: #f0ad4e;
        }
    </style>
    <div class="container">
          
        <div class="row">
           
            <div class="col-12">
                 
                <div class="card bd-callout bd-callout-warning" style="border-radius: .25rem">
                    <h3>Information</h3>
                    <asp:Repeater ID="InformationRepeater" runat="server" OnItemCommand="InformationRepeater_ItemCommand">
                        <HeaderTemplate>
                            <div class="table-bordered table-condensed table-responsive" style="height: 500px">
                                <table border="1" class="table table-striped">
                                    <tr>
                                        <td style="width:10%"><b>Product ID</b></td>
                                        <td style="width:10%"><b>ProductName</b></td>
                                        <td style="width:10%"><b>ProductDescription</b></td>
                                        <td style="width:10%"><b>ProductPicture</b></td>
                                        <td style="width:50%"><b>ProductInformation</b></td>
                                        <td style="width:10%"><b>Action</b></td>
                                    </tr>
                        </HeaderTemplate>

                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "Product.Product_ID") %>                  
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem,"Product.ProductName") %>
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem,"Product.ProductDescription") %>
                                </td>
                                <td>                                    
                                   <img alt="" height="100px" width="100px" src="data:image/png;base64,<%# Eval("Product.ProductPicture") %>" />
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "ProductInformation") %> 
                                </td>
                                <td>                                                                  
                                    <asp:Button runat="server" CssClass="btn btn-info" CommandName="InformationCommand" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Product.Product_ID") %>' ID="btnOriginal" Text="Update" />
                                </td>
                            </tr>
                        </ItemTemplate>

                        <FooterTemplate>
                            </table>
                            </div>
                            
                        </FooterTemplate>

                    </asp:Repeater>
                    <br />                    
                </div>
            </div>
        </div>
    </div>

   
</asp:Content>
