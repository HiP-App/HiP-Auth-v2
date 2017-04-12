using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaderbornUniversity.SILab.Hip.Auth.Utility;

namespace PaderbornUniversity.SILab.Hip.Auth
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            loggerFactory.AddConsole(LogLevel.Debug);

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

            var appConfig = new AppConfig(Configuration);
            // configure identity server with in-memory stores, keys, clients and resources
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryApiResources(AuthConfig.GetApiResources())
                .AddInMemoryClients(AuthConfig.GetClients(appConfig))
                .AddTestUsers(AuthConfig.GetUsers(appConfig))
                .AddInMemoryIdentityResources(AuthConfig.GetIdentityResources());
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();

			app.UseStaticFiles();
			app.UseMvcWithDefaultRoute();
        }
    }
}