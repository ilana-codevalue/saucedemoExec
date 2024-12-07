using OpenQA.Selenium;

namespace Test.Infrastructure.Pages
{
    public class CartPage : BasePage
    {
        protected readonly By cartList = By.CssSelector(".cart_list");
        protected readonly By cartItem = By.CssSelector(".cart_item");
        protected readonly By itemName = By.CssSelector(".inventory_item_name");
        protected readonly By itemDesc = By.CssSelector(".inventory_item_desc");
        protected readonly By itemPrice = By.CssSelector(".inventory_item_price");
        protected readonly By removeItemBtn = By.CssSelector(".btn_small.cart_button");
        protected readonly By continueShopingBtn = By.CssSelector("#continue-shopping");
        protected readonly By checkoutBtn = By.CssSelector("#checkout");

        public CartPage(IWebDriver driver) : base(driver) { }

        public List<IWebElement> GetItemsList()
        {
            return Driver.FindElements(cartItem).ToList();
        }

        public CartPage RemoveItem(IWebElement item)
        {
            Driver.FindElement(item, removeItemBtn).Click();
            return this;
        }

        public ProductsPage ClickOnContinueShoping()
        {
            Driver.Click(continueShopingBtn);
            return new ProductsPage(driver);
        }

        public CheckoutPage ClickOnCheckout()
        {
            Driver.Click(checkoutBtn);
            return new CheckoutPage(driver);
        }
    }
}