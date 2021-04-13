using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Models
{
    public class OnsiteTransactionModel
    {
        public int TransactionID { get; set; }
        public CustomerModel Customer { get; set; }
        public int TotalSale { get; set; }
    }
}