using NUnit.Framework;
using OpenQA.Selenium;

namespace Test.Infrastructure.Pages
{
    public class LoginPage(IWebDriver _driver) : BasePage(_driver)
    {
        protected readonly By loginBtn = By.CssSelector("#login-button");
        protected readonly By usernameInput = By.CssSelector("#user-name");
        protected readonly By passwordInput = By.CssSelector("#password");
        protected readonly By errorMessage = By.CssSelector(".error-message-container.error > h3");

        public override bool IsPageLoaded()
        {
            return Driver.WaitForDisplayed(loginBtn);
        }

        public ProductsPage Login(string username, string password)
        {
            Driver.TypeTo(usernameInput, username);
            Driver.TypeTo(passwordInput, password);

            TestContext.WriteLine($"Login with username: {username}");
            Driver.FindElement(loginBtn).Click();
            return new ProductsPage(driver);
        }

        public LoginPage FillLogin(string username, string password)
        {
            Driver.TypeTo(usernameInput, username);
            Driver.TypeTo(passwordInput, password);
            
            TestContext.WriteLine($"Login with username: {username}");
            return this;
        }

        public void ClickLogin()
        {
            Driver.FindElement(loginBtn).Click();
        }

        public string GetLoginErrorMessage()
        {
            return Driver.FindElement(errorMessage).Text;
        }

        public bool IsLoginError()
        {
            return Driver.FindElement(errorMessage).Displayed;
        }
    }
}