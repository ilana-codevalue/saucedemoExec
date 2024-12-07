using OpenQA.Selenium;

namespace Test.Infrastructure.Pages
{
    public class CheckoutPage : BasePage
    {

        protected readonly By firstNameInput = By.CssSelector("#first-name");
        protected readonly By lastNameInput = By.CssSelector("#last-name");
        protected readonly By postalCodeInput = By.CssSelector("#postal-code");
        protected readonly By cancelBtn = By.CssSelector("#cancel");
        protected readonly By continueBtn = By.CssSelector("#continue");
        protected readonly By errorMessageLbl = By.CssSelector(".error-message-container.error");


        public CheckoutPage(IWebDriver driver) : base(driver) { }

        public override bool IsPageLoaded()
        {
            return Driver.WaitForDisplayed(firstNameInput);
        }

        public CheckoutOverviewPage Checkout(string firstName, string lastName, string postalCode)
        {
            Driver.TypeTo(firstNameInput, firstName);
            Driver.TypeTo(lastNameInput, lastName);
            Driver.TypeTo(postalCodeInput, postalCode);
            Driver.Click(continueBtn);

            return new CheckoutOverviewPage(driver);
        }

        public CartPage ClickOnCancel()
        {
            Driver.Click(cancelBtn);
            return new CartPage(driver);
        }

        public CheckoutOverviewPage ClickOnContinue()
        {
            Driver.Click(continueBtn);
            return new CheckoutOverviewPage(driver);
        }
    }
}