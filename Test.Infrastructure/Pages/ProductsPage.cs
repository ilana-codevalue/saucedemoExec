using OpenQA.Selenium;
using System.Linq;
using Test.Infrastructure.Models;

namespace Test.Infrastructure.Pages
{
    public class ProductsPage(IWebDriver driver) : BasePage(driver)
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

        public override bool IsPageLoaded()
        {
            return Driver.WaitForDisplayed(productsList, 3);
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
                productEl.FindElement(productPrice).Text.Replace("$",""),
                productEl.FindElement(productImage),
                productEl.FindElement(productAddToCartBtn)
            );
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

        public ProductsPage AddOrRemoveProductToCart(IWebElement productEl, bool IsRemove = false)
        {
            if(IsRemove)
            {
                if (GetProductAddToCartBtnText(productEl) == "Remove")
                {
                    productEl.FindElement(productAddToCartBtn).Click();
                }
                
            }
            else
            {
                if (GetProductAddToCartBtnText(productEl) == "Add to cart")
                {
                    productEl.FindElement(productAddToCartBtn).Click();
                }
            }
            
            return this;
        }

        public string GetProductAddToCartBtnText(IWebElement productEl) => productEl.FindElement(productAddToCartBtn).Text;

        public ProductPage ClickOnProduct(IWebElement prodictEl)
        {
            Driver.FindElement(prodictEl, productName).Click();
            return new ProductPage(driver);
        }

        public ProductPage SelectRandomProductAndGoToProductPage()
        {
            var allProductsElements = GetAllProductElements();
            var rnd = new Random();
            int randomProductNumber = rnd.Next(0, allProductsElements.Count - 1);
            Driver.FindElement(allProductsElements[randomProductNumber], productName).Click();
            
            return new ProductPage(driver);
        }

        public CartPage CreatSampleCartListAndGoToCartPage()
        {
            var allProductsElements = GetAllProductElements();
            
            Driver.FindElement(allProductsElements[0], productAddToCartBtn).Click();
            Driver.FindElement(allProductsElements[1], productAddToCartBtn).Click();
            Driver.FindElement(allProductsElements[3], productAddToCartBtn).Click();
            ClickOnCartIcon();

            return new CartPage(driver);
        }
        
        public IWebElement? GetProductElByName(string name)
        {
            var allProductsElements = GetAllProductElements();
            return allProductsElements.Find(el => Driver.FindElement(el, productName).Text == name);

        }
        public ProductPage SelectProductByNameAndGoToProductPage(string name)
        {
            var productEl = GetProductElByName(name);  
            if (productEl != null)
            {
                Driver.FindElement(productEl, productName).Click();
            }

            return new ProductPage(driver);
        }
    }
}