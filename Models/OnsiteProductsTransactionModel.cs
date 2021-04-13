using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Models
{
    public class OnsiteProductsTransactionModel
    {
        public int OnsiteTransactionID { get; set; }
        public OnsiteTransactionModel OnsiteTransactionModel { get; set; }
        public ProductModel Product { get; set; }
        public int TotalProductsCount { get; set; }
    }
}