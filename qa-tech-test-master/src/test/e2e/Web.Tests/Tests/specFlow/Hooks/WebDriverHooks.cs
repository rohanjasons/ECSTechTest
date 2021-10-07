using BoDi;
using TechTalk.SpecFlow;
using Web.Tests.Tests.Base;

namespace Web.Tests.Tests.specFlow.Hooks
{
    [Binding]
    public class WebDriverHooks: WebBase
    {
        public WebDriverHooks(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [AfterScenario()]
        public static void AfterScenario()
        {
            WebBrowserDriver?.Quit();
            WebBrowserDriver?.Dispose();
        }
    }
}