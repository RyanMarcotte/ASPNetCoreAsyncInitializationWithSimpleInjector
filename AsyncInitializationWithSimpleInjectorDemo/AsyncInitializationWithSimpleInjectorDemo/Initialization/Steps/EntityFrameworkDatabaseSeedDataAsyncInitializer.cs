using System;
using System.Threading;
using System.Threading.Tasks;
using Functional;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	public class EntityFrameworkDatabaseSeedDataAsyncInitializer : IAsyncInitializer
	{
		public Task<Result<Unit, Exception>> InitializeAsync(CancellationToken cancellationToken = default)
		{
			return Task.FromResult(Result.Unit<Exception>());
		}
	}
}
