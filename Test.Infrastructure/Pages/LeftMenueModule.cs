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
        public readonly By AllItems = By.CssSelector("#inventory_sidebar_link");
        public LeftMenueModule() { }

    }
}
