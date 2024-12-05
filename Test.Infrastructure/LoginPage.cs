using OpenQA.Selenium;

namespace Test.Infrastructure
{
    public class LoginPage : BasePage
    {
        protected readonly By loginBtn = By.CssSelector("#login-button");
        protected readonly By usernameInput = By.CssSelector("#user-name");
        protected readonly By passwordInput = By.CssSelector("#password");

        public LoginPage(IWebDriver _driver) : base(_driver)
        {
        }

        protected override void WaitForPageLoad()
        {
            wait.Until(d => d.FindElement(loginBtn).Displayed);
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
    }
}