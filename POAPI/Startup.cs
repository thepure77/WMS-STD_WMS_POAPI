using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using jwt.Shared.System.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace GRAPI
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.Configure<JwtTokenValidationSettings>(Configuration.GetSection(nameof(JwtTokenValidationSettings)));
            //services.AddSingleton<IJwtTokenValidationSettings, JwtTokenValidationSettingsFactory>();
            services.AddSwaggerGen(options =>
            {
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                options.AddSecurityDefinition("Bearer", new Swashbuckle.AspNetCore.Swagger.ApiKeyScheme()
                {
                    Description = "Truck queue gate format : Bearer {token}",
                    Name = "Plan GR API",
                    In = "header",
                    Type = "apiKey"
                });

                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Plan GR API",
                    Version = "v1",
                    Description = "The Catalog Microservice HTTP API. This is a Data-Driven/CRUD microservice",
                    TermsOfService = "Service For Kasco INET JV Only"
                });

                options.AddSecurityRequirement(security);
            });


            // Create TokenValidation factory with DI priciple
            var tokenValidationSettings = services.BuildServiceProvider().GetService<IJwtTokenValidationSettings>();

            //// todo
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //      .AddJwtBearer(options =>
            //      {
            //          options.TokenValidationParameters = tokenValidationSettings.CreateTokenValidationParameters();
            //          options.SaveToken = true;
            //          // todo options.RequireHttpsMetadata = false;
            //      });

            //// Secure all controllers by default
            //var authorizePolicy = new AuthorizationPolicyBuilder()
            //                      .RequireAuthenticatedUser()
            //                      .Build();

            //// Add Mvc with options
            //services.AddMvc(config => { config.Filters.Add(new AuthorizeFilter(authorizePolicy)); }
            //)

            //// Override default camelCase style (yes its strange the default configuration results in camel case)
            //.AddJsonOptions(options =>
            //{
            //    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            //    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //});


            services.AddDbContext<PODbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

            app.UseHttpsRedirection();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                RequireHeaderSymmetry = false,
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Truck Queue API V1");
                });

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
