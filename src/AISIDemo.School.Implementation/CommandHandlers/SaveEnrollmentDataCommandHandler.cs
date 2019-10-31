using System;
using System.Threading;
using System.Threading.Tasks;
using AISIDemo.School.Domain.Commands;
using Functional;
using Functional.CQS;

namespace AISIDemo.School.Implementation.CommandHandlers
{
	public class SaveEnrollmentDataCommandHandler : IAsyncCommandHandler<SaveEnrollmentDataCommand, Exception>
	{
		public Task<Result<Unit, Exception>> HandleAsync(SaveEnrollmentDataCommand parameters, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
