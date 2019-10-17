using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
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
				var container = new Container();
				container.Collection.Register<IAsyncInitializer>(typeof(EntityFrameworkDatabaseMigrationAsyncInitializer));
				container.Register<RootInitializer>();
				container.RegisterApplicationComponentsAndVerify();

				await container.GetInstance<RootInitializer>().InitializeAsync();
			}

			return host;
		}

		public static async Task RunAsync(this Task<IWebHost> host) => await (await host).RunAsync();
	}
}
