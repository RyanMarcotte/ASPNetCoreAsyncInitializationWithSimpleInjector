using System;
using System.Threading;
using System.Threading.Tasks;
using Functional;
using Microsoft.EntityFrameworkCore;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	public class EntityFrameworkDatabaseMigrationAsyncInitializer : IAsyncInitializer
	{
		private readonly IDbContextFactory<SchoolContext> _dbContextFactory;

		public EntityFrameworkDatabaseMigrationAsyncInitializer(IDbContextFactory<SchoolContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
		}

		public Task<Result<Unit, Exception>> InitializeAsync(CancellationToken cancellationToken = default)
		{
			return Result.TryAsync(async () =>
			{
				using (var context = _dbContextFactory.CreateContext())
				{
					await context.Database.MigrateAsync();
				}
			});
		}
	}
}
