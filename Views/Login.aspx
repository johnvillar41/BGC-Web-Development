<%@Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SoftEngWebEmployee.Views.Login" Async="true" EnableEventValidation="false" %>

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

        .main-head {
            height: 150px;
            background: #FFF;
        }

        .sidenav {
            height: 100%;
            background-color: #242526;
            overflow-x: hidden;
            padding-top: 20px;
        }


        .main {
            padding: 0px 10px;
        }

        @media screen and (max-height: 450px) {
            .sidenav {
                padding-top: 15px;
            }
        }

        @media screen and (max-width: 450px) {
            .login-form {
                margin-top: 10%;
            }

            .register-form {
                margin-top: 10%;
            }
        }

        @media screen and (min-width: 768px) {
            .main {
                margin-left: 30%;
            }

            .sidenav {
                width: 30%;
                position: fixed;
                z-index: 1;
                top: 0;
                left: 0;
            }

            .login-form {
                margin-top: 70%;
            }

            .register-form {
                margin-top: 20%;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div class="sidenav">
                <div class="login-main-text">
                    <center> <img src="/Images/logo.PNG"width="300" height="300" alt="...">
                    <h2 style="color:orange; font-family:Georgia;">  <b><br/> AGT</b> <br/>
                        Login Page </h2>  </center>

                </div>
            </div>
            <div class="main">
                <div class="col-md-6 col-sm-12">
                    <div class="login-form">
                        <form>
                            <div class="form-group">
                                <label><b>Username</b></label>
                                <asp:TextBox ID="txtbox_username" runat="server" class="form-control" placeholder="username"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label><b>Password</b></label>
                                <asp:TextBox ID="txtbox_password" runat="server" class="form-control" placeholder="password"></asp:TextBox>
                            </div>

                            <asp:Button ID="btn_login" runat="server" Text="Login" class="btn btn-black" OnClick="btn_login_Click" />

                        </form>
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
