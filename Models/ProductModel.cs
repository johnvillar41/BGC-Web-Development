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
        public override bool Equals(object obj)
        {
            return obj is ProductModel model &&
                   Product_ID == model.Product_ID &&
                   ProductName == model.ProductName &&
                   ProductDescription == model.ProductDescription &&
                   ProductPrice == model.ProductPrice &&
                   ProductPicture == model.ProductPicture &&
                   ProductStocks == model.ProductStocks &&
                   ProductCategory == model.ProductCategory;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}