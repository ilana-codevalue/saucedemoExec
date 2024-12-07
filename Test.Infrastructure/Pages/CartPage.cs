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

        
        
        public List<Product> GetItemsDetalisList()
        {
            var itemDetailsList = new List<Product>();
            var items = GetItemsList();
            foreach (var item in items)
            {
                itemDetailsList.Add(GetItemDetails(item));
            }
            return itemDetailsList;
        }
        public List<IWebElement> GetItemsList()
        {
            return Driver.FindElements(cartItem).ToList();
        }

        public Product GetItemDetails(IWebElement productEl)
        {
            return new Product(
                productEl.FindElement(itemName).Text,
                productEl.FindElement(itemDesc).Text,
                productEl.FindElement(itemPrice).Text.Replace("$",""),
                null,
                productEl.FindElement(removeItemBtn)
            );
        }

        public bool VerifyItemsDetails(List<IWebElement> itemsElements)
        {
            foreach (var item in itemsElements)
            {
                if (!VerifyItemDetails(item))
                    return false;
            }

            return true;
        }

        public bool VerifyItemDetails(IWebElement productEl)
        {
            var productDetails = GetItemDetails(productEl);
            return productDetails.name != string.Empty &&
                productDetails.description != string.Empty &&
                productDetails.price != string.Empty &&
                productDetails.Button != null;
        }


        public CartPage RemoveItem(IWebElement item)
        {
            Driver.FindElement(item, removeItemBtn).Click();
            return this;
        }

        public bool IsItemExists(string name)
        {
            return GetItemsList()
                .Select(item => Driver.FindElement(item, itemName).Text == name)
                .FirstOrDefault();
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