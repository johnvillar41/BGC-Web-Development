using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Models
{
    public class Constants
    {
        public enum NotificationType
        {
            DeleteUser,
            UpdateUser,
            CreateUser,
            CancelledOrder,
            FinishedOrder
        }
        public enum SalesType { Onsite, Order }
        public enum EmployeeType { Administrator, Employee }
    }
}