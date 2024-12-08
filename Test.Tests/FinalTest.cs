using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Test.Infrastructure.Consts;
using Test.Infrastructure.Pages;
using Test.Infrastructure.Utils;

namespace Test.Tests
{
    public class FinalTest : BaseTest
    {

        [Test]
        public void SummaryTest()
        {
            const double PRICE = 16;

            var productsPage =
                homepage
                .NavigateToLoginPage()
                .Login(Users.STANDARD_USER, Users.PASSWORD);
            Helpers.Assert(() => Assert.That(productsPage.IsPageLoaded(), Is.True));

            // selecting all product price > PRICE
            var selectedProducts = Helpers.GetAllProductByPrice(PRICE, productsPage);

            // find the expensive one
            var sortedProducts = selectedProducts.OrderByDescending(p => double.Parse(p.price)).ToList();
            var mostExpensiveProductName = sortedProducts[0].name;
            Helpers.Assert(() => Assert.That(mostExpensiveProductName, Is.Not.Empty));

            // remove expensive product from cart
            var cartPage = productsPage.ClickOnCartIcon();
            var elToRemove = cartPage.GetItemsList()
                .Where(el => cartPage.GetItemDetails(el).name == mostExpensiveProductName).First();
            cartPage.RemoveItem(elToRemove);
            
            // continue shoping
            productsPage = cartPage.ClickOnContinueShoping();
            productsPage.SelectSortingProducts(SortingOptions.Price_Low_To_High);
            
            var allProductElements = productsPage.GetAllProductElements();
            
            // add the cheapest product
            productsPage.AddOrRemoveProductToCart(allProductElements[0]);
            
            var checkoutCompletePage = productsPage
                .ClickOnCartIcon()
                .ClickOnCheckout()
                .Checkout(TestData.FIRST_NAME, TestData.LAST_NAME, TestData.POSTAL_CODE)
                .ClickOnFinish();

            var loginPage = checkoutCompletePage.ClickOnLogoutLink();
            loginPage.FillLogin(Users.LOCKEDOUT_USER, Users.PASSWORD);
            loginPage.ClickLogin();

            Helpers.Assert(() =>
            Assert.Multiple(() =>
            {
                Assert.That(loginPage.IsLoginError(), Is.True);
                Assert.That(loginPage.GetLoginErrorMessage(), Does.Contain("locked out"));
            }));
        }
    }
}
