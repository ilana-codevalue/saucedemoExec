using OpenQA.Selenium;

namespace Test.Infrastructure.Pages
{
    public class CheckouCompletePage : BasePage
    {

        protected readonly By completeHeaderTxt = By.CssSelector(".complete-header");
        protected readonly By completeTxt = By.CssSelector(".complete-text");
        protected readonly By backToProductsBtn = By.CssSelector("#back-to-products");

        public CheckouCompletePage(IWebDriver driver) : base(driver) { }


        public ProductsPage ClickOnBackHome()
        {
            Driver.Click(backToProductsBtn);
            return new ProductsPage(driver);
        }
    }
}