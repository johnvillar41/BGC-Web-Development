using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Models
{
    public class ProductSalesReportModel
    {
        public String ProductName { get; set; }
        public int UnitPrice { get; set; }
        public int QuantitySold { get; set; }
        public int ProductRevenue { get; set; }
    }
}