using Test.Infrastructure.Consts;
using Test.Infrastructure.Utils;

namespace Test.Tests;

[TestFixture]
public class LoginTests : BaseTest
{

    [Test]
    public void SuccessLoginWithStandardUser()
    {
        var productPage = homepage
            .NavigateToLoginPage()
            .Login(Users.STANDARD_USER, Users.PASSWORD);

        Helpers.Assert(() => Assert.That(productPage.IsPageLoaded(), Is.True));
    }

    [Test]
    [TestCase(Users.WRONG_USER, Users.PASSWORD, "do not match")]
    [TestCase(Users.STANDARD_USER, Users.WRONG_PASSWORD, "do not match")]
    [TestCase(Users.LOCKEDOUT_USER, Users.PASSWORD, "locked out")]
    public void FailLoginWithWrongUser(string username, string password, string error)
    {
        var loginPage = homepage
             .NavigateToLoginPage();
        loginPage
            .FillLogin(username, password)
            .ClickLogin();

        Helpers.Assert(() =>
        Assert.Multiple(() =>
        {
            Assert.That(loginPage.IsLoginError(), Is.True);
            Assert.That(loginPage.GetLoginErrorMessage().Contains(error), Is.True);
        }));

    }

    [Test] 
    // I wasn't sure if i needed to wait for long login or to limit this
    // user case to timeout and verify test fails
    public void SuccessLoginWithPerformance_glitch_user()
    {
        var productPage = homepage
            .NavigateToLoginPage()
            .Login(Users.PERFORMANCE_GLITCH_USER, Users.PASSWORD);

        Helpers.Assert(() =>
        Assert.That(productPage.IsPageLoaded(), Is.True));
    }
}
