using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncInitializationWithSimpleInjectorDemo.IntegrationTests._Infrastructure
{
	public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Startup>
	{
		// 
		private static readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.AddUserSecrets<IntegrationTestHttpClientFactory>()
			.Build();

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			base.ConfigureWebHost(builder);
			builder.ConfigureAppConfiguration((context, configuration) => configuration.AddConfiguration(_configuration));
			builder.ConfigureTestServices(ConfigureServices);
		}

		private static void ConfigureServices(IServiceCollection services)
		{
			// https://stackoverflow.com/a/54202403 (adding seed data for integration tests)
			// https://stackoverflow.com/q/14485115 (avoid deadlock by running async code with Task.Run)
			var serviceProvider = services.BuildServiceProvider();
			Task.Run(async () => await serviceProvider.BuildInitializationContainerAndPerformInitialization()).Wait();
		}
	}
}