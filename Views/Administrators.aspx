<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrators.aspx.cs" Inherits="SoftEngWebEmployee.Views.Administrators" %>

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
    <div class="container-fluid">
        <div class="row">
            <div class="col-12" style="margin: 5px">
                <div class="card bd-callout bd-callout-warning" style="border-radius: .25rem">
                    <div class="row">
                        <div class="col-3">
                            <h3 class="float-left">User Information</h3>
                        </div>
                        <div class="col-6">
                            <!--Empty Div-->
                        </div>
                        <div class="col-3 ">
                            <button type="button" class="btn btn-success float-end" style="margin: 5px;" data-bs-toggle="modal" data-bs-target="#AddNewUserModal">
                                Add New User
                            </button>
                        </div>

                    </div>

                    <div class="table-bordered table-condensed table-responsive">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th scope="col">User ID</th>
                                    <th scope="col">Username</th>
                                    <th scope="col">Password</th>
                                    <th scope="col">FullName</th>
                                    <th scope="col">Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%foreach (var admins in DisplayMockAdmins())%>
                                <%{ %>
                                <tr>
                                    <td><%=admins.User_ID %></td>
                                    <td>
                                        <img src="/Images/logo.PNG" width="35" height="35" class="d-inline-block align-top" alt="">
                                        <%=admins.User_Username %>                                       
                                    </td>
                                    <td><%=admins.User_Password %></td>
                                    <td><%=admins.User_Name %></td>
                                    <td>
                                        <button type="button" style="height: 40px; width: 100px" class="btn btn-danger">Delete</button>
                                        <button type="button" style="height: 40px; width: 100px" class="btn btn-warning">Update</button>
                                    </td>
                                </tr>
                                <%} %>
                            </tbody>

                        </table>

                    </div>
                    <nav aria-label="Page navigation example">
                        <ul class="pagination">
                            <li class="page-item">
                                <a class="page-link" href="#" aria-label="Previous">
                                    <span aria-hidden="true">&laquo;</span>
                                </a>
                            </li>
                            <li class="page-item"><a class="page-link" href="#">1</a></li>
                            <li class="page-item"><a class="page-link" href="#">2</a></li>
                            <li class="page-item"><a class="page-link" href="#">3</a></li>
                            <li class="page-item">
                                <a class="page-link" href="#" aria-label="Next">
                                    <span aria-hidden="true">&raquo;</span>
                                </a>
                            </li>
                        </ul>
                    </nav>
                </div>
            </div>

        </div>



        <!-- Modal -->
        <div class="modal fade" id="AddNewUserModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Add new Administrator</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <label for="exampleInputEmail1" class="form-label">Username</label>
                            <input type="email" class="form-control" aria-describedby="emailHelp">
                        </div>
                        <div class="mb-3">
                            <label for="exampleInputEmail1" class="form-label">Fullname</label>
                            <input type="email" class="form-control" aria-describedby="emailHelp">
                        </div>
                        <div class="mb-3">
                            <label for="exampleInputPassword1" class="form-label">Password</label>
                            <input type="password" class="form-control">
                        </div>
                        <div class="mb-3">
                            <label class="form-label" for="customFile">Upload Picture</label>
                            <input type="file" class="form-control" id="customFile" />
                        </div>

                        <button type="submit" class="btn btn-primary">Submit</button>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary">Save changes</button>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
