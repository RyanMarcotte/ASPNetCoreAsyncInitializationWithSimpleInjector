using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncInitializationWithSimpleInjectorDemo.IntegrationTests._Infrastructure
{
	public class IntegrationTestHttpClientFactory : IntegrationTestHttpClientFactoryBase
	{
		private static readonly IntegrationTestWebApplicationFactory _webApplicationFactory = new IntegrationTestWebApplicationFactory();
		private static readonly IntegrationTestHttpClientFactoryConfiguration _httpClientFactoryConfiguration = new IntegrationTestHttpClientFactoryConfiguration();

		public IntegrationTestHttpClientFactory()
			: base(() => _webApplicationFactory.WithWebHostBuilder(builder => { }).CreateClient(), _httpClientFactoryConfiguration)
		{

		}

		private static void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
		{
			// https://stackoverflow.com/a/54202403 (adding seed data for integration tests)
			// https://stackoverflow.com/q/14485115 (avoid deadlock by running async code with Task.Run)
			var serviceProvider = services.BuildServiceProvider();
			Task.Run(async () => await serviceProvider.BuildInitializationContainerAndPerformInitialization()).Wait();
		}
	}
}
