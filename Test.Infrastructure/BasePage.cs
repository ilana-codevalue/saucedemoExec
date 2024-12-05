using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Test.Infrastructure
{
    public abstract class BasePage
    {
        public readonly IWebDriver driver;
        public readonly WebDriverWait wait;

        public BasePage(IWebDriver _driver)
        {
            driver = _driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            WaitForPageLoad();
        }

        protected virtual void WaitForPageLoad()
        {
            /**
             * Assert each page loading automaticcaly while
             * inherited page override with its own implementation 
             */
        }

    }
}
