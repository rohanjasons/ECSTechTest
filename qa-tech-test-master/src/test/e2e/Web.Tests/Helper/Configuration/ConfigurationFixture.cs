using System.IO;
using Microsoft.Extensions.Configuration;

namespace Helper.Test.Configuration
{
    public class ConfigurationFixture
    {
        public ConfigurationFixture()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", false)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        private IConfiguration Configuration { get; }

        public string BaseUrl => Configuration["BaseUrl"];
    }
}