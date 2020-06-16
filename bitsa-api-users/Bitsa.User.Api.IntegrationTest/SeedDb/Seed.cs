using Bitsa.User.Api.Model.Classes;

namespace Bitsa.User.Api.IntegrationTest
{
    public abstract class Seed
    {
        public abstract void PopulateTestData(DomainContext dbContext);
    }
}
