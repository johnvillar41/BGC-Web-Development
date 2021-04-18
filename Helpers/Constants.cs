using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Helpers
{
    public class Constants
    {
        public enum NotificationType
        {
            DeleteUser,
            UpdateUser,
            CreateUser,
            CancelledOrder,
            FinishedOrder,
            SoldItem
        }
        public enum SalesType { Onsite, Order }
        public enum EmployeeType { Administrator, Employee }
        public enum UserStatus { Active, InActive }
        public enum AlertStatus { success, error, warning, info, question }
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