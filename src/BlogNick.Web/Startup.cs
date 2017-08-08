using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VillaNangie.Web.WebLogProvider;
using WilderMinds.MetaWeblog;
using VillaNangie.Data.Models;
using Microsoft.AspNetCore.Identity;
using VillaNangie.Data;
using VillaNangie.Data.Interfaces;
using VillaNangie.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using VillaNangie.Data.Utils;

namespace VillaNangie.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        private IHostingEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.AddDbContext<BlogContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BlogDb")));

            services.AddIdentity<BlogUser, IdentityRole>()
              .AddEntityFrameworkStores<BlogContext>();

            if (_env.IsDevelopment())
            {
                services.AddScoped<IBlogRepository, MemoryRepository>();
            }
            else
            {
                services.AddScoped<IBlogRepository, BlogRepository>();
            }

            services.AddAuthentication();
            services.AddJwtBearerAuthentication();

            services.AddGoogleAuthentication();
            // Supporting Live Writer (MetaWeblogAPI)
            services.AddMetaWeblog<OpenWebLogProvider>();

            // Add Caching Support
            services.AddMemoryCache(opt => opt.ExpirationScanFrequency = TimeSpan.FromMinutes(5));

            // Add MVC to the container
            var mvcBuilder = services.AddMvc();
            mvcBuilder.AddJsonOptions(opts => opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            // Add Https - renable once Azure Certs work
            if (_env.IsProduction()) mvcBuilder.AddMvcOptions(options => options.Filters.Add(new RequireHttpsAttribute()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceScopeFactory scopeFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                // Early so we can catch the StatusCode error
                app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // Support MetaWeblog API
            app.UseMetaWeblog("/livewriter");

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            if (env.IsDevelopment())
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var Seeder = scope.ServiceProvider.GetService<BlogDBSeeder>();
                    Seeder.SeedAsync().Wait();
                }
            }

        }
    }
}
