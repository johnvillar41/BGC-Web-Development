namespace SoftEngWebEmployee.Models
{
    public class ProductSalesReportModel
    {
        public ProductModel Product { get; set; }
        /// <summary>
        ///     Total product count for column total_product_count
        /// </summary>
        public int QuantitySold { get; set; } 
        /// <summary>
        ///     Sum of rows in total_sale
        /// </summary>
        public int ProductRevenue { get; set; }
    }
}