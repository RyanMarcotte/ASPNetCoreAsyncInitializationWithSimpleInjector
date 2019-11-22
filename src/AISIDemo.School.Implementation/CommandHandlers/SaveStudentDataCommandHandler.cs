using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
	public class SaveStudentDataCommandHandler : IAsyncCommandHandler<SaveStudentDataCommand, Exception>
	{
		private readonly IDbContextFactory<SchoolContext> _contextFactory;

		public SaveStudentDataCommandHandler(IDbContextFactory<SchoolContext> contextFactory)
		{
			_contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
		}

		public Task<Result<Unit, Exception>> HandleAsync(SaveStudentDataCommand parameters, CancellationToken cancellationToken)
		{
			return Result.TryAsync(async () =>
			{
				using (var context = _contextFactory.CreateContext())
				{
					context.Students.AddRange(parameters.StudentCollection.Select(x => x.ToStudentRecord()).ToArray());
					await context.SaveChangesAsync(cancellationToken);
				}
			});
		}
	}

	internal static class StudentExtensions
	{
		public static StudentRecord ToStudentRecord(this Student source)
		{
			return new StudentRecord()
			{
				ID = source.ID,
				FirstName = source.FirstName,
				LastName = source.LastName,
				EnrollmentDate = source.EnrollmentDate
			};
		}
	}
}
