using BoDi;
using Helper.Test.Configuration;
using Web.Test.Selenium;
using Xunit.Abstractions;

namespace Web.Tests.Tests.Base
{
    public class WebBase : WebTestBase
    {
        protected ConfigurationFixture testConfig;
        protected IObjectContainer objectContainer;
        protected ITestOutputHelper testOutputHelper;

        protected WebBase(){}

        protected WebBase(ConfigurationFixture testConfig, IObjectContainer objectContainer, ITestOutputHelper testOutputHelper)
        {
            this.testConfig = testConfig;
            this.objectContainer = objectContainer;
            this.testOutputHelper = testOutputHelper;
        }

        protected WebBase(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        protected WebBase(ConfigurationFixture testConfig, IObjectContainer objectContainer)
        {
            this.testConfig = testConfig;
            this.objectContainer = objectContainer;
        }

        protected WebBase(ConfigurationFixture testConfig)
        {
            this.testConfig = testConfig;
        }
    }
}