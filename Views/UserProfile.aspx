<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="SoftEngWebEmployee.Views.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container rounded bg-white mt-5">
        <div class="row">
            <div class="col-md-4 border-right">
                <div class="d-flex flex-column align-items-center text-center p-3 py-5">

                    <img alt="" class="rounded-circle mt-5" width="200" height="200" src="data:image/png;base64,<%=ImageString%>" />
                    <div>
                        <br />

                    </div>

                    <div class="input-group mb-3">                        
                        <asp:FileUpload type="file" ID="ProfileFileUpload" CssClass="form-control" runat="server" />    
                        <asp:Button ID="UploadImage" CssClass="btn btn-info" runat="server" Text="Upload" OnClick="UploadImage_Click" />
                    </div>

                    <asp:Label ID="FullnameLabel" CssClass="font-weight-bold" runat="server" Text="Label"></asp:Label>
                    <%if (EmployeeType == SoftEngWebEmployee.Helpers.Constants.EmployeeType.Administrator)%>
                    <%{%>
                    <span class="badge bg-dark"><%=EmployeeType.ToString() %></span>
                    <%}%>
                    <%else %>
                    <%{ %>
                    <span class="badge bg-secondary text-dark"><%=EmployeeType.ToString()  %></span>
                    <%} %>
                </div>
            </div>
            <div class="col-md-8">
                <div class="p-3 py-5">
                    <div class="d-flex justify-content-between align-items-center mb-3">

                        <h6 class="text-right">User Profile</h6>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <div class="form-floating mb-3">
                                <asp:TextBox type="text" ID="Fullname" class="form-control" placeholder="FullName" runat="server"></asp:TextBox>
                                <label for="floatingInput">FullName</label>
                            </div>
                        </div>

                    </div>
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <div class="form-floating mb-3">
                                <asp:TextBox type="text" ID="Username" class="form-control" placeholder="Username" runat="server"></asp:TextBox>
                                <label for="floatingInput">Username</label>
                            </div>
                        </div>

                    </div>
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <div class="form-floating mb-3">
                                <asp:TextBox type="text" ID="Password" class="form-control" placeholder="Password" runat="server"></asp:TextBox>
                                <label for="floatingInput">Password</label>
                            </div>
                        </div>
                        <div class="col-md-6">
                        </div>
                    </div>
                    <div class="mt-5">                        
                        <asp:Button ID="ButtonSaveProfile" class="btn btn-primary profile-button" runat="server" Text="Save Profile" OnClick="ButtonSaveProfile_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>




</asp:Content>
