using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Test.Infrastructure.Pages
{
    public abstract class BasePage
    {
        public readonly IWebDriver driver;
        public readonly Driver Driver;
        protected readonly LeftMenueModule leftMenue;
        
        public readonly By leftModulemenueBtn = By.CssSelector("#react-burger-menu-btn");
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


        public bool IsCartBadgeNumberExists()
        {
            return Driver.WaitForDisplayed(shoppingCartBadge, 1);
        }


        public LoginPage ClickOnLogoutLink()
        {
            Driver.Click(leftModulemenueBtn);

            TestContext.WriteLine("Logging out..");
            Driver.FindElement(leftMenue.logoutLink).Click();
            return new LoginPage(driver);
        }

        public ProductsPage ClickOnAllItemsLink()
        {
            Driver.Click(leftModulemenueBtn);
            Driver.FindElement(leftMenue.allItemsLink).Click();
            return new ProductsPage(driver);
        }

        public void ClickOnAboutLink()
        {
            Driver.Click(leftModulemenueBtn);
            Driver.FindElement(leftMenue.aboutLink).Click();
        }

        public void ClickOnResetAppStateLink()
        {
            Driver.Click(leftModulemenueBtn);
            TestContext.WriteLine("Reseting App state..");
            Driver.FindElement(leftMenue.resetAppStateLink).Click();
        }
    }
}
