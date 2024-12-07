using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Test.Infrastructure.Pages
{
    public class LeftMenueModule
    {
        
        public readonly By allItemsLink = By.CssSelector("#inventory_sidebar_link");
        public readonly By aboutLink = By.CssSelector("#about_sidebar_link");
        public readonly By logoutLink = By.CssSelector("#logout_sidebar_link");
        public readonly By resetAppStateLink = By.CssSelector("#reset_sidebar_link");
        
        public LeftMenueModule() { }

    }
}
