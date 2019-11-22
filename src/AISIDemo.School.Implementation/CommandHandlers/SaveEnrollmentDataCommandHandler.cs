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
using Grade = AISIDemo.EntityFramework.Models.Grade;

namespace AISIDemo.School.Implementation.CommandHandlers
{
	public class SaveEnrollmentDataCommandHandler : IAsyncCommandHandler<SaveEnrollmentDataCommand, Exception>
	{
		private readonly IDbContextFactory<SchoolContext> _contextFactory;

		public SaveEnrollmentDataCommandHandler(IDbContextFactory<SchoolContext> contextFactory)
		{
			_contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
		}

		public Task<Result<Unit, Exception>> HandleAsync(SaveEnrollmentDataCommand parameters, CancellationToken cancellationToken)
		{
			return Result.TryAsync(async () =>
			{
				using (var context = _contextFactory.CreateContext())
				{
					context.Enrollments.AddRange(parameters.EnrollmentCollection.Select(x => x.ToEnrollmentRecord()).ToArray());
					await context.SaveChangesAsync(cancellationToken);
				}
			});
		}
	}

	internal static class EnrollmentExtensions
	{
		public static EnrollmentRecord ToEnrollmentRecord(this Enrollment source)
		{
			return new EnrollmentRecord()
			{
				ID = source.ID,
				CourseID = source.CourseID,
				StudentID = source.StudentID,
				Grade = source.Grade.Match(a => Grade.A, b => Grade.B, c => Grade.C, d => Grade.D, f => Grade.F)
			};
		}
	}
}
