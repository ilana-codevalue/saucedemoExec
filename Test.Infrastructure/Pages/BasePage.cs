using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Test.Infrastructure.Pages
{
    public abstract class BasePage
    {
        public readonly IWebDriver driver;
        public readonly Driver Driver;
        protected readonly LeftMenueModule leftMenue;
        
        protected readonly By shoppingCartLink = By.CssSelector(".shopping_cart_link");
        protected readonly By shoppingCartBadge = By.CssSelector(".shopping_cart_badge");

        public BasePage(IWebDriver _driver)
        {
            driver = _driver;
            Driver = new Driver(_driver);
            leftMenue = new LeftMenueModule();

            IsPageLoaded();
        }

        public virtual bool IsPageLoaded()
        {
            /**
             * Assert each page loading automaticcaly while
             * inherited page override with its own implementation 
             */

            return false;
        }

        public CartPage ClickOnCartIcon()
        {
            Driver.FindElement(shoppingCartLink).Click();
            return new CartPage(driver);
        }

        public int GetCartBadgeNumber() => Int32.Parse(driver.FindElement(shoppingCartBadge).Text);
    }
}
