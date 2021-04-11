<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Information.aspx.cs" Inherits="SoftEngWebEmployee.Views.Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Repeater Example</h3>

    <b>Repeater1:</b>

    <br />

    <asp:Repeater ID="InformationRepeater" runat="server"  onitemcommand="InformationRepeater_ItemCommand">
        <HeaderTemplate>
            <table border="1" class="table table-dark">
                <tr>
                    <td><b>Product_ID</b></td>
                    <td><b>ProductInformation</b></td>
                    <td><b>Action</b></td>
                </tr>
        </HeaderTemplate>

        <ItemTemplate>
            <tr>
                <td><%# DataBinder.Eval(Container.DataItem, "Product_ID") %>                      
                   
                </td>               
                <td><%# DataBinder.Eval(Container.DataItem, "ProductInformation") %> </td>
                <td><asp:Button runat="server" CssClass="btn btn-info" CommandName="InformationCommand" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ProductInformation") %>' ID="btnOriginal" Text="Original" /></td>                    
            </tr>
        </ItemTemplate>

        <FooterTemplate>
            </table>
        </FooterTemplate>

    </asp:Repeater>
    <br />
    <asp:Label ID="Label1" runat="server" Text="ID here"></asp:Label>


</asp:Content>
