using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Test.Infrastructure
{
    public class Driver
    {

        private readonly IWebDriver driver;
        public Driver(IWebDriver _driver) 
        {
            driver = _driver;
        }

        public IWebElement FindElement(By by, double timeoutInSeconds = 0, Predicate<IWebElement> predicate = null)
        {
            IReadOnlyCollection<IWebElement> elements = FindElements(by, timeoutInSeconds, predicate);
            return elements.Count != 0 ? elements.First() : null;
        }

        public IWebElement FindElement(IWebElement root, By by, double timeoutInSeconds = 0, Predicate<IWebElement> predicate = null)
        {
            IReadOnlyCollection<IWebElement> elements = FindElements(root, by, timeoutInSeconds, predicate);
            return elements.Count != 0 ? elements.First() : null;
        }

        public IReadOnlyCollection<IWebElement> FindElements(By by, double timeoutInSeconds = 0, Predicate<IWebElement> predicate = null)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IReadOnlyCollection<IWebElement> elements = driver.FindElements(by);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            if (predicate != null)
            {
                elements = elements.ToList().FindAll(predicate);
            }
            return elements;
        }

        public IReadOnlyCollection<IWebElement> FindElements(IWebElement root, By by, double timeoutInSeconds = 0, Predicate<IWebElement> predicate = null)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
            IReadOnlyCollection<IWebElement> elements = root.FindElements(by);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            if (predicate != null)
            {
                elements = elements.ToList().FindAll(predicate);
            }
            return elements;
        }

        public Driver TypeTo(By by, string keys)
        {
            var el = FindElement(by);
            if (el != null)
            {
                el.Clear();
                el.SendKeys(keys);
            }
            return this;
        }

        public Driver Click(By by)
        {
            FindElement(by).Click();
            return this;
        }


        public bool WaitForDisplayed(By by, double timeout = 10)
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

        public Driver SelectFromDropDown(By by, string option)
        {
            var dropDownEl = new SelectElement(driver.FindElement(by));
            dropDownEl.SelectByValue(option);
            return this;
        }

    }
}
