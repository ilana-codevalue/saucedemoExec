using Test.Infrastructure.Pages;

namespace Test.Tests
{
    public class BaseTest
    {

        public required HomePage homepage;

        [SetUp]
        public void Setup() => homepage = new HomePage();
        

        [TearDown]
        public void Close() =>  homepage.Close();

        
        [OneTimeTearDown]
        public void Quit() => homepage.Quit();
    }
}