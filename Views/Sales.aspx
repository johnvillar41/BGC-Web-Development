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
         <div class="table-bordered table-condensed table-responsive" style="height: 600px">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Activity</th>
                                    <th scope="col">Transaction</th>
                                    <th scope="col">Transacted by</th>
                                    <th scope="col">Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%for (int i = 0; i < 100; i++) %>
                                <%{ %>
                                <tr>
                                    <th scope="row">1</th>
                                    <td>Added</td>
                                    <td>Product(s) in the Inventory</td>
                                    <td>John Doe</td>
                                    <td>April 1, 2021</td>
                                </tr>
                                <tr>
                                    <th scope="row">2</th>
                                    <td>Updated</td>
                                    <td>Product(s) in the Inventory</td>
                                    <td>John Doe</td>
                                    <td>April 1, 2021</td>
                                </tr>
                                <tr>
                                    <th scope="row">3</th>
                                    <td>Deleted</td>
                                    <td>Product(s) in the Inventory</td>
                                    <td>John Doe</td>
                                    <td>April 1, 2021</td>
                                </tr>
                                <tr>
                                    <th scope="row">4</th>
                                    <td>Added</td>
                                    <td>Sales</td>
                                    <td>John Doe</td>
                                    <td>April 1, 2021</td>
                                </tr>
                            </tbody>
                            <%} %>
                        </table>

                    </div>
        </div>
    </div>

</asp:Content>
