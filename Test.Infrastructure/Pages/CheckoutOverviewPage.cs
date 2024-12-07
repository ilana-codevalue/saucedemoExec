using OpenQA.Selenium;
using System.Linq;

namespace Test.Infrastructure.Pages
{
    public class CheckoutOverviewPage : BasePage
    {

        protected readonly By cartList = By.CssSelector(".cart_list");
        protected readonly By cartItem = By.CssSelector(".cart_item");
        protected readonly By itemName = By.CssSelector(".inventory_item_name");
        protected readonly By itemDesc = By.CssSelector(".inventory_item_desc");
        protected readonly By itemPrice = By.CssSelector(".inventory_item_price");
        protected readonly By subTotalLbl = By.CssSelector(".summary_subtotal_label");
        protected readonly By totalLbl = By.CssSelector(".summary_total_label");


        protected readonly By cancelBtn = By.CssSelector("#cancel");
        protected readonly By finishBtn = By.CssSelector("#finish");

        public CheckoutOverviewPage(IWebDriver driver) : base(driver) { }


        public List<IWebElement> GetItemsList()
        {
            return Driver.FindElements(cartItem).ToList();
        }

        public double GetSummarySubTotalAmount()
        {
            var subTotalText = Driver.FindElement(subTotalLbl).Text;
            return double.Parse(subTotalText);
        }

        public double GetSummaryTotalAmount()
        {
            var totalText = Driver.FindElement(totalLbl).Text;
            return double.Parse(totalText);
        }

        public double CalculateSummaryTotalAmount()
        {
            double summaryTotal = 0;
            var items = GetItemsList();
            foreach (var price in from item in items
                                  let price = Driver.FindElement(item, itemPrice).Text
                                  select price)
            {
                summaryTotal += double.Parse(price);
            }

            return summaryTotal;
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