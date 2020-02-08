using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AsyncInitializationWithSimpleInjectorDemo.IntegrationTests._Infrastructure;
using FluentAssertions;
using Xunit;

namespace AsyncInitializationWithSimpleInjectorDemo.IntegrationTests
{
	[Collection(IntegrationTestCollection.ID)]
	public class PingControllerTests
	{
		private readonly IntegrationTestHttpClientFactory _httpClientFactory;

		public PingControllerTests(IntegrationTestHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
		}

		[Fact]
		public async Task ReturnsHTTP200()
		{
			(await _httpClientFactory.CreateClientWithoutToken().GetAsync(MakeURI(), CancellationToken.None))
				.Should()
				.Match<HttpResponseMessage>(message => message.StatusCode == HttpStatusCode.OK);
		}

		private static string MakeURI() => "ping";
	}
}
