<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SoftEngWebEmployee.Views.Login" Async="true" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
<script src="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
<script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
<head runat="server">
    <title></title>
    <style>
        body {
            font-family: "Lato", sans-serif;
            background: #ffae42;
        }

        .sidenav {
            height: 100%;
            background-color: #242526;
            overflow-x: hidden;
            padding-top: 10px;
        }

        .main {
            padding: 0px 0px;
        }

        @media screen and (max-height: 450px) {
            .sidenav {
                padding-top: 15px;
            }
        }

        @media screen and (max-width: 450px) {
            .login-form {
                margin-top: 100%;
            }
        }

        @media screen and (min-width: 768px) {
            .main {
                margin-left: 30%;
                margin-top: 5%;
            }

            .sidenav {
                width: 30%;
                position: fixed;
                z-index: 10;
                top: 0;
                left: 0;
            }

            .login-form {
                margin-top: 50%;
                margin-left: 5%;
            }
        }


        .login-main-text {
            margin-top: 20%;
            padding: 60px;
            color: #fff;
        }

        .login-main-text h2 {
            font-weight: 300;
        }

        .btn-black {
            background-color: #000 !important;
            color: #fff;
        }

        img {
            width: 70%;
            height: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div class="sidenav">
                <div class="login-main-text">
                    <center><img src="/Images/logo.PNG" alt="AGT Logo"></center>
                    <h2 style="color: orange; font-family: Georgia;">
                        <b>
                            <br />
                            <center>
                            AGT Seedling Supply
                        </b>
                        <br />
                        Login Page </h2>
                    </center>

                </div>
            </div>
            <div class="main">
                <div class="col-md-6 col-sm-12">
                    <div class="login-form">

                        <div class="form-group">
                            <label><b>Username</b></label><br />
                            <asp:TextBox ID="txtbox_username" runat="server" placeholder="username" Width="400" Height="35"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label><b>Password</b></label><br />
                            <asp:TextBox ID="txtbox_password" runat="server" placeholder="password" Width="400" Height="35"></asp:TextBox>
                        </div>

                        <asp:Button ID="btn_login" runat="server" Text="Login" class="btn btn-black" OnClick="btn_login_Click" />


                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
