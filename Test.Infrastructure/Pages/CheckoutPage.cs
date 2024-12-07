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

        public CheckoutOverviewPage checkout(string firstName, string lastName, string postalCode)
        {
            Driver.TypeTo(firstNameInput, firstName);
            Driver.TypeTo(lastNameInput, lastName);
            Driver.TypeTo(postalCodeInput, postalCode);
            Driver.Click(continueBtn);

            return new CheckoutOverviewPage(driver);
        }
    }
}