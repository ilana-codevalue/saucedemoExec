using Test.Infrastructure.Consts;

namespace Test.Tests;

public class LoginTests : BaseTest
{

    [Test]
    public void SuccessLoginWithStandardUser()
    {
        var productPage = homepage
            .NavigateToLoginPage()
            .Login(Users.STANDARD_USER, Users.PASSWORD);

        Assert.That(productPage.IsPageLoaded(), Is.True);
    }

    [Test]
    public void FailLoginWithWrongUser()
    {
        var productPage = homepage
            .NavigateToLoginPage()
            .Login(Users.WRONG_USER, Users.PASSWORD);

        Assert.That(productPage.IsPageLoaded(), Is.False);
    }

    [Test]
    public void FailLoginWithWrongPassword()
    {
        var productPage = homepage
            .NavigateToLoginPage()
            .Login(Users.STANDARD_USER, Users.WRONG_PASSWORD);

        Assert.That(productPage.IsPageLoaded(), Is.False);
    }

    [Test]
    public void FailLoginWithLockedoutUser()
    {
        var loginPage = homepage
            .NavigateToLoginPage();

        var productPage = loginPage
            .Login(Users.LOCKEDOUT_USER, Users.PASSWORD);

        Assert.Multiple(() =>
        {
            Assert.That(loginPage.GetLoginErrorMessage(), Does.Contain("user has been locked out"));
            Assert.That(productPage.IsPageLoaded(), Is.False);
        });
    }

    [Test]
    public void SuccessLoginWithPerformance_glitch_user()
    {
        //var productPage = homepage
        //    .NavigateToLoginPage()
        //    .Login(Users.PERFORMANCE_GLITCH_USER, Users.PASSWORD);

        //Assert.That(productPage.IsPageLoaded(), Is.True);
    }
}
