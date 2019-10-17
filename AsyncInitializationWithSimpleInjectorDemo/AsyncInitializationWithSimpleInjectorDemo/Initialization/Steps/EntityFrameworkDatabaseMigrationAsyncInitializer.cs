using System;
using System.Threading;
using System.Threading.Tasks;
using Functional;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	public class EntityFrameworkDatabaseMigrationAsyncInitializer : IAsyncInitializer
	{
		public Task<Result<Unit, Exception>> InitializeAsync(CancellationToken cancellationToken = default)
		{
			return Result.TryAsync(() => throw new NotImplementedException());
		}
	}
}
