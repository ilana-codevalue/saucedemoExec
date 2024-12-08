using OpenQA.Selenium;
using System.Linq;
using Test.Infrastructure.Models;

namespace Test.Infrastructure.Pages
{
    public class CheckoutOverviewPage(IWebDriver driver) : BasePage(driver)
    {
        protected readonly By pageTitle = By.CssSelector("span.title");
        protected readonly By cartItem = By.CssSelector(".cart_item");
        protected readonly By itemName = By.CssSelector(".inventory_item_name");
        protected readonly By itemDesc = By.CssSelector(".inventory_item_desc");
        protected readonly By itemPrice = By.CssSelector(".inventory_item_price");
        protected readonly By subTotalLbl = By.CssSelector(".summary_subtotal_label");
        protected readonly By totalLbl = By.CssSelector(".summary_total_label");
        protected readonly By cancelBtn = By.CssSelector("#cancel");
        protected readonly By finishBtn = By.CssSelector("#finish");

        public override bool IsPageLoaded()
        {
            return Driver.FindElement(pageTitle).Text.Contains("Overview");
        }

        public List<Product> GetItemsDetalisList()
        {
            var itemDetailsList = new List<Product>();
            var items = GetItemsList();
            foreach (var item in items)
            {
                itemDetailsList.Add(GetItemDetails(item));
            }
            return itemDetailsList;
        }

        public List<IWebElement> GetItemsList()
        {
            return Driver.FindElements(cartItem).ToList();
        }
       
        public Product GetItemDetails(IWebElement productEl)
        {
            return new Product(
                productEl.FindElement(itemName).Text,
                productEl.FindElement(itemDesc).Text,
                productEl.FindElement(itemPrice).Text.Replace("$",""),
                null, null
            );
        }

        public double GetSummarySubTotalAmount()
        {
            var subTotalText = Driver.FindElement(subTotalLbl).Text;
            return double.Parse(subTotalText.Replace("Item total: $", ""));
        }

        public ProductsPage ClickOnCancel()
        {
            Driver.Click(cancelBtn);
            return new ProductsPage(driver);
        }

        public CheckouCompletePage ClickOnFinish()
        {
            Driver.Click(finishBtn);
            return new CheckouCompletePage(driver);
        }
    }
}