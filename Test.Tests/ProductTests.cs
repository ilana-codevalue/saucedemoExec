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
    public class ProductTests : BaseTest
    {
        public required ProductsPage productsPage;

        [SetUp]
        public void ProductSetup()
        {
            productsPage = homepage.NavigateToLoginPage()
                .Login(Users.STANDARD_USER, Users.PASSWORD);
        }

        [Test]
        public void PageSanity()
        {
            var productList = productsPage.GetAllProductElements();

            for(int index = 0; index < productList.Count; index++)
            {
                productList = productsPage.GetAllProductElements();
                    var productPage = productsPage.ClickOnProduct(productList[index]);
                var productDetails = productPage.GetProductDetails();
               
                Helpers.Assert(() =>
                Assert.Multiple(() =>
                {
                    Assert.That(productDetails.name, Is.Not.Empty);
                    Assert.That(productDetails.description, Is.Not.Empty);
                    Assert.That(productDetails.price, Is.Not.Empty);
                    Assert.That(productDetails.Button, Is.Not.Null);
                    Assert.That(productDetails.Image, Is.Not.Null);
                }));

                productPage.ClickOnBackToProducts();
            }
               
        }

        [Test]
        public void SuccessAddToCart()
        {
            var productPage = productsPage.SelectRandomProductAndGoToProductPage();
            var productDetails = productPage.GetProductDetails();

            var cartPage =
                productPage
                .ClickOnAddToCart()
                .ClickOnCartIcon();

            var IsProducrInCart = cartPage.IsItemExists(productDetails.name);
            Helpers.Assert(() => Assert.That(IsProducrInCart, Is.True));
        }

        [Test]
        public void SuccessRemoveFromCart()
        {
            var productPage = productsPage.SelectRandomProductAndGoToProductPage();
            var productDetails = productPage.GetProductDetails();

            var cartPage = productPage
                .ClickOnAddToCart()
                .ClickOnCartIcon();

            var IsProducrInCart = cartPage.IsItemExists(productDetails.name);
            Helpers.Assert(() => Assert.That(IsProducrInCart, Is.True));

            productsPage = cartPage
              .ClickOnContinueShoping();

            productPage = productsPage.SelectProductByNameAndGoToProductPage(productDetails.name);
            cartPage = productPage
                .ClickOnAddToCart()
                .ClickOnCartIcon();

            IsProducrInCart = cartPage.IsItemExists(productDetails.name);
            Helpers.Assert(() => Assert.That(IsProducrInCart, Is.False));
        }
    }
}
