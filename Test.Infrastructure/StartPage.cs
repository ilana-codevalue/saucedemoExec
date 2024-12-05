using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Test.Infrastructure
{
    internal class StartPage
    {
        protected readonly IWebDriver driver;
        internal StartPage() 
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl("https://www.clalbit.co.il/");

            AssertInPage();
        }
    }
}
