using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Test.Infrastructure
{
    internal abstract class BasePage
    {
        public readonly IWebDriver driver;
        public readonly WebDriverWait wait;

        public BasePage(IWebDriver _driver)
        {
            driver = _driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

    }
}
