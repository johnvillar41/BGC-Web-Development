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
    </style>
    <div class="container-fluid">
        <div class="row">
            <div class="col-12" style="margin: 5px">
                <div class="card bd-callout bd-callout-warning" style="border-radius: .25rem">
                    <div class="row">
                        <div class="col-3">
                            <h3 class="float-left">Business Transactions</h3>
                        </div>
                        <div class="col-3">
                            <!--Empty Div-->
                        </div>
                        <div class="col-6 ">
                            <div class="row">
                                <div class="col-6">
                                    <asp:TextBox class="form-control" ID="DateText" runat="server" type="date"></asp:TextBox>
                                </div>
                                <div class="col-6">
                                    <asp:Button ID="FindDate" runat="server" Text="Search" CssClass="btn btn-info form-control" OnClick="FindDate_Click" />
                                </div>
                            </div>

                        </div>

                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="table-bordered table-condensed table-responsive" style="height: 600px">
                                <table class="table table-striped">
                                    <thead>
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

                                        <%foreach (var notification in DisplayNotifications()) %>
                                        <%{ %>
                                        <tr>
                                            <td><%=notification.Notifications_ID%></td>
                                            <%if (notification.TypeOfNotification == Constants.NotificationType.CancelledOrder)%>
                                            <%{ %>
                                            <td><span class="badge bg-danger">Cancelled</span></td>
                                            <%} %>
                                            <%else if (notification.TypeOfNotification == Constants.NotificationType.CreateUser)%>
                                            <%{ %>
                                            <td><span class="badge bg-success">Created</span></td>
                                            <%} %>
                                            <%else if (notification.TypeOfNotification == Constants.NotificationType.DeleteUser)%>
                                            <%{ %>
                                            <td><span class="badge bg-danger">Deleted</span></td>
                                            <%} %>
                                            <%else if (notification.TypeOfNotification == Constants.NotificationType.FinishedOrder)%>
                                            <%{ %>
                                            <td><span class="badge bg-success">Finished</span></td>
                                            <%} %>
                                            <%else if (notification.TypeOfNotification == Constants.NotificationType.UpdateUser)%>
                                            <%{ %>
                                            <td><span class="badge bg-info">Updated</span></td>
                                            <%} %>
                                            <td><%=notification.NotificationTitle %></td>
                                            <td><%=notification.NotificationContent %></td>
                                            <td><%=notification.Username %></td>
                                            <td><%=notification.NotificationDate %></td>
                                        </tr>
                                        <%} %>
                                    </tbody>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="FindDate" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
