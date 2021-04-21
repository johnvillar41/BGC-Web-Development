using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Models.ReportModel
{
    public class SalesIncomeReportViewModel
    {
        public AdministratorModel Administrator { get; set; }
        public int TotalSale { get; set; }
        public int TotalSaleOrders { get; set; }
        public int TotalSaleOnsite { get; set; }
    }
}