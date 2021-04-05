<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="SoftEngWebEmployee.Views.Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p><b>Inventory</b></p>

    <div class="btn-group" role="group" aria-label="Button group with nested dropdown">

        <button type="button" class="btn btn-secondary">Greenhouse</button>

    
        <button type="button" class="btn btn-secondary">Hydroponics</button>

        <button type="button" class="btn btn-secondary">Seeds</button>
  
    </div>

    <div class="card" style="width: 18rem;">
            <img class="card-img-top" src="..." alt="Card image cap">
            <div class="card-body">
                <h5 class="card-title">Card title</h5>
                <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                <a href="#" class="btn btn-primary">Go somewhere</a>
            </div>
        </div>




</asp:Content>
