using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Infrastructure.Consts;
using Test.Infrastructure.Pages;

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
            Assert.That(productsPage.VerifyProductsDetails(products), Is.EqualTo(true));
        }

        [Test]
        public void VerifyProductSortingAtoZ()
        {
            var activeSorting = productsPage
                .SelectSortingProducts(SortingOptions.Name_AToZ);

            Assert.That(activeSorting, Does.Contain("A to Z"));

            var productNameList = productsPage.GetProductNameList();
            var sortedList = productsPage.GetProductNameList();
            sortedList.Sort();

            Assert.That(productNameList.SequenceEqual(sortedList));
        }

        [Test]
        public void VerifyProductSortingZToA()
        {
            var activeSorting = productsPage
                .SelectSortingProducts(SortingOptions.Name_ZToA);

            Assert.That(activeSorting, Does.Contain("Z to A"));

            var productNameList = productsPage.GetProductNameList();
            var sortedList = productsPage.GetProductNameList();
            sortedList.Sort((x, y) => y.CompareTo(x)); // sort in desending order

            Assert.That(productNameList.SequenceEqual(sortedList));
        }

        [Test]
        public void VerifyProductSortingPriceLowToHigh()
        {
            var activeSorting = productsPage
                .SelectSortingProducts(SortingOptions.Price_Low_To_High);

            Assert.That(activeSorting, Does.Contain("low to high"));

            var productPriceList = productsPage.GetProductPriceList();
            var sortedList = productsPage.GetProductPriceList();
            sortedList.Sort();

            Assert.That(productPriceList.SequenceEqual(sortedList));
        }

        [Test]
        public void VerifyProductSortingPriceHighToLow()
        {
            var activeSorting = productsPage
                .SelectSortingProducts(SortingOptions.Price_High_To_Low);

            Assert.That(activeSorting, Does.Contain("high to low"));

            var productPriceList = productsPage.GetProductPriceList();
            var sortedList = productsPage.GetProductPriceList();
            sortedList.Sort((x, y) => y.CompareTo(x)); // sort in desending order

            Assert.That(productPriceList.SequenceEqual(sortedList));
        }


        [Test]
        public void SuccessToAddItemToCart()
        {
            //var activeSorting = productsPage
            //    .SelectSortingProducts(SortingOptions.Price_High_To_Low);

            //Assert.That(activeSorting, Does.Contain("high to low"));

            //var productPriceList = productsPage.GetProductPriceList();
            //var sortedList = productsPage.GetProductPriceList();
            //sortedList.Sort((x, y) => y.CompareTo(x)); // sort in desending order

            //Assert.That(productPriceList.SequenceEqual(sortedList));
        }




    }
}
