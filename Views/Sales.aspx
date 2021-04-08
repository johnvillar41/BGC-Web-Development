<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="SoftEngWebEmployee.Views.Sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>Sales Here</p>

    <!-- First modal dialog -->
    <div class="modal fade" id="modal" aria-hidden="true" aria-labelledby="..." tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-xl">
            <div class="modal-content">
                Modal 1 Text
      <div class="modal-footer">
          <!-- Toogle to second dialog -->
          <button class="btn btn-primary" data-bs-target="#modal2" data-bs-toggle="modal" data-bs-dismiss="modal">Open #modal2</button>
      </div>
            </div>
        </div>
    </div>
    <!-- Second modal dialog -->
    <div class="modal fade" id="modal2" aria-hidden="true" aria-labelledby="..." tabindex="-1">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                ...
      <div class="modal-footer">
          <!-- Toogle to first dialog, `data-bs-dismiss` attribute can be omitted - clicking on link will close dialog anyway -->
          <!-- <a class="btn btn-primary" href="#modal" data-bs-toggle="modal" role="button">Open #modal</a> -->
          <a class="btn btn-primary" href="#modal" data-bs-toggle="modal" data-bs-dismiss="modal" role="button">Open #modal</a>
      </div>
            </div>
        </div>
    </div>
    <!-- Open first dialog -->
    <a class="btn btn-primary" data-bs-toggle="modal" href="#modal" role="button">Open #modal</a>
    <div>


    </div>

    <div>
        <div class="card" style="width: 18rem;">
            <div class="card-header">   
                Featured
            </div>
            <ul class="list-group list-group-flush">
                <li class="list-group-item">Cras justo odio</li>
                <li class="list-group-item">Dapibus ac facilisis in</li>
                <li class="list-group-item">Vestibulum at eros</li>
            </ul>
        </div>
    </div>

</asp:Content>
