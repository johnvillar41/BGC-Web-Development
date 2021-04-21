using System;

namespace SoftEngWebEmployee.Models
{
    public class QuantitySoldModel
    {
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ProductID { get; set; }
        public string SaleType { get; set; }
        public DateTime Date { get; set; }
        public string Administrator { get; set; }
        public int TotalSale { get; set; }
        public int ProductCount { get; set; }
    }
}