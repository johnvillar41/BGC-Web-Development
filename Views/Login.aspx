<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SoftEngWebEmployee.Views.Login" Async="true" EnableEventValidation="false" %>

<!DOCTYPE html>
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
<html xmlns="http://www.w3.org/1999/xhtml">
<script src="//cdn.jsdelivr.net/npm/sweetalert2@10"></script>
<head>
    <title>Login 04</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700&amp;display=swap" rel="stylesheet">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="../Content/LoginCss.css" />
    
</head>
<body>
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
                        <form runat="server">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <h1 class="mb-3"><b>Welcome to AGT Seedlings Supply</b></h1>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label><b>Username</b></label>
                                            <asp:TextBox ID="txtbox_username" runat="server" placeholder="username" CssClass="form-control text-area" Height="35"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label><b>Password</b></label><br />
                                            <asp:TextBox ID="txtbox_password" runat="server" placeholder="password" CssClass="form-control text-area" Height="35"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Button ID="btn_login" runat="server" Text="Login" Style="font-family: 'Arial Rounded MT'" CssClass="btn btn-submit submit btn-blue btn-blue:hover" OnClick="btn_login_Click" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </form>                        
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
</body>
</html>
