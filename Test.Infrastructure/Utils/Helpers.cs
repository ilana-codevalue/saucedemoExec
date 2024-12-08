using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Test.Infrastructure.Models;
using Test.Infrastructure.Pages;

namespace Test.Infrastructure.Utils
{
    public class Helpers
    {

       public static List<Product> GetAllProductByPrice(double UserPrice, ProductsPage productsPage)
        {
            var selectedProducts = new List<Product>();
            var allProductElements = productsPage.GetAllProductElements();
            foreach (IWebElement el in allProductElements)
            {
                var product = productsPage.GetProductDetails(el);
                if (double.Parse(product.price) > UserPrice)
                {
                    selectedProducts.Add(product);
                    productsPage.AddOrRemoveProductToCart(el);
                }
            }

            return selectedProducts;
        }

        public static double GetCheckoutCalculatedSummarySubTotal(List<Product> cartlist)
        {
            double calculatedSummarySubTotal = 0;
            foreach (var item in cartlist.Select(x => x.price))
            {
                calculatedSummarySubTotal += double.Parse(item);
            }
            return calculatedSummarySubTotal;
        }

        public static bool VerifyProductsDetails(List<IWebElement> productsElements, ProductsPage productsPage)
        {
            foreach (var product in productsElements)
            {
                if (!VerifyProductDetails(product, productsPage))
                    return false;
            }

            return true;
        }

        public static bool VerifyProductDetails(IWebElement productEl, ProductsPage productsPage)
        {
            var product = productsPage.GetProductDetails(productEl);
            return product.name != string.Empty &&
                product.description != string.Empty &&
                product.price != string.Empty &&
                product.Image != null &&
                product.Button != null;
        }
    }
}
