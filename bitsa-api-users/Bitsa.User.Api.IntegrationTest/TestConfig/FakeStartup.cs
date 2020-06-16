using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bitsa.User.Api.IntegrationTest.TestConfig
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureAuth(IServiceCollection services)
        {

        }


        //public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    base.Configure(app, env);          

        //}
    }
}
