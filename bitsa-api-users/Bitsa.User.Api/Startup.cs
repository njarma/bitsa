using API.Exceptions;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Bitsa.User.Api.Model.Classes;
using Bitsa.User.Api.ViewModels.ClassesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bitsa.User.Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BaseViewModel>());
            services.AddControllers();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorOnly", policy =>
                                  policy.RequireClaim("Administrator", "1"));
            });

            #region Cors

            services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
            {
                //builder.WithOrigins("http://10.0.32.11:8090");
                builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
            }));

            #endregion


            ConfigureAuth(services);

            #region Services

            GetTypes("Services", "Service").ToList().ForEach(type => services.TryAddScoped(type.Key, type.Value));

            #endregion

            #region Repositories

            GetTypes("Repositories", "Repository").ToList().ForEach(type => services.TryAddScoped(type.Key, type.Value));

            #endregion

            #region DbContext
            var conn = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DomainContext>(options => options.UseMySQL(conn));
            services.AddScoped<DbContext, DomainContext>();
            #endregion

            #region AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            services.AddHttpClient();

            #region Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bitsa User Api", Version = "v1" });
            });
            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bitsa User Api");
            });

            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseExceptionHandlerMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        protected virtual void ConfigureAuth(IServiceCollection services)
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

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "BitsaKey";
                o.DefaultChallengeScheme = "BitsaKey";
            })
            .AddJwtBearer("BitsaKey", x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });

        }

        private IDictionary<Type, Type> GetTypes(string nameSpace, string endWith)
        {
            var res = new Dictionary<Type, Type>();
            var thisAssembly = Assembly.GetExecutingAssembly();
            var assemblyTypes = thisAssembly.GetTypes();
            foreach (var typeImplementation in assemblyTypes
                     .Where(p => p.Name.EndsWith(endWith))
                     .Where(p => !p.Name.Contains("Generic"))
                     .Where(t => string.Equals(t.Namespace, thisAssembly.GetName().Name + "." + nameSpace, StringComparison.Ordinal)))
                res.Add(assemblyTypes.FirstOrDefault(p => p.Name == "I" + typeImplementation.Name), typeImplementation);

            return res;
        }
    }

}
