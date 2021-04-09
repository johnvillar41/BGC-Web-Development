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
        }
        public enum SalesType { Onsite, Order }
    }
}