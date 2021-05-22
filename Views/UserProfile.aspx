<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="SoftEngWebEmployee.Views.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        body {
            background: #fdfd96;
        }

        .profile-button {
            background: #eba800;
            color: black;
            box-shadow: none;
            border: none
        }

            .profile-button:hover {
                background: #876100;
            }

            .profile-button:focus {
                background: #876100;
                box-shadow: none
            }

            .profile-button:active {
                background: #876100;
                box-shadow: none
            }

        .rounded-lg {
            background: #f5f5dc;
        }

        #overlayDiv {
            position: fixed;
            left: 50%;
            top: 50%;
            -webkit-transform: translate(-50%, -50%);
            transform: translate(-50%, -50%);
            z-index: 99;
        }
    </style>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="overlayDiv">
                <lottie-player src="https://assets8.lottiefiles.com/packages/lf20_LqA9yY.json" background="transparent" speed="1" style="width: 400px; height: 400px;" loop autoplay></lottie-player>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="container rounded-lg mt-5" style="border: 5px solid orange">
        <div class="row">
            <div class="col-md-4 border-right">
                <div class="d-flex flex-column align-items-center text-center p-3 py-5">


                    <img alt="" class="rounded-circle mt-5" width="200" height="200" src="data:image/png;base64,<%=ImageString%>" />
                    <div>
                        <br />

                    </div>

                    <div class="input-group mb-3">
                        <asp:FileUpload type="file" ID="ProfileFileUpload" CssClass="form-control" runat="server" />
                        <asp:Button ID="UploadImage" CssClass="btn btn-info" runat="server" Style="background-color: #eba800; border-color: none" Text="Upload" OnClick="UploadImage_Click" />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="FullnameLabel" CssClass="font-weight-bold" runat="server" Text="Label"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ButtonSaveProfile" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
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

                        <h6 class="text-right"><b><u>USER PROFILE</u></b></h6>
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
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <div class="form-floating mb-3">
                                <asp:TextBox type="text" ID="Email" class="form-control" placeholder="Email" runat="server"></asp:TextBox>
                                <label for="floatingInput">Email</label>
                            </div>
                        </div>
                        <div class="col-md-6">
                        </div>
                    </div>
                    <div class="mt-5">
                        <asp:Button ID="ButtonSaveProfile" class="btn btn-primary profile-button" runat="server" Text="Save Profile" OnClick="ButtonSaveProfile_Click" ForeColor="Black" />
                    </div>
                </div>


            </div>

        </div>
    </div>
    
</asp:Content>
