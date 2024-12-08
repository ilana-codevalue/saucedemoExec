using Test.Infrastructure.Pages;

namespace Test.Tests
{
    public class BaseTest
    {
        public required HomePage homepage;

        [SetUp]
        public void Setup()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            TestContext.WriteLine($"Starting test: {testName}");

            homepage = new HomePage();
        }

        [TearDown]
        public void Close()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            TestContext.WriteLine($"test: {testName} finished");

            homepage.Close();
        }

        [OneTimeTearDown]
        public void Quit()
        {
            TestContext.WriteLine($"test class finished");
            homepage.Quit();
        }
    }
}