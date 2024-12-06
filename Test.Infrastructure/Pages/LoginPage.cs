using OpenQA.Selenium;

namespace Test.Infrastructure.Pages
{
    public class LoginPage : BasePage
    {
        protected readonly By loginBtn = By.CssSelector("#login-button");
        protected readonly By usernameInput = By.CssSelector("#user-name");
        protected readonly By passwordInput = By.CssSelector("#password");
        protected readonly By errorMessage = By.CssSelector(".error-message-container.error > h3");

        public LoginPage(IWebDriver _driver) : base(_driver)
        {
        }

        public override bool IsPageLoaded()
        {
            //return wait.Until(d => d.FindElement(loginBtn).Displayed);
            return WaitForDisplayed(loginBtn);
        }

        public ProductsPage Login(string username, string passwors)
        {
            driver.FindElement(usernameInput).Clear();
            driver.FindElement(usernameInput).SendKeys(username);

            driver.FindElement(passwordInput).Clear();
            driver.FindElement(passwordInput).SendKeys(passwors);

            driver.FindElement(loginBtn).Click();
            return new ProductsPage(driver);
        }

        public string GetLoginErrorMessage()
        {
            return driver.FindElement(errorMessage).Text;
        }

        public bool IsLoginError()
        {
            return driver.FindElement(errorMessage).Displayed;
        }
    }
}