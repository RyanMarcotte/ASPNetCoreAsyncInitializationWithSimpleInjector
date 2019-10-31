using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AISIDemo.EntityFramework.Infrastructure;
using Functional;
using Functional.CQS;
using Microsoft.EntityFrameworkCore;

namespace AsyncInitializationWithSimpleInjectorDemo.Initialization.QueryHandlers
{
	public class GetSchoolInitializationStatusQuery : IQueryParameters<Result<bool, Exception>>
	{
	}

	public class GetSchoolInitializationStatusQueryHandler : IAsyncQueryHandler<GetSchoolInitializationStatusQuery, Result<bool, Exception>>
	{
		private readonly IDbContextFactory<SchoolContext> _dbContextFactory;

		public GetSchoolInitializationStatusQueryHandler(IDbContextFactory<SchoolContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
		}

		public Task<Result<bool, Exception>> HandleAsync(GetSchoolInitializationStatusQuery parameters, CancellationToken cancellationToken)
		{
			return Result.TryAsync(async () =>
			{
				using (var context = _dbContextFactory.CreateContext())
				{
					return await context.Courses.AnyAsync(cancellationToken);
				}
			});
		}
	}
}
