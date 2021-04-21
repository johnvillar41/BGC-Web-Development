using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Models.ViewModels
{
    public class ProductSalesReportViewModel
    {
        public List<QuantitySoldModel> QuantitySold { get; set; }
        public ProductSalesReportModel ProductReport { get; set; }
    }
}