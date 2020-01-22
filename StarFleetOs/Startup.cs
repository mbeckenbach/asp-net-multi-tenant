using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StarFleetOs.Database.App;
using StarFleetOs.Database.Tenants;

namespace StarFleetOs
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Register the db context with dependency injection
            services.AddDbContext<AppDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register the db context with dependency injection
            services.AddDbContext<TenantsDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Provide MVC
            services.AddControllersWithViews();

            // Add OpenAPI/Swagger document
            // registers a Swagger v2.0 document with the name "v1" (default)
            // see https://github.com/RSuter/NSwag/wiki/AspNetCore-Middleware
            services.AddSwaggerDocument(configure =>
            {
                configure.PostProcess = (document) =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = $"{nameof(StarFleetOs)}";
                    document.Info.Description = "API Explorer";
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Error Handling
            if (env.IsDevelopment())
            {
                // Show exceptions
                app.UseDeveloperExceptionPage();
            }

            // Add OpenAPI/Swagger middleware
            // Serves the registered OpenAPI/Swagger documents by default on `/swagger/{documentName}/swagger.json`
            app.UseOpenApi();
            app.UseSwaggerUi3();

            // Adds endpoint routing support
            app.UseRouting();

            // Sets default endpoint routes
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
