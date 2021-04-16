using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Helpers
{
    public class Cart
    {
        private static List<ProductModel> CartItems = new List<ProductModel>();       
        public static void ClearCartItems()
        {
            CartItems.Clear();
        }
        public static void AddCartItem(ProductModel cartProduct)
        {
            if (cartProduct != null)
            {
                CartItems.Add(cartProduct);
            }            
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