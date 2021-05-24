using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Models.ReportModel
{
    public class ProductSalesReportViewModel
    {
        public List<QuantitySoldModel> QuantitySold_Onsite { get; set; }
        public List<QuantitySoldModel> QuantitySold_Order { get; set; }
        public ProductSalesReportModel ProductReport { get; set; }
    }
}