<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SoftEngWebEmployee.Views.Login" Async="true" EnableEventValidation="false" %>

<!DOCTYPE html>
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
<html xmlns="http://www.w3.org/1999/xhtml">
<script src="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
<script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
<script src="//cdn.jsdelivr.net/npm/sweetalert2@10"></script>
<script src="https://unpkg.com/@lottiefiles/lottie-player@latest/dist/lottie-player.js"></script>
<head runat="server">
    <title></title>
    <style>
        .center {
            margin: auto;
            width: 90%;
            border: 3px solid green;
            padding: 10px;
            position: initial;
        }

        .centerRight {
            margin-left: auto;
            margin-right: auto;
            width: 300px;
            margin-top: auto;
            margin-bottom: auto;
        }

        .centerPage {
            left: 50%;
            top: 50%;
            margin-left: -10px;
            /* -1/2 width */
            margin-top: -10px;
            /* -1/2 height */
        }

        .parentDiv {
            display: flex;
            justify-content: center;
            align-items: center;
            margin-left: 100px;

        }

        body {
            display: flex;
            flex-direction: column;
            justify-content: center;
            min-height: 100vh;
            background-color: lightyellow;
        }

        .vertical-center {
            margin: 0;
            top: 50%;
            -ms-transform: translateY(-50%);
            transform: translateY(-50%);
        }
        .submit {
            background: #eba800;
            color: black;
            box-shadow: none;
            border: none
        }
    </style>
</head>
<body>

    <div class="container-fluid center p-3" style="background-color: lightblue">
        <div class="row">
            <div class="col-lg-8 col-md-6 col-sm-12">
                <center><img class="animated-gif img-fluid" src="../Images/money-tree.gif" style="min-height:300px;min-width:300px;"></center>
            </div>
            <div class="col-lg-4 col-md-6 col-sm-12 centerRight ">
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
                                    <asp:TextBox ID="txtbox_username" runat="server" placeholder="username" CssClass="form-control" Height="35"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label><b>Password</b></label><br />
                                    <asp:TextBox ID="txtbox_password" runat="server" placeholder="password" CssClass="form-control" Height="35"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row">
                                <asp:Button ID="btn_login" runat="server" Text="Login" style="font-family:'Arial Rounded MT'" CssClass="btn btn-submit submit" OnClick="btn_login_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
