using System;
using System.Net.Http;

namespace AsyncInitializationWithSimpleInjectorDemo.IntegrationTests._Infrastructure
{
	public abstract class IntegrationTestHttpClientFactoryBase : IDisposable
	{
		private readonly Lazy<HttpClient> _clientWithoutToken;

		protected IntegrationTestHttpClientFactoryBase(Func<HttpClient> httpClientFactory, IntegrationTestHttpClientFactoryConfiguration configuration)
		{
			if (httpClientFactory == null) throw new ArgumentNullException(nameof(httpClientFactory));
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			_clientWithoutToken = new Lazy<HttpClient>(httpClientFactory);
		}

		public HttpClient CreateClientWithoutToken() => _clientWithoutToken.Value;

		public void Dispose()
		{
			if (_clientWithoutToken.IsValueCreated)
				_clientWithoutToken.Value.Dispose();
		}
	}
}