using System.Linq;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
            services.AddSingleton<AppConfig>(appConfig);

            // server=(localdb)\mssqllocaldb;database=IdentityServer4.Quickstart.EntityFramework;trusted_connection=yes";
			var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

			// configure identity server with in-memory users, but EF stores for clients and resources
			services.AddIdentityServer()
				.AddTemporarySigningCredential()
				.AddTestUsers(AuthConfig.GetUsers(appConfig))
				.AddConfigurationStore(builder =>
					builder.UseSqlServer(appConfig.DatabaseConfig.ConnectionString, options =>
						options.MigrationsAssembly(migrationsAssembly)))
				.AddOperationalStore(builder =>
					builder.UseSqlServer(appConfig.DatabaseConfig.ConnectionString, options =>
						options.MigrationsAssembly(migrationsAssembly)));
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, AppConfig config)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            app.UseDeveloperExceptionPage();

            InitializeDatabase(app, config);

            app.UseIdentityServer();

			app.UseStaticFiles();
			app.UseMvcWithDefaultRoute();
        }

        private void InitializeDatabase(IApplicationBuilder app, AppConfig config)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in AuthConfig.GetClients(config))
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in AuthConfig.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in AuthConfig.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}