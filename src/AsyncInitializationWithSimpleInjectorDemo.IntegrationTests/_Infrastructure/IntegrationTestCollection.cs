using Xunit;

namespace AsyncInitializationWithSimpleInjectorDemo.IntegrationTests._Infrastructure
{
	[CollectionDefinition(ID)]
	public class IntegrationTestCollection : ICollectionFixture<IntegrationTestHttpClientFactory>
	{
		public const string ID = "APPLICATION_TEST";
	}
}