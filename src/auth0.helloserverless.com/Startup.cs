using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using auth0.helloserverless.com.Initialization;
using clearwaterstream.Configuration;
using clearwaterstream.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace auth0.helloserverless.com
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, IHostingEnvironment env, ILogger<Startup> logger)
        {
            AppEnvironment.SetName(env.EnvironmentName);

            AppInitializer.Initialize(configuration);

            Configuration = configuration;
            HostingEnvironment = env;

            _logger = logger;

            _logger.LogInformation("app is booting");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ServiceRegistrar.SetServiceProvider(app.ApplicationServices);

            var appLifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            appLifetime.ApplicationStopping.Register(OnShuttingDown);

            // global hanlder for any uncaught exceptions
            app.UseExceptionHandler(new ExceptionHandlerOptions() { ExceptionHandler = GlobalErrorHandler.ExceptionHandlerDelegate });

            app.UseMvc(ConfigureRoutes);
        }

        protected virtual void ConfigureRoutes(IRouteBuilder routes)
        {
            routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
        }

        void OnShuttingDown()
        {
            _logger.LogInformation("app is shutting down");
        }
    }
}
