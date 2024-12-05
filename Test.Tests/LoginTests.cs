namespace Test.Tests;

public class LoginTests : BaseTest
{

    [Test]
    public void Test()
    {
        homepage.NavigateToLoginPage()
            .Login("", "");
    }
}
