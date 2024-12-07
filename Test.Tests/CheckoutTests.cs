using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Infrastructure;
using Test.Infrastructure.Consts;
using Test.Infrastructure.Pages;

namespace Test.Tests
{
    internal class CheckoutTests : BaseTest
    {
        public required ProductsPage productsPage;

        [SetUp]
        public void ProductSetup()
        {
            productsPage = homepage.NavigateToLoginPage()
                .Login(Users.STANDARD_USER, Users.PASSWORD);
        }


        // Checkout Page - Tests

        [Test]
        public void SuccessCheckout()
        {
            var cartPage = productsPage.CreatSampleCartListAndGoToCartPage();

            var checkoutOverviewPage = cartPage
                .ClickOnCheckout()
                .Checkout(TestData.FIRST_NAME, TestData.LAST_NAME, TestData.POSTAL_CODE);

            Assert.That(checkoutOverviewPage.IsPageLoaded(), Is.True);
        }

        [Test]
        public void FailCheckoutWhenMissingDetail()
        {
            var cartPage = productsPage.CreatSampleCartListAndGoToCartPage();

            var checkoutOverviewPage = cartPage
                .ClickOnCheckout()
                .Checkout(TestData.FIRST_NAME, string.Empty, TestData.POSTAL_CODE);

            Assert.That(checkoutOverviewPage.IsPageLoaded(), Is.False);
        }

        [Test]
        public void SuccessBackToCartPage()
        {
            var cartPage = productsPage.CreatSampleCartListAndGoToCartPage();
            var itemsDetailsList = cartPage.GetItemsDetalisList().Select(x => x.name).ToList();

            var checkoutPage = cartPage
                .ClickOnCheckout();

            cartPage = checkoutPage
                .ClickOnCancel();

            var returnedItemDetailList = cartPage
                .GetItemsDetalisList().Select( x => x.name).ToList();

            Assert.That(returnedItemDetailList, Is.EqualTo(itemsDetailsList));
        }


        
        
        // Checkout Overview Page - Tests
        
        [Test]
        public void CheckoutOverviewPageSanity()
        {
            var cartPage = productsPage.CreatSampleCartListAndGoToCartPage();
            var itemsDetailsList = cartPage.GetItemsDetalisList();

            var checkoutOverviewPage = cartPage
                .ClickOnCheckout()
                 .Checkout(TestData.FIRST_NAME, TestData.LAST_NAME, TestData.POSTAL_CODE);

            var checkoutOverviewlList = checkoutOverviewPage
                .GetItemsDetalisList();

            Assert.Multiple(() =>
            {
                Assert.That(checkoutOverviewlList.Select(x => x.name), Is.EqualTo(itemsDetailsList.Select(x => x.name)));
                Assert.That(checkoutOverviewlList.Select(x => x.description), Is.EqualTo(itemsDetailsList.Select(x => x.description)));
                Assert.That(checkoutOverviewlList.Select(x => x.price), Is.EqualTo(itemsDetailsList.Select(x => x.price)));
            });
        }

        [Test]
        public void SuccessCheckoutOverviewFinish()
        {
            var cartPage = productsPage.CreatSampleCartListAndGoToCartPage();

            var checkoutCompletePage = cartPage
                .ClickOnCheckout()
                 .Checkout(TestData.FIRST_NAME, TestData.LAST_NAME, TestData.POSTAL_CODE)
                 .ClickOnFinish();

            
           Assert.That(checkoutCompletePage.IsPageLoaded(), Is.True);
        }

        [Test]
        public void VerifySubTotalPaymentCorrect()
        {
            var cartPage = productsPage.CreatSampleCartListAndGoToCartPage();
            var itemsDetailsList = cartPage.GetItemsDetalisList();

            var checkoutOverviewPage = cartPage
                .ClickOnCheckout()
                 .Checkout(TestData.FIRST_NAME, TestData.LAST_NAME, TestData.POSTAL_CODE);

            var checkoutOverviewlList = checkoutOverviewPage
                .GetItemsDetalisList();

            var displayedSubTotal = checkoutOverviewPage.GetSummarySubTotalAmount();

            double calculatedSummarySubTotal = 0;
            foreach (var item in itemsDetailsList.Select(x => x.price))
            {
                calculatedSummarySubTotal += double.Parse(item);
            }

            Assert.That(displayedSubTotal, Is.EqualTo(calculatedSummarySubTotal));
        }

        [Test]
        public void SucceesBackToProductsPage()
        {
            var cartPage = productsPage.CreatSampleCartListAndGoToCartPage();

            var checkoutOverviewPage = cartPage
                .ClickOnCheckout()
                 .Checkout(TestData.FIRST_NAME, TestData.LAST_NAME, TestData.POSTAL_CODE);

            var checkoutOverviewlList = checkoutOverviewPage
                .GetItemsDetalisList();

            productsPage = checkoutOverviewPage.ClickOnCancel();

           // verify checkout items are still selected
            foreach (var name in checkoutOverviewlList.Select(x => x.name))
            {
                var product = productsPage.GetProductElByName(name);
                if (product != null)
                {
                    var btnText = productsPage.GetProductAddToCartBtnText(product);
                    Assert.That("Remove", Is.EqualTo(btnText));
                }
            }
        }



        // Checkout Complete Tests

        [Test]
        public void VerifyCheckoutCompletePage()
        {
            var checkoutCompletePage = DoSuccessCheckoutAndFinish();
            Assert.IsNotEmpty(checkoutCompletePage.GetCompleteChckoutTitle());
            Assert.IsNotEmpty(checkoutCompletePage.GetCompleteChckoutDescription());
        }

        [Test]
        public void SuccessGoToProducts()
        {
            var checkoutCompletePage = DoSuccessCheckoutAndFinish();
            var productPage = checkoutCompletePage.ClickOnBackToHomeBtn();
            var prouctsNameList = productPage.GetProductNameList();

            // verify all items are not selected
            foreach (var name in prouctsNameList)
            {
                var product = productsPage.GetProductElByName(name);
                if (product != null)
                {
                    var btnText = productsPage.GetProductAddToCartBtnText(product);
                    Assert.That("Add to cart", Is.EqualTo(btnText));
                }
            }
        }


        public CheckouCompletePage DoSuccessCheckoutAndFinish()
        {
            var cartPage = productsPage.CreatSampleCartListAndGoToCartPage();

            return cartPage
                .ClickOnCheckout()
                 .Checkout(TestData.FIRST_NAME, TestData.LAST_NAME, TestData.POSTAL_CODE)
                 .ClickOnFinish();
        }
    }
}
