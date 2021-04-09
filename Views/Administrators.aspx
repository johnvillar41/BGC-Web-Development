<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrators.aspx.cs" Inherits="SoftEngWebEmployee.Views.Administrators" Async="true" %>

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
            <div class="col-12" style="margin: 5px">
                <div class="card bd-callout bd-callout-warning" style="border-radius: .25rem">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-12">
                            <h3 class="float-left">User Information</h3>
                        </div>
                        <div class="col-lg-4 col-md-3">
                            <!--Empty Div-->
                        </div>
                        <%if (IsAdmin()) %>
                        <%{ %>
                        <div class="col-lg-5 col-md-6 col-sm-12">
                            <button type="button" class="btn btn-success float-end" style="margin: 2px;" data-bs-toggle="modal" data-bs-target="#AddNewUserModal">
                                Add New User
                            </button>
                            <button type="button" class="btn btn-danger float-end" style="margin: 2px;" data-bs-toggle="modal" data-bs-target="#DeleteUserModal">
                                Delete User
                            </button>
                            <button type="button" class="btn btn-info float-end" style="margin: 2px;" data-bs-toggle="modal" data-bs-target="#UpdateUserModal">
                                Update User
                            </button>
                        </div>
                        <%} %>

                        <div class="table-bordered table-condensed table-responsive" style="height: 500px">
                            <table class="table table-striped">
                                <thead>
                                    <tr>
                                        <th scope="col">User ID</th>
                                        <th scope="col">Username</th>
                                        <%if (IsAdmin()) %>
                                        <%{ %>
                                        <th scope="col">Password</th>
                                        <%} %>                                        
                                        <th scope="col">FullName</th>
                                        <th scope="col">Position</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%foreach (var admins in DisplayAdministrators())%>
                                    <%{ %>
                                    <tr>
                                        <td><%=admins.User_ID %></td>
                                        <td>
                                            <img src="/Images/logo.PNG" width="35" height="35" class="d-inline-block align-top" alt="">
                                            <%=admins.Username %>                                       
                                        </td>
                                        <%if (IsAdmin()) %>
                                        <%{ %>
                                        <td><%=admins.Password %></td>
                                        <%} %>                                        
                                        <td><%=admins.Fullname %></td>
                                        <td><%=admins.EmployeeType.ToString() %></td>
                                    </tr>
                                    <%} %>
                                </tbody>

                            </table>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Delete Modal-->
    <div class="modal fade" id="DeleteUserModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn btn-danger">
                    <h5 class="modal-title" id="exampleModalLabel">Delete User</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="exampleInputEmail1" class="form-label">User ID</label>
                        <asp:TextBox ID="AdministratorId_Delete" runat="server" type="text" class="form-control" required></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <asp:Button ID="BtnDelete" runat="server" Text="Delete User" CssClass="btn btn-danger" OnClick="BtnDelete_Click" UseSubmitBehavior="false" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>


    <!-- Modal Update User -->
    <div class="modal fade" id="UpdateUserModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn btn-info">
                    <h5 class="modal-title" id="exampleModalLabel">Update Administrator</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="exampleInputEmail1" class="form-label">User ID</label>
                        <asp:TextBox ID="AdministratorID" runat="server" type="text" class="form-control" required></asp:TextBox>
                        <asp:Button ID="ButtonFindID" Style="margin-top: 3px" type="submit" runat="server" Text="Find" CssClass="btn btn-info" OnClick="ButtonFindID_Click" UseSubmitBehavior="false" />

                    </div>
                    <hr />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="mb-3">
                                <label for="exampleInputEmail1" class="form-label">Username</label>
                                <%if (String.IsNullOrEmpty(UsernameUpdate.Text)) %>
                                <%{ %>
                                <asp:TextBox runat="server" type="text" disabled class="form-control" required></asp:TextBox>
                                <%} %>
                                <%else %>
                                <%{ %>
                                <asp:TextBox ID="UsernameUpdate" runat="server" type="text" class="form-control" required></asp:TextBox>
                                <%} %>
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputEmail1" class="form-label">Fullname</label>
                                <%if (String.IsNullOrEmpty(FullnameUpdate.Text)) %>
                                <%{ %>
                                <asp:TextBox runat="server" type="text" disabled class="form-control" required></asp:TextBox>
                                <%} %>
                                <%else %>
                                <%{ %>
                                <asp:TextBox ID="FullnameUpdate" runat="server" type="text" class="form-control" required></asp:TextBox>
                                <%} %>
                            </div>
                            <div class="mb-3">
                                <label for="exampleInputPassword1" class="form-label">Password</label>
                                <%if (String.IsNullOrEmpty(PasswordUpdate.Text)) %>
                                <%{ %>
                                <asp:TextBox runat="server" type="password" disabled class="form-control" required></asp:TextBox>
                                <%} %>
                                <%else %>
                                <%{ %>
                                <asp:TextBox ID="PasswordUpdate" runat="server" type="password" class="form-control" required></asp:TextBox>
                                <%} %>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ButtonFindID" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="ButtonUpdateUser" type="submit" runat="server" Text="Update" class="btn btn-info" OnClick="ButtonUpdateUser_Click" UseSubmitBehavior="false" />
                </div>
            </div>
        </div>
    </div>



    <!-- Modal Add New User -->
    <div class="modal fade" id="AddNewUserModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn btn-success">
                    <h5 class="modal-title" id="exampleModalLabel">Add new Administrator</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="exampleInputEmail1" class="form-label">Username</label>
                        <asp:TextBox ID="Username" runat="server" type="text" class="form-control" required></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="exampleInputEmail1" class="form-label">Fullname</label>
                        <asp:TextBox ID="FullName" runat="server" type="text" class="form-control" required></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="exampleInputPassword1" class="form-label">Password</label>
                        <asp:TextBox ID="Password" runat="server" type="password" class="form-control" required></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label class="form-label" for="customFile">Upload Picture</label>
                        <asp:FileUpload ID="ImageUpload" type="file" runat="server" class="form-control" />
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnSave" type="submit" runat="server" Text="Submit" class="btn btn-primary" OnClick="BtnSave_Click" UseSubmitBehavior="false" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>



</asp:Content>
