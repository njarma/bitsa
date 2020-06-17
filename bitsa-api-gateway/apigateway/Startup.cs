using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Eureka;

namespace Bitsa.ApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var audienceConfig = Configuration.GetSection("Audience");

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
            {
                //builder.WithOrigins("http://10.0.32.11:8090");
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "BitsaKey";
            });

            services.AddAuthentication()
                    .AddJwtBearer("BitsaKey", x =>
                     {
                         x.RequireHttpsMetadata = false;
                         x.TokenValidationParameters = tokenValidationParameters;
                     });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOcelot(Configuration);
            //services.AddOcelot(Configuration).AddEureka();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // -- Loging
            loggerFactory.AddNLog();  
            loggerFactory.ConfigureNLog("nlog.config"); 
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));  
            app.UseOcelot().Wait();
            // -- Loging 

            var configuration = new OcelotPipelineConfiguration
            {
                PreQueryStringBuilderMiddleware = async (ctx, next) =>
                {
                    var name = ctx.HttpContext.User.Identity.Name;
                    //Console.WriteLine(name);
                    //Log.Information($"{name}");
                    await next.Invoke();
                }
            };

            app.UseCors("AllowOrigin");
            app.UseAuthentication(); // auth

            app.UseMvc();
            await app.UseOcelot(configuration);
        }
    }
}
