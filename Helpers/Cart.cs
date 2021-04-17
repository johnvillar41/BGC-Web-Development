﻿using SoftEngWebEmployee.Models;
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
        public static int CalculateTotalSales()
        {
            int totalSale = 0;
            foreach(var product in CartItems)
            {
                totalSale += product.ProductPrice;
            }
            return totalSale;
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
        public static List<OnsiteProductsTransactionModel> ListOfOnsiteProducts(int transactionID)
        {
            List<OnsiteProductsTransactionModel> OnSiteProducts = new List<OnsiteProductsTransactionModel>();
            foreach(var product in CartItems)
            {
                OnSiteProducts.Add(
                        new OnsiteProductsTransactionModel
                        {                            
                            TransactionID = transactionID,                         
                            Product = product,
                            TotalProductsCount = CartItems.Count
                        }
                    );
            }
            return OnSiteProducts;
        }
    }
}