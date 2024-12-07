using OpenQA.Selenium;

namespace Test.Infrastructure.Pages
{
    public class ProductPage : BasePage
    {
        protected readonly By productName = By.CssSelector(".inventory_details_name");
        protected readonly By productDescription = By.CssSelector(".inventory_details_desc");
        protected readonly By productPrice = By.CssSelector(".inventory_details_price");
        protected readonly By productAddToCartBtn = By.CssSelector(".btn_small.btn_inventory");
        protected readonly By productImage = By.CssSelector(".inventory_details_img");
        protected readonly By backToProductsBtn = By.CssSelector("#back-to-products");

        public ProductPage(IWebDriver driver): base(driver) { }


        public Product GetProductDetails()
        {
            return new Product(
                Driver.FindElement(productName).Text,
                Driver.FindElement(productDescription).Text,
                Driver.FindElement(productPrice).Text.Replace("$",""),
                Driver.FindElement(productImage),
                Driver.FindElement(productAddToCartBtn)
            );
        }

        public ProductsPage ClickOnBackToProducts()
        {
            Driver.FindElement(backToProductsBtn).Click();
            return new ProductsPage( driver );
        }

        public ProductPage ClickOnAddToCart()
        {
            Driver.FindElement(productAddToCartBtn).Click();
            return this;
        }
    }
}