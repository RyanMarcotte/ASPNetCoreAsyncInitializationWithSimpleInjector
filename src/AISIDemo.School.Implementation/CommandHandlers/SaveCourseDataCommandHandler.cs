using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AISIDemo.EntityFramework.Infrastructure;
using AISIDemo.EntityFramework.Models;
using AISIDemo.School.Domain;
using AISIDemo.School.Domain.Commands;
using Functional;
using Functional.CQS;

namespace AISIDemo.School.Implementation.CommandHandlers
{
	public class SaveCourseDataCommandHandler : IAsyncCommandHandler<SaveCourseDataCommand, Exception>
	{
		private readonly IDbContextFactory<SchoolContext> _contextFactory;

		public SaveCourseDataCommandHandler(IDbContextFactory<SchoolContext> contextFactory)
		{
			_contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
		}

		public Task<Result<Unit, Exception>> HandleAsync(SaveCourseDataCommand parameters, CancellationToken cancellationToken)
		{
			return Result.TryAsync(async () =>
			{
				using (var context = _contextFactory.CreateContext())
				{
					context.Courses.AddRange(parameters.CourseCollection.Select(x => x.ToCourseRecord()).ToArray());
					await context.SaveChangesAsync(cancellationToken);
				}
			});
		}
	}

	internal static class CourseExtensions
	{
		public static CourseRecord ToCourseRecord(this Course source)
		{
			return new CourseRecord()
			{
				ID = source.ID,
				Title = source.Title,
				Credits = source.Credits
			};
		}
	}
}
