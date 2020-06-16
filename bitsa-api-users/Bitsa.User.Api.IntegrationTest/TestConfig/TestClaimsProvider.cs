using System.Collections.Generic;
using System.Security.Claims;

namespace Bitsa.User.Api.IntegrationTest.TestConfig
{
    public class TestClaimsProvider
    {
        public IList<Claim> Claims { get; }

        public TestClaimsProvider(IList<Claim> claims)
        {
            Claims = claims;
        }

        public TestClaimsProvider()
        {
            Claims = new List<Claim>();
        }

        public static TestClaimsProvider WithAdminClaims()
        {
            var provider = new TestClaimsProvider();
            provider.Claims.Add(new Claim("UserId", "1"));
            provider.Claims.Add(new Claim("CompanyId", "1"));

            return provider;
        }

        public static TestClaimsProvider WithUserClaims()
        {
            var provider = new TestClaimsProvider();
            provider.Claims.Add(new Claim("UserId", "1"));
            provider.Claims.Add(new Claim("CompanyId", "1"));

            return provider;
        }
    }
}
