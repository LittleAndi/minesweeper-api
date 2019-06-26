using MineSweeper.Host;
using Xunit;

namespace Test.Level0.MineSweeper.Host
{
    [Trait("Category", "L0")]
    public class AutoMapperConfigurationTests
    {
        [Fact]
        public void AssertConfigurationIsValid()
        {
            AutoMapperConfiguration.Configure();
            AutoMapperConfiguration.Configuration.AssertConfigurationIsValid();
        }
    }
}
