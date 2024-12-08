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
    public class ProductsTests : BaseTest
    {
        public required ProductsPage productsPage;

        [SetUp]
        public void ProductSetup()
        {
            productsPage = homepage.NavigateToLoginPage()
                .Login(Users.STANDARD_USER, Users.PASSWORD);
        }
        
        
        [Test]
        public void VerifyProductDetailsDisplayed()
        {
            var products = productsPage.GetAllProductElements();
            Helpers.Assert(() => Assert.That(Helpers.VerifyProductsDetails(products, productsPage), Is.EqualTo(true)));
        }

        [Test]
        public void VerifyProductSortingAtoZ()
        {
            var activeSorting = productsPage
                .SelectSortingProducts(SortingOptions.Name_AToZ);

            Helpers.Assert(() => Assert.That(activeSorting, Does.Contain("A to Z")));

            var productNameList = productsPage.GetProductNameList();
            var sortedList = productsPage.GetProductNameList();
            sortedList.Sort();

            Helpers.Assert(() => Assert.That(productNameList.SequenceEqual(sortedList)));
        }

        [Test]
        public void VerifyProductSortingZToA()
        {
            var activeSorting = productsPage
                .SelectSortingProducts(SortingOptions.Name_ZToA);

            Helpers.Assert(() => Assert.That(activeSorting, Does.Contain("Z to A")));

            var productNameList = productsPage.GetProductNameList();
            var sortedList = productsPage.GetProductNameList();
            sortedList.Sort((x, y) => y.CompareTo(x)); // sort in desending order

            Helpers.Assert(() => Assert.That(productNameList.SequenceEqual(sortedList)));
        }

        [Test]
        public void VerifyProductSortingPriceLowToHigh()
        {
            var activeSorting = productsPage
                .SelectSortingProducts(SortingOptions.Price_Low_To_High);

            Helpers.Assert(() => Assert.That(activeSorting, Does.Contain("low to high")));

            var productPriceList = productsPage.GetProductPriceList();
            var sortedList = productsPage.GetProductPriceList();
            sortedList.Sort();

            Helpers.Assert(() => Assert.That(productPriceList.SequenceEqual(sortedList)));
        }

        [Test]
        public void VerifyProductSortingPriceHighToLow()
        {
            var activeSorting = productsPage
                .SelectSortingProducts(SortingOptions.Price_High_To_Low);

            Helpers.Assert(() => Assert.That(activeSorting, Does.Contain("high to low")));

            var productPriceList = productsPage.GetProductPriceList();
            var sortedList = productsPage.GetProductPriceList();
            sortedList.Sort((x, y) => y.CompareTo(x)); // sort in desending order

            Helpers.Assert(() => Assert.That(productPriceList.SequenceEqual(sortedList)));
        }


        [Test]
        public void SuccessClickAddItemToCartBtn()
        {
            var products = productsPage.GetAllProductElements();
            var productToAdd = products[0];

            productsPage
                .AddOrRemoveProductToCart(productToAdd);
           
            var btnText = productsPage.GetProductAddToCartBtnText(productToAdd);
            Helpers.Assert(() => Assert.That(btnText, Is.EqualTo("Remove")));
        }

        [Test]
        public void SuccessClickRemoveBtn()
        {
            // pre conditions
            var products = productsPage.GetAllProductElements();
            var productToAdd = products[1];

            productsPage.AddOrRemoveProductToCart(productToAdd);

            var btnText = productsPage.GetProductAddToCartBtnText(productToAdd);
            Helpers.Assert(() => Assert.That(btnText, Is.EqualTo("Remove")));

            // Act
            productsPage
                .AddOrRemoveProductToCart(productToAdd, true);

            btnText = productsPage.GetProductAddToCartBtnText(productToAdd);
            Helpers.Assert(() => Assert.That(btnText, Is.EqualTo("Add to cart")));
        }

        [Test]
        public void SuccessAddItemToCart()
        {
            var products = productsPage.GetAllProductElements();
            var productToAdd = products[0];
            var productDetails = productsPage.GetProductDetails(productToAdd);

            var cartPage = productsPage
                .AddOrRemoveProductToCart(productToAdd)
                .ClickOnCartIcon();

            var IsProducrInCart = cartPage.IsItemExists(productDetails.name);
            Helpers.Assert(() => Assert.That(IsProducrInCart, Is.True));
        }
        [Test]
        public void SuccessRemoveItemFromCart()
        {
            
            var products = productsPage.GetAllProductElements();
            var productToAdd = products[0];
            var productDetails = productsPage.GetProductDetails(productToAdd);

            // pre conditions
            var cartPage = productsPage
                .AddOrRemoveProductToCart(productToAdd)
                .ClickOnCartIcon();

            var IsProducrInCart = cartPage.IsItemExists(productDetails.name);
            Helpers.Assert(() => Assert.That(IsProducrInCart, Is.True));


            // Act
            productsPage = cartPage
                .ClickOnContinueShoping();
            
            productToAdd = productsPage.GetAllProductElements()[0];
            
            cartPage =
            productsPage
                .AddOrRemoveProductToCart(productToAdd, true)
                .ClickOnCartIcon();

            IsProducrInCart = cartPage.IsItemExists(productDetails.name);
            Helpers.Assert(() => Assert.That(IsProducrInCart, Is.False) );
        }
    }
}
