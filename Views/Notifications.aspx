<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeBehind="Notifications.aspx.cs" Inherits="SoftEngWebEmployee.Views.Notifications" %>

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
                        <div class="col-6">
                            <!--Empty Div-->
                        </div>
                        <div class="col-3 ">
                            <input type="date" id="birthday" name="birthday" class="form-control">
                        </div>

                    </div>

                    <div class="table-bordered table-condensed table-responsive" style="height: 600px">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
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
                                    <td><%=notification.NotificationTitle %></td>
                                    <td><%=notification.NotificationContent %></td>
                                    <td><%=notification.Username %></td>
                                    <td><%=notification.NotificationDate %></td>
                                </tr>                               
                                <%} %>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
