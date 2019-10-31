using System;
using System.Threading;
using System.Threading.Tasks;
using AISIDemo.School.Domain.Commands;
using Functional;
using Functional.CQS;

namespace AISIDemo.School.Implementation.CommandHandlers
{
	public class SaveCourseDataCommandHandler : IAsyncCommandHandler<SaveCourseDataCommand, Exception>
	{
		public Task<Result<Unit, Exception>> HandleAsync(SaveCourseDataCommand parameters, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
