<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SoftEngWebEmployee.Views.Login" Async="true" EnableEventValidation="false" %>

<%@ Import Namespace="SoftEngWebEmployee.Helpers" %>
<!DOCTYPE html>
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>Login 04</title>
    <meta charset="utf-8">
    <link rel="icon" href="<%= ResolveUrl("~/Images/logo.png") %>" type="image/png" />
    <link rel="shortcut icon" href="<%= ResolveUrl("~/Images/logo.png") %>" type="image/png" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700&amp;display=swap" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js" integrity="sha384-JEW9xMcG8R+pH31jmWH6WWP0WintQrMb4s7ZOdauHnUtxwoG2vI5DkLtS3qm9Ekf" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="../Content/LoginCss.css" />
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@10"></script>
    <script src="https://unpkg.com/@lottiefiles/lottie-player@latest/dist/lottie-player.js"></script>
    <style>
        .centerBlock {
            display: table;
            margin: auto;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div class="container-fluid px-1 px-md-5 px-lg-1 px-xl-5 py-5 mx-auto">
            <div class="centerBlock">
            <div class="card card0 border-0">
                <div class="row d-flex">
                    <div class="col-lg-6">
                        <div class="card1 pb-5">
                            <div class="row">
                                <img src="../Images/logo.png" class="logo">
                            </div>
                            <div class="row px-3 justify-content-center mt-4 mb-5 border-line">
                                <lottie-player src="https://assets3.lottiefiles.com/packages/lf20_wrrmnklf.json" background="transparent" speed="1" style="width: 400px; height: 400px;" loop autoplay></lottie-player>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="centerBlock">
                        <div class="card2 card0 card border-0 px-4 py-5 bg-dark">
                            <div class="container">

                                <div class="row">
                                    <div class="col-lg-8 col-md-10 col-sm-12">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <h1 class="mb-3 text-warning"><b>Welcome to <%=Constants.BGC %> Seedlings Supply</b></h1>
                                                </div>
                                                <div class="row">
                                                    <label class="text-warning"><b>Username</b></label>
                                                    <asp:TextBox ID="txtbox_username" runat="server" placeholder="username" CssClass="form-control text-area" Height="35"></asp:TextBox>

                                                </div>
                                                <div class="row">
                                                    <label class="text-warning"><b>Password</b></label><br />
                                                    <asp:TextBox ID="txtbox_password" type="password" runat="server" placeholder="password" CssClass="form-control text-area" Height="35"></asp:TextBox>

                                                </div>
                                                <div class="row mt-5">
                                                    <asp:Button ID="btn_login" runat="server" Text="Login" Style="font-family: 'Arial Rounded MT'" CssClass="btn btn-warning form-control" OnClick="btn_login_Click" />
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnSendCode" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="row">
                                            <button type="button" class="btn btn-warning form-control mb-5" style="font-family: 'Arial Rounded MT'" data-bs-toggle="modal" data-bs-target="#emailModal">
                                                Forgot Password
                                            </button>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                            </div>
                    </div>
                </div>
                <div class="bg-dark py-4">
                    <div class="row px-3">
                        <small class="ml-4 ml-sm-5 mb-2 text-warning">Copyright &copy; 2021. All rights reserved.</small>
                        <div class="social-contact ml-4 ml-sm-auto"><span class="fa fa-facebook mr-4 text-sm"></span><span class="fa fa-google-plus mr-4 text-sm"></span><span class="fa fa-linkedin mr-4 text-sm"></span><span class="fa fa-twitter mr-4 mr-sm-5 text-sm"></span></div>
                    </div>
                </div>
            </div>
                </div>
        </div>

        <!--Modal Here-->
        <!--Modal Enter Email-->

        <div class="modal fade" id="emailModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">

                    <div class="modal-header bg-dark">
                        <h5 class="modal-title text-warning" id="exampleModalLabel">Enter Email</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="EmailTextBox" placeholder="Enter your email here!" runat="server"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-dark" data-bs-dismiss="modal">Close</button>
                        <asp:Button ID="BtnSendCode" CssClass="btn btn-warning" runat="server" Text="Send Code" OnClick="BtnSendCode_Click" data-bs-toggle="modal" data-bs-target="#codeModal" />
                    </div>
                </div>
            </div>
        </div>
        <!--Modal Enter Code and New Password-->
        <div class="modal fade" id="codeModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header bg-dark">
                                <h5 class="modal-title text-warning">Enter Code</h5>
                                <button type="button" class="btn-dark" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                <asp:TextBox ID="CodeConfirmation" placeholder="Enter code confirmation here!" runat="server"></asp:TextBox>
                                <%if (IsCodeConfirmed) %>
                                <%{ %>
                                <asp:TextBox ID="NewPassword" placeholder="Enter new Password!" runat="server"></asp:TextBox>
                                <%} %>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="BtnConfirmCode" CssClass="btn btn-warning" runat="server" Text="Send Code" OnClick="BtnConfirmCode_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BtnConfirmCode" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
