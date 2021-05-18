<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SoftEngWebEmployee.Views.Login" Async="true" EnableEventValidation="false" %>

<!DOCTYPE html>
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Login 04</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700&amp;display=swap" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js" integrity="sha384-JEW9xMcG8R+pH31jmWH6WWP0WintQrMb4s7ZOdauHnUtxwoG2vI5DkLtS3qm9Ekf" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="../Content/LoginCss.css" />
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@10"></script>
</head>
<body>
    <form runat="server">
        <div class="container-fluid px-1 px-md-5 px-lg-1 px-xl-5 py-5 mx-auto">
            <div class="card card0 border-0">
                <div class="row d-flex">
                    <div class="col-lg-6">
                        <div class="card1 pb-5">
                            <div class="row">
                                <img src="../Images/logo.png" class="logo">
                            </div>
                            <div class="row px-3 justify-content-center mt-4 mb-5 border-line">
                                <img src="https://i.imgur.com/uNGdWHi.png" class="image">
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="card2 card border-0 px-4 py-5">

                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <h1 class="mb-3"><b>Welcome to AGT Seedlings Supply</b></h1>
                                    </div>
                                    <div class="row">
                                        <label><b>Username</b></label>
                                        <asp:TextBox ID="txtbox_username" runat="server" placeholder="username" CssClass="form-control text-area" Height="35"></asp:TextBox>

                                    </div>
                                    <div class="row">
                                        <label><b>Password</b></label><br />
                                        <asp:TextBox ID="txtbox_password" type="password" runat="server" placeholder="password" CssClass="form-control text-area" Height="35"></asp:TextBox>

                                    </div>
                                    <div class="row mt-5">
                                        <asp:Button ID="btn_login" runat="server" Text="Login" Style="font-family: 'Arial Rounded MT'" CssClass="btn btn-success form-control" OnClick="btn_login_Click" />
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="row">
                                <button type="button" class="btn btn-danger form-control" style="font-family: 'Arial Rounded MT'" data-bs-toggle="modal" data-bs-target="#codeModal">
                                    Forgot Password
                                </button>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="bg-orange py-4">
                    <div class="row px-3">
                        <small class="ml-4 ml-sm-5 mb-2">Copyright &copy; 2021. All rights reserved.</small>
                        <div class="social-contact ml-4 ml-sm-auto"><span class="fa fa-facebook mr-4 text-sm"></span><span class="fa fa-google-plus mr-4 text-sm"></span><span class="fa fa-linkedin mr-4 text-sm"></span><span class="fa fa-twitter mr-4 mr-sm-5 text-sm"></span></div>
                    </div>
                </div>
            </div>
        </div>

        <!--Modal Here-->
        <div class="modal fade" id="codeModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">

                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Enter Email</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="EmailTextBox" placeholder="Enter your email here!" runat="server"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <asp:Button ID="BtnSendCode" CssClass="btn btn-primary" runat="server" Text="Send Code" OnClick="BtnSendCode_Click"/>                        
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
