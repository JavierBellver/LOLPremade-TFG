using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using React.AspNet;
using lolpremade.Data;
using lolpremade.Models;
using Microsoft.EntityFrameworkCore;
using lolpremade.Utils.Services;

namespace lolpremade
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<LolpremadeContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRiotAPIService, RiotAPIService>();
            services.AddReact();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, LolpremadeContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseReact(config =>
            {
                // If you want to use server-side rendering of React components,
                // add all the necessary JavaScript files here. This includes
                // your components as well as all of their dependencies.
                // See http://reactjs.net/ for more information. Example:
                //config
                //  .AddScript("~/Scripts/First.jsx")
                //  .AddScript("~/Scripts/Second.jsx");

                // If you use an external build too (for example, Babel, Webpack,
                // Browserify or Gulp), you can improve performance by disabling
                // ReactJS.NET's version of Babel and loading the pre-transpiled
                // scripts. Example:
                //config
                //  .SetLoadBabel(false)
                //  .AddScriptWithoutTransform("~/Scripts/bundle.server.js");
            });

            app.UseStaticFiles();

            ConfigureAuth(app,context);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "login-route",
                    template: "login",
                    defaults: new { controller = "LandingPage", action = "Index" }
                );
                routes.MapRoute(
                    name: "register-route",
                    template: "register",
                    defaults: new { controller = "LandingPage", action = "Index" }
                );
                routes.MapRoute(
                   name: "home-route",
                   template: "home",
                   defaults: new { controller = "MainPage", action = "Index" }
                );
                routes.MapRoute(
                   name: "teams-route",
                   template: "teams",
                   defaults: new { controller = "MainPage", action = "Index" }
                );
                routes.MapRoute(
                   name: "users-route",
                   template: "users",
                   defaults: new { controller = "MainPage", action = "Index" }
                );
                routes.MapRoute(
                   name: "tournaments-route",
                   template: "tournaments",
                   defaults: new { controller = "MainPage", action = "Index" }
                );
                routes.MapRoute(
                   name: "user-profile-route",
                   template: "profile",
                   defaults: new { controller = "MainPage", action = "Index" }
                );
                routes.MapRoute(
                   name: "team-profile-route",
                   template: "teamprofile",
                   defaults: new { controller = "MainPage", action = "Index" }
                );
                routes.MapRoute(
                   name: "about",
                   template: "about",
                   defaults: new { controller = "MainPage", action = "Index" }
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=LandingPage}/{action=Index}/{id?}"
                );
            });

            DbInitializer.Initialize(context);
        }
    }
}
