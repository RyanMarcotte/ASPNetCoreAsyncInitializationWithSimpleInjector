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
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			await CreateWebHostBuilder(args)
				.Build()
				.InitializeAsync()
				.RunAsync();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
	}

	internal static class AsyncInitializationExtensions
	{
		/// <summary>
		/// At this point, <see cref="Startup.ConfigureServices"/> has been run, but not <see cref="Startup.Configure"/>.  Thus, we build
		/// a separate <see cref="Container"/> for async initialization.  The cost of performing async initialization itself is greater than
		/// performing application component registration twice (using <see cref="SimpleInjectorExtensions.RegisterApplicationComponentsAndVerify"/>
		/// here and in <see cref="Startup.Configure"/>), so the minor performance hit is an acceptable tradeoff.  The split between a 
		/// 'startup <see cref="Container"/>' and a 'runtime <see cref="Container"/>' also prevents async initialization components from being 
		/// resolved after startup (because the 'runtime <see cref="Container"/>' did not have the async initialization components registered with it).
		/// </summary>
		/// <param name="host"></param>
		/// <returns></returns>
		public static async Task<IWebHost> InitializeAsync(this IWebHost host)
		{
			using (var scope = host.Services.CreateScope())
			{
				// register any infrastructure components that have already been registered with the .NET Core DI container
				// you only need to register the infrastructure components that are required by asynchronous initializers
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
