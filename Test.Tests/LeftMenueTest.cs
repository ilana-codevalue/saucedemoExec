using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Infrastructure.Consts;
using Test.Infrastructure.Pages;

namespace Test.Tests
{
    public  class LeftMenueTest : BaseTest
    {
        public required ProductsPage productsPage;

        [SetUp]
        public void ProductSetup()
        {
            productsPage = homepage.NavigateToLoginPage()
                .Login(Users.STANDARD_USER, Users.PASSWORD);
        }

        [Test]
        public void VerifyAllItemsLinkNavigateToProductsPage()
        {
            var cartPage = productsPage
                .CreatSampleCartListAndGoToCartPage();

            
        }
    }
}
