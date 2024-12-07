﻿using OpenQA.Selenium;
using System.Linq;

namespace Test.Infrastructure.Pages
{
    public class ProductsPage : BasePage
    {

        protected readonly By productsList = By.CssSelector(".inventory_list");
        protected readonly By product = By.CssSelector(".inventory_item");
        protected readonly By productName = By.CssSelector(".inventory_item_name");
        protected readonly By productDescription = By.CssSelector(".inventory_item_desc");
        protected readonly By productPrice = By.CssSelector(".inventory_item_price");
        protected readonly By productAddToCartBtn = By.CssSelector(".btn_small.btn_inventory");
        protected readonly By productImage = By.CssSelector(".inventory_item_img");
        protected readonly By productSortingBtn = By.CssSelector(".product_sort_container");
        protected readonly By sortingBtnActiveOption = By.CssSelector(".active_option");


        public ProductsPage(IWebDriver driver) : base(driver)
        {

        }

        public override bool IsPageLoaded()
        {
            return Driver.WaitForDisplayed(productsList);
        }

        public List<IWebElement> GetAllProductElements()
        {
            return Driver.FindElements(product).ToList();
        }

        public Product GetProductDetails(IWebElement productEl)
        {
            return new Product(
                productEl.FindElement(productName).Text,
                productEl.FindElement(productDescription).Text,
                productEl.FindElement(productPrice).Text,
                productEl.FindElement(productImage),
                productEl.FindElement(productAddToCartBtn)
            );
        }

        public bool VerifyProductsDetails(List<IWebElement> productsElements)
        {
            foreach (var product in productsElements)
            {
                if (!VerifyProductDetails(product))
                    return false;
            }

            return true;
        }

        public bool VerifyProductDetails(IWebElement productEl)
        {
            var product = GetProductDetails(productEl);
            return product.name != string.Empty &&
                product.description != string.Empty &&
                product.price != string.Empty &&
                product.Image != null &&
                product.Button != null;
        }

        public List<string> GetProductNameList()
        {
            var products = GetAllProductElements()
                .Select(product => GetProductDetails(product)).ToList();

            return products.Select(p => p.name).ToList();
        }

        public List<double> GetProductPriceList()
        {
            var products = GetAllProductElements()
                .Select(product => GetProductDetails(product)).ToList();

            List<string> stringPriceList = products.Select(p => p.price).ToList();
            return stringPriceList.Select(double.Parse).ToList();
        }

        public string SelectSortingProducts(string sortingOption)
        {
            Driver.SelectFromDropDown(productSortingBtn, sortingOption);
            return GetSortingBtnActiveOption();
        }

        private string GetSortingBtnActiveOption()
        {
            return Driver.FindElement(sortingBtnActiveOption).Text;
        }

        public void AddProductToCart(IWebElement productEl)
        {
            productEl.FindElement(productAddToCartBtn).Click();
        }

        public string GetProductAddToCartBtnText(IWebElement productEl) => productEl.FindElement(productAddToCartBtn).Text;

    }
}