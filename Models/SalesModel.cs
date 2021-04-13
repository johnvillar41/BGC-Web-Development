using System;
using static SoftEngWebEmployee.Helpers.Constants;

namespace SoftEngWebEmployee.Models
{
    public class SalesModel
    {
        public int SalesID { get; set; }
        public SalesType SalesType { get; set; }
        public AdministratorModel Administrator { get; set; }
        public DateTime Date { get; set; }
        public OrdersModel Orders { get; set; }
        public OnsiteTransactionModel OnsiteTransaction { get; set; }
    }
}