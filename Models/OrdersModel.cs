using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Models
{
    public class OrdersModel
    {
        public int Order_ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public double OrderTotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public string OrderDate { get; set; }
        public int TotalNumberOfOrders { get; set; }
        public List<SpecificOrdersModel> SpecificOrdersModel { get; set; }

        public static explicit operator List<object>(OrdersModel v)
        {
            throw new NotImplementedException();
        }
    }
}