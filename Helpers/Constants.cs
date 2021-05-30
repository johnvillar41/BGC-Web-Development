using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Helpers
{
    public class Constants
    {
        public const string BGC = "BGC";
        public enum NotificationType
        {
            DeleteUser,
            UpdateUser,
            CreateUser,
            CancelledOrder,
            FinishedOrder,
            SoldItem,
            AddedProduct,
            UpdatedProduct
        }
        public static class NotificationTypeDefinitions
        {
            public const string DELETE_USER = "Deleted User";
            public const string CREATED_NEW_USER = "Created New User";
            public const string UPDATED_USER = "Updated User";
            public const string CANCELLED_ORDER = "Cancelled Order";
            public const string FINISHED_ORDER = "Finished Order";
            public const string SOLD_ITEM = "Sold Item";
            public const string ADDED_PRODUCT = "Added Product";
            public const string UPDATED_PRODUCT = "Updated Product";
        }
        public enum SalesType { Onsite, Order }
        public enum EmployeeType { Administrator, Employee }
        public enum UserStatus { Active, InActive }
        public enum AlertStatus { success, error, warning, info, question }
        public enum OrderStatus { Pending, Cancelled, Finished }
        public static class AlertPositions
        {
            public const string TOP = "top";
            public const string TOP_START = "top-start";
            public const string TOP_END = "top-end";
            public const string CENTER = "center";
            public const string CENTER_START = "center-start";
            public const string CENTER_END = "center-end";
            public const string BOTTOM = "bottom";
            public const string BOTTOM_START = "bottom-start";
            public const string BOTTOM_END = "bottom-end";
        }
    }
}