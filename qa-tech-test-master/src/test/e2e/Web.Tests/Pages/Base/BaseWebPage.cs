using OpenQA.Selenium;
using Web.Test.Selenium;

namespace Web.Integration.Test.Pages.Base
{
    public class BaseWebPage : BasePage
    { 
        private readonly IWebDriver _driver;

        public BaseWebPage(IWebDriver webDriver, string controlId) : base(webDriver, controlId)
        {
            _driver = webDriver;
        }

        public BaseWebPage(IWebDriver webDriver, By controlId) : base(webDriver, controlId)
        {
            _driver = webDriver;
        }

        public BaseWebPage(IWebDriver webDriver) : base(webDriver)
        {
            _driver = webDriver;
        }
    }
}