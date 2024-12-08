using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Test.Infrastructure.Consts;

namespace Test.Infrastructure.Pages
{
    public class HomePage
    {
        const string BASE_URL = TestData.BASE_URL;
        const string DRIVER_PATH = TestData.DRIVER_PATH;

        protected readonly IWebDriver driver;

        public HomePage()
        {
            driver = new ChromeDriver(DRIVER_PATH);
            driver.Manage().Window.Maximize();

            NavigateToLoginPage();
        }

        public LoginPage NavigateToLoginPage()
        {
            driver.Navigate().GoToUrl(BASE_URL);
            
            TestContext.WriteLine($"Navigating to: {BASE_URL}");
            return new LoginPage(driver);
        }

        // Kill web driver and close browser tab in test tear down
        public void Close()
        {
            TestContext.WriteLine("Kill web driver and closing browser tab");
            driver.Close();
        }

        // Kill web driver and close all browser sessions in test class tear down
        public void Quit()
        {
            Console.WriteLine("Kill web driver and closing all browser session");
            driver.Quit();
        }

    }
}
