using System;
using static SoftEngWebEmployee.Models.Constants;

namespace SoftEngWebEmployee.Models
{
    public class SalesModel
    {
        public int Sales_ID { get; set; }
        public string SalesTitle { get; set; }
        public string SalesImage { get; set; }
        public double SalesTransactionValue { get; set; }
        public int Product_ID { get; set; }
        public int TotalNumberOfProducts { get; set; }
        public DateTime SalesDate { get; set; }
        public int DateMonth { get; set; }
        public string User_Username { get; set; }
        public SalesType TypeOfSale { get; set; }
       
    }
}