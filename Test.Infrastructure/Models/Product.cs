using OpenQA.Selenium;

namespace Test.Infrastructure.Models
{
    public class Product(string _name, string _description, string _price, IWebElement? _Image, IWebElement? _Button)
    {
        public string name = _name;
        public string description = _description;
        public string price = _price;
        public IWebElement? Image = _Image;
        public IWebElement? Button = _Button;
    }
}