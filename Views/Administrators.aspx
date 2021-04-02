<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrators.aspx.cs" Inherits="SoftEngWebEmployee.Views.Administrators" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <button type="button" class="btn btn-primary" style="margin: 10px" data-bs-toggle="modal" data-bs-target="#exampleModal">
                Add New User
            </button>
        </div>

        <div class="row">
            <!--Add Dynamic Data here-->
            <%foreach (var admins in DisplayMockAdmins())%>
            <%{%>
            <div class="card" style="margin: 10px">
                <h5 class="card-header"><%=admins.User_ID.ToString() %></h5>
                <div class="card-body">
                    <img src="/Images/logo.PNG" class="img-thumbnail" alt="..." width="100" height="100">
                    <div class="row">
                        <div class="col-6">
                            <label for="username">Username</label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <input readonly type="email" class="form-control" id="username" aria-describedby="emailHelp" value="<%=admins.User_Username.ToString() %>" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <label for="password">Password</label>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom: 5px">
                        <div class="col-6">
                            <input readonly type="email" class="form-control" id="password" aria-describedby="emailHelp" value="<%=admins.User_Password.ToString() %>" />
                        </div>
                    </div>
                    <a href="#" class="btn btn-danger"><i class="fa fa-exclamation-circle" style="color: black"></i>Remove</a>
                    <a href="#" class="btn btn-warning"><i class="fa fa-check-square" style="color: black"></i>Update</a>
                </div>
            </div>
            <%}%>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
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

</asp:Content>
