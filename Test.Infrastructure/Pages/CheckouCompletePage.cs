using OpenQA.Selenium;

namespace Test.Infrastructure.Pages
{
    public class CheckouCompletePage : BasePage
    {

        protected readonly By completeHeaderTxt = By.CssSelector(".complete-header");
        protected readonly By completeTxt = By.CssSelector(".complete-text");
        protected readonly By backToProductsBtn = By.CssSelector("#back-to-products");

        public CheckouCompletePage(IWebDriver driver) : base(driver) { }

        public override bool IsPageLoaded()
        {
            return Driver.FindElement(completeHeaderTxt).Displayed;
        }


        public ProductsPage ClickOnBackHome()
        {
            Driver.Click(backToProductsBtn);
            return new ProductsPage(driver);
        }

        public string GetCompleteChckoutTitle()
        {
            return Driver.FindElement(completeHeaderTxt).Text;
        }

        public string GetCompleteChckoutDescription()
        {
            return Driver.FindElement(completeTxt).Text;
        }

        public ProductsPage ClickOnBackToHomeBtn()
        {
            Driver.Click(backToProductsBtn);
            return new ProductsPage(driver);
        }
    }
}