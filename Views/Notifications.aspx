<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeBehind="Notifications.aspx.cs" Inherits="SoftEngWebEmployee.Views.Notifications" %>

<%@ Import Namespace="SoftEngWebEmployee.Helpers" %>
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

        #overlayDiv {
            position: fixed;
            left: 50%;
            top: 50%;
            -webkit-transform: translate(-50%, -50%);
            transform: translate(-50%, -50%);
            z-index: 99;
        }
    </style>
    <div class="container-fluid">
        <div class="row">
            <div class="col-12" style="margin: 5px">
                <div class="card bd-callout bd-callout-warning" style="border-radius: .25rem">
                    <div class="row">
                        <h3 class="float-left">Notifications</h3>
                    </div>
                    <div class="row m-1">
                        <div class="col-md-3 col-sm-12">
                            <div class="row mb-1">
                                <h5><b>Search by Date</b></h5>
                            </div>
                            <div class="row mb-1">
                                <asp:TextBox class="form-control" ID="DateText" runat="server" type="date"></asp:TextBox>
                            </div>
                            <div class="row">
                                <asp:Button ID="FindDate" runat="server" Text="Search" CssClass="btn btn-info form-control" OnClick="FindDate_Click" />
                            </div>
                        </div>
                        <%if (UserSession.SingleInstance.IsAdministrator()) %>
                        <%{ %>
                        <div class="col-md-3 col-sm-12">
                            <div class="row mb-1">
                                <h5><b>Search by Employees</b></h5>
                            </div>
                            <div class="row">
                            </div>
                            <div class="row">
                                <div class="btn-group">
                                    <!-- Dropdown Button -->
                                    <asp:Button ID="DropDownEmployee" CssClass="btn btn-warning dropdown-toggle dropdown-toggle-split" data-bs-toggle="dropdown" aria-expanded="false" runat="server" Text="Select Employee &#x25BC;" />
                                    <!-- Dropdown List -->
                                    <ul class="dropdown-menu dropdown-menu-dark" aria-labelledby="dropdownMenuReference1">
                                        <li>
                                            <asp:Button ID="BtnEmployeeAll" runat="server" CssClass="dropdown-item" Text="All Employee" UseSubmitBehavior="false" OnClick="BtnEmployeeAll_Click"/></li>
                                        <li>
                                            <hr class="dropdown-divider">
                                        </li>

                                        <asp:Repeater ID="EmployeeFullnameRepeater" OnItemCreated="EmployeeFullnameRepeater_ItemCreated1" runat="server">
                                            <ItemTemplate>
                                                <a runat="server" class="dropdown-item" id="categorySelected">
                                                    <li>
                                                        <asp:Button ID="EmployeeFullnameCategory" runat="server" CssClass="dropdown-item" Text='<%#DataBinder.Eval(Container.DataItem,"Username")%>' UseSubmitBehavior="false" OnClick="EmployeeFullnameCategory_Click" />
                                                    </li>
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <%} %>
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <div id="overlayDiv">
                                <lottie-player src="https://assets8.lottiefiles.com/packages/lf20_LqA9yY.json" background="transparent" speed="1" style="width: 400px; height: 400px;" loop autoplay></lottie-player>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="table-bordered table-condensed table-responsive" style="height: 600px">
                                <table class="table table-borderless table-hover">
                                    <%if (NotificationsList == null) return; %>
                                    <%if (NotificationsList.Count() == 0) %>
                                    <%{ %>
                                    <div>
                                        <h1>
                                            <center>No Results Found!</center>
                                        </h1>
                                        <center><img src="/Images/cancell.PNG" style="border-radius: 50%" width="100" height="100" class="d-inline-block align-top" alt="" /></center>
                                    </div>
                                    <%} %>
                                    <%else %>
                                    <%{ %>
                                    <thead class="thead-dark">
                                        <tr>
                                            <th scope="col">#</th>
                                            <th scope="col">Tag</th>
                                            <th scope="col">Activity</th>
                                            <th scope="col">Transaction</th>
                                            <th scope="col">Transacted by</th>
                                            <th scope="col">Date</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%foreach (var notification in NotificationsList) %>
                                        <%{ %>
                                        <tr>
                                            <td><%=notification.Notifications_ID%></td>
                                            <%if (notification.TypeOfNotification == Constants.NotificationType.CancelledOrder)%>
                                            <%{ %>
                                            <td><span class="badge bg-danger">
                                                <center>Cancelled</center>
                                                <img src="/Images/cancell.PNG" style="border-radius: 50%" width="35" height="35" class="d-inline-block align-top" alt="" />
                                            </span></td>
                                            <%} %>
                                            <%else if (notification.TypeOfNotification == Constants.NotificationType.CreateUser)%>
                                            <%{ %>
                                            <td><span class="badge bg-success">
                                                <center>Created</center>
                                                <img src="/Images/add_product.PNG" style="border-radius: 50%" width="35" height="35" class="d-inline-block align-top" alt="" />
                                            </span></td>
                                            <%} %>
                                            <%else if (notification.TypeOfNotification == Constants.NotificationType.DeleteUser)%>
                                            <%{ %>
                                            <td>
                                                <span class="badge bg-danger">
                                                    <center>Deleted</center>
                                                    <img src="/Images/delete_logo.PNG" style="border-radius: 50%" width="35" height="35" class="d-inline-block align-top" alt="" />
                                                </span></td>
                                            <%} %>
                                            <%else if (notification.TypeOfNotification == Constants.NotificationType.FinishedOrder)%>
                                            <%{ %>
                                            <td><span class="badge bg-success">
                                                <center>Finished</center>
                                                <img src="/Images/check.PNG" style="border-radius: 50%" width="35" height="35" class="d-inline-block align-top" alt="" />
                                            </span></td>
                                            <%} %>
                                            <%else if (notification.TypeOfNotification == Constants.NotificationType.UpdateUser)%>
                                            <%{ %>
                                            <td><span class="badge bg-info">
                                                <center>Updated</center>
                                                <img src="/Images/update_icon_order.PNG" style="border-radius: 50%" width="35" height="35" class="d-inline-block align-top" alt="" />
                                            </span></td>
                                            <%} %>
                                            <td><%=notification.NotificationTitle %></td>
                                            <td><%=notification.NotificationContent %></td>
                                            <td><%=notification.Administrator.Fullname %></td>
                                            <td><%=notification.NotificationDate %></td>
                                        </tr>
                                        <%} %>
                                        <%} %>
                                    </tbody>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="FindDate" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="DropDownEmployee" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="BtnEmployeeAll" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
