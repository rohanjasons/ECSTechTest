using OpenQA.Selenium;

namespace Web.Integration.Test.Pages.Base
{
    /// <summary>
    /// Fluent API approach to exposing page objects. Using this approach to reduce verbosity in test classes
    /// </summary>
    internal static class PageExtensions
    {
        internal static LandingPage LandingPage(this IWebDriver webDriver)
        {
            return new LandingPage(webDriver);
        }
    }
}