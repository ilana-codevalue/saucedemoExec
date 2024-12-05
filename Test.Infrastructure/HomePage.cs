using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Test.Infrastructure
{
    public class HomePage
    {
        const string BASE_URL = "https://www.saucedemo.com";
        const string DRIVER_PATH = "C:\\saucedemo\\saucedemoExec\\Test.Infrastructure\\chromedriver.exe";

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
            return new LoginPage(driver);
        }

        // Kill web driver and close browser tab in test tear down
        public void Close() => driver.Close();

        // Kill web driver and close all browser sessions in test class tear down
        public void Quit() => driver.Quit();
    }
}
