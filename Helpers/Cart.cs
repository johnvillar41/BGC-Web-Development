using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Helpers
{
    public class Cart
    {
        private static List<ProductModel> CartItems { get; set; }
        public static void ClearCartItems()
        {
            CartItems.Clear();
        }
        public static void AddCartItem(ProductModel cartProduct)
        {
            CartItems.Add(cartProduct);
        }
        public static void RemoveCartItem(ProductModel cartProduct)
        {
            foreach(var product in CartItems)
            {
                if(product.Equals(cartProduct))
                {
                    CartItems.Remove(product);
                    break;
                }
            }
        }
    }
}