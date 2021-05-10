using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Models
{
    public class OnsiteProductsTransactionModel
    {
        public int OnsiteProductTransactionID { get; set; }
        public int TransactionID { get; set; }
        public string Administrator { get; set; }
        public ProductModel Product { get; set; }
        public int TotalProductsCount { get; set; }
        public int SubTotalPrice { get; set; }
        public override string ToString()
        {
            string generateStringVal =
                "Transaction ID: " + TransactionID + "\n" +
                "Product: " + Product.ProductName + "\n";                
            return generateStringVal;
        }
    }
}