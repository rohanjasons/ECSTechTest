using Xunit;
using Web.Tests.Tests.Base;
using Helper.Test.Configuration;
using System.Collections.Generic;
using Web.Integration.Test.Helper;
using Shouldly;

namespace Web.Tests.Tests.xUnit
{
    [Collection("Configuration Collection")]
    public class WebTests : WebBase
    {
        public WebTests(ConfigurationFixture testConfig) : base(testConfig) { }

        private static readonly List<int> arrayOne = new List<int> { 1,2,3,4,5,4,3,2,1 };

        [Fact]
        public void EcsTechnicalTest_EndToEnd_Success()
        {
            var result = TestHelper.CheckPointArraySumMatches(arrayOne);
            result.ShouldBe(4);
        }
    }
}