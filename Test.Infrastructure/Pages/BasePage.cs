using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Test.Infrastructure.Pages
{
    public abstract class BasePage
    {
        public readonly IWebDriver driver;
        //public readonly WebDriverWait wait;

        protected readonly LeftMenueModule leftMenue;

        public BasePage(IWebDriver _driver)
        {
            driver = _driver;
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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


        public bool WaitForDisplayed(By by, double timeout = 5)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(d => d.FindElement(by).Displayed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void SelectFromDropDown(By by, string option)
        {
            var dropDownEl = new SelectElement(driver.FindElement(by));
            dropDownEl.SelectByValue(option);
        }

    }
}
