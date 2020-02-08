using System;

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
	}
}
