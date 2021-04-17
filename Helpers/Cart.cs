using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Helpers
{
    public class Cart
    {
        private static readonly List<ProductModel> CartItems = new List<ProductModel>();       
        public static void ClearCartItems()
        {
            CartItems.Clear();
        }
        public static void AddCartItem(ProductModel cartProduct)
        {
            foreach(var product in CartItems)
            {
                if(product.Product_ID == cartProduct.Product_ID)
                {
                    product.TotalNumberOfCartItems = cartProduct.TotalNumberOfCartItems;
                    return;
                }
            }
            CartItems.Add(cartProduct);
        }
        public static List<ProductModel> GetCartItems()
        {
            return CartItems;
        }
        public static void RemoveCartItem(int productID)
        {
            foreach(var product in CartItems)
            {
                if(product.Product_ID == productID)
                {
                    CartItems.Remove(product);
                    break;
                }
            }
        }
    }
}