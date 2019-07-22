using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Example.Api.Database;
using CQRS.Example.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CQRS.Example.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Swagger for API",
                    Version = "v1"
                });
            });

            this.ConfigureDi(services);
            this.ConfigureOptions(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger API");
            });
        }

        private void ConfigureDi(IServiceCollection services)
        {
            services.AddSingleton<IRavenStore, RavenStore>();
            services.AddSingleton<ITestDataService, TestDataService>();
            services.AddTransient<IShoppingQueryService, ShoppingQueryService>();
            services.AddSingleton<INotificationService, NotificationService>();
        }

        private void ConfigureOptions(IServiceCollection services)
        {
            services.Configure<RavenDbSettings>(this.Configuration.GetSection("Raven"));
        }
    }
}
