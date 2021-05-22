namespace SoftEngWebEmployee.Models
{
    public class ProductModel
    {
        public int Product_ID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public string ProductPicture { get; set; }
        public int ProductStocks { get; set; }
        public string ProductCategory { get; set; }
        /// <summary>
        ///     This property is for cart user only
        ///     This will consider the total number of products
        ///     inside the cart.
        /// </summary>
        public int TotalNumberOfProduct { get; set; }
        public int SubTotalPrice { get; set; }     
    }
}