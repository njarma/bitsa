using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Bitsa.User.Api.Model.Classes;
using System.IO;
using System.Linq;

namespace Bitsa.User.Api.IntegrationTest.TestConfig
{
    public class TestApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var projectDir = Directory.GetCurrentDirectory();

            return WebHost.CreateDefaultBuilder(null)
                          .UseStartup<TEntryPoint>()
                          .UseSolutionRelativeContentRoot("Product.Microservice.Api")
                          .UseConfiguration(new ConfigurationBuilder()
                                                .SetBasePath(projectDir)
                                                .AddJsonFile("appsettings.json")
                                                .Build());
        }

        public InMemoryDatabaseRoot InMemoryDatabaseRoot;
        public DomainContext DbContext;
        public IConfiguration Configuration { get; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            InMemoryDatabaseRoot = new InMemoryDatabaseRoot();
            builder.ConfigureServices(ConfigureServices);
            builder.ConfigureLogging((WebHostBuilderContext context, ILoggingBuilder loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole(options => options.IncludeScopes = true);
            });
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddApplicationPart(typeof(Controllers.ClaimsController).Assembly);

            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DomainContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            services.AddDbContext<DomainContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting", InMemoryDatabaseRoot)
                /*.UseInternalServiceProvider(serviceProvider)*/;
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                DbContext = scopedServices.GetRequiredService<DomainContext>();
                DbContext.Database.EnsureCreated();
            }
        }

        public void PopulateDb(params Seed[] seedsDb)
        {
            using (var scope = Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<DomainContext>();
                db.Database.EnsureDeleted();
                seedsDb.ToList().ForEach(p => p.PopulateTestData(db));
            }
        }

    }
}
