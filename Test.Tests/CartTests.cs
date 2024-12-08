using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Infrastructure.Consts;
using Test.Infrastructure.Pages;
using Test.Infrastructure.Utils;

namespace Test.Tests
{
    [TestFixture]
    public class CartTests : BaseTest
    {
        public required ProductsPage productsPage;

        [SetUp]
        public void ProductSetup()
        {
            productsPage = homepage.NavigateToLoginPage()
                .Login(Users.STANDARD_USER, Users.PASSWORD);
        }

        [Test]
        public void Sanitytest()
        {
            var cartPage = productsPage.CreatSampleCartListAndGoToCartPage();
            var itemElList = cartPage.GetItemsList();
            Helpers.Assert(() =>
            Assert.That(cartPage.VerifyItemsDetails(itemElList), Is.True));
        }

        [Test]
        public void SuccessRemoveItem()
        {
            var cartPage = productsPage.CreatSampleCartListAndGoToCartPage();
            var itemElList = cartPage.GetItemsList();

            var itemToRemove = itemElList[1]; // random item
            var itemDetails = cartPage.GetItemDetails(itemToRemove);

            cartPage.RemoveItem(itemToRemove);
            
            Helpers.Assert(() => Assert.That(cartPage.IsItemExists(itemDetails.name), Is.False));
        }

        [Test]
        public void SuccessBackContinueShoping()
        {
            var cartPage = productsPage.CreatSampleCartListAndGoToCartPage();
            var itemNameList = cartPage.GetItemsDetalisList().Select(x => x.name).ToList();
            
            productsPage = cartPage
                .ClickOnContinueShoping();

            // verify cart items are still selected
            foreach (var name in itemNameList)
            {
                var product = productsPage.GetProductElByName(name);
                if (product != null)
                {
                    var btnText = productsPage.GetProductAddToCartBtnText(product);
                    Helpers.Assert(() => Assert.That("Remove", Is.EqualTo(btnText)));
                }
                
            }
            Helpers.Assert(() => Assert.That(productsPage.IsPageLoaded(), Is.True));
        }

        [Test]
        public void SuccessGoToCheckout()
        {
            var cartPage = productsPage.CreatSampleCartListAndGoToCartPage();
            var checkoutPage = cartPage.ClickOnCheckout();
           
            Helpers.Assert(() => Assert.That(checkoutPage.IsPageLoaded(), Is.True));
        }

    }
}
