using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AISIDemo.EntityFramework.Infrastructure;
using AsyncInitializationWithSimpleInjectorDemo.Initialization;
using AsyncInitializationWithSimpleInjectorDemo.Initialization.Steps;
using AsyncInitializationWithSimpleInjectorDemo.IoC;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			await CreateWebHostBuilder(args).Build().InitializeAsync().RunAsync();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
	}

	internal static class AsyncInitializationExtensions
	{
		public static async Task<IWebHost> InitializeAsync(this IWebHost host)
		{
			using (var scope = host.Services.CreateScope())
			{
				// register any infrastructure components that have already been registered with the .NET Core DI container
				var container = new Container();
				container.RegisterInstance(scope.ServiceProvider.GetService<DbContextOptions<SchoolContext>>());

				// register the individual async initializers in the order you want to execute them
				container.Collection.Register<IAsyncInitializer>(
					typeof(EntityFrameworkDatabaseMigrationAsyncInitializer),
					typeof(EntityFrameworkDatabaseSeedDataAsyncInitializer));
				container.Register<RootInitializer>();

				// register all your application components
				container.RegisterApplicationComponentsAndVerify();

				// execute all asynchronous initializers specified above
				await container.GetInstance<RootInitializer>().InitializeAsync(CancellationToken.None);
			}

			return host;
		}

		public static async Task RunAsync(this Task<IWebHost> host) => await (await host).RunAsync();
	}
}
