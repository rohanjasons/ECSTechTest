using Xunit;

namespace Helper.Test.Configuration.Base
{
    [CollectionDefinition("Configuration Collection")]
    public class ConfigurationFixtureBase : ICollectionFixture<ConfigurationFixture> { }
}

