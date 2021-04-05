using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Models
{
    public class SpecificOrdersModel
    {
        public int SpecificOrdersId { get; set; }
        public int OrdersID { get; set; }
        public int ProductID { get; set; }
        public int TotalOrders { get; set; }
        public ProductsModel ProductsModel { get; set; }
    }
}