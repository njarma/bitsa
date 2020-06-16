using Xunit;

namespace Bitsa.User.Api.IntegrationTest.TestConfig
{
    public abstract class TestBase : IClassFixture<TestApplicationFactory<FakeStartup>>
    {
        protected TestApplicationFactory<FakeStartup> Factory { get; }

        public TestBase(TestApplicationFactory<FakeStartup> factory)
        {
            Factory = factory;
        }
    }
}
