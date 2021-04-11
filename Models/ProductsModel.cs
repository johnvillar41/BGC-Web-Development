using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SoftEngWebEmployee.Models
{
    public class ProductsModel
    {
        public int Product_ID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public string ProductPicture { get; set; }
        public int ProductStocks { get; set; }
        public string ProductCategory { get; set; }       
    }
}