using SoftEngWebEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftEngWebEmployee.Helpers
{
    public class Cart
    {
        private static readonly Dictionary<string, List<ProductModel>> CartItems = new Dictionary<string, List<ProductModel>>();

        public static void ClearCartItems()
        {
            foreach (KeyValuePair<string, List<ProductModel>> keyValuePair in CartItems)
            {
                if (keyValuePair.Key.Equals(UserSession.SingleInstance.GetLoggedInUser()))
                {
                    keyValuePair.Value.Clear();
                    return;
                }
            }
        }
        public static void AddCartItem(ProductModel cartProduct)
        {
            if (!CartItems.Keys.Contains(UserSession.SingleInstance.GetLoggedInUser()))
            {
                CartItems.Add(UserSession.SingleInstance.GetLoggedInUser(), new List<ProductModel>() { cartProduct });
                return;
            }

            foreach (KeyValuePair<string, List<ProductModel>> keyValuePair in CartItems)
            {
                if (keyValuePair.Key.Equals(UserSession.SingleInstance.GetLoggedInUser()))
                {
                    for (int i = 0; i < keyValuePair.Value.Count; i++)
                    {
                        if (keyValuePair.Value[i].Product_ID == cartProduct.Product_ID)
                        {
                            keyValuePair.Value[i].TotalNumberOfProduct = cartProduct.TotalNumberOfProduct;
                            return;
                        }
                        else
                        {
                            keyValuePair.Value.Add(cartProduct);
                            return;
                        }
                    }
                }
            }
        }
        public static List<ProductModel> GetCartItems()
        {
            foreach (KeyValuePair<string, List<ProductModel>> keyValuePair in CartItems)
            {
                if (keyValuePair.Key.Equals(UserSession.SingleInstance.GetLoggedInUser()))
                {
                    return keyValuePair.Value;
                }
            }
            return new List<ProductModel>();
        }
        public static int CalculateTotalSales()
        {
            int totalSale = 0;
            foreach (KeyValuePair<string, List<ProductModel>> keyValuePair in CartItems)
            {
                if (keyValuePair.Key.Equals(UserSession.SingleInstance.GetLoggedInUser()))
                {
                    for (int i = 0; i < keyValuePair.Value.Count; i++)
                    {
                        totalSale += (keyValuePair.Value[i].ProductPrice * keyValuePair.Value[i].TotalNumberOfProduct);
                    }
                }
            }
            return totalSale;
        }
        public static void RemoveCartItem(int productID)
        {
            foreach (KeyValuePair<string, List<ProductModel>> keyValuePair in CartItems)
            {
                if (keyValuePair.Key.Equals(UserSession.SingleInstance.GetLoggedInUser()))
                {
                    for (int i = 0; i < keyValuePair.Value.Count; i++)
                    {
                        if (keyValuePair.Value[i].Product_ID == productID)
                        {
                            keyValuePair.Value.Remove(keyValuePair.Value[i]);
                            break;
                        }
                    }
                }
            }
        }
        public static List<OnsiteProductsTransactionModel> ListOfOnsiteProducts(int transactionID)
        {
            List<OnsiteProductsTransactionModel> OnSiteProducts = new List<OnsiteProductsTransactionModel>();
            foreach (KeyValuePair<string, List<ProductModel>> keyValuePair in CartItems)
            {
                for (int i = 0; i < keyValuePair.Value.Count; i++)
                {
                    OnSiteProducts.Add(
                       new OnsiteProductsTransactionModel
                       {
                           TransactionID = transactionID,
                           Product = keyValuePair.Value[i],
                           TotalProductsCount = keyValuePair.Value[i].TotalNumberOfProduct,
                           Administrator = UserSession.SingleInstance.GetLoggedInUser(),
                           SubTotalPrice = keyValuePair.Value[i].TotalNumberOfProduct * keyValuePair.Value[i].ProductPrice
                       }
                   );
                }
            }
            return OnSiteProducts;
        }
    }
}