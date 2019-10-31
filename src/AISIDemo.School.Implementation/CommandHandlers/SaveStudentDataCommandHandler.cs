using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AISIDemo.School.Domain.Commands;
using Functional;
using Functional.CQS;

namespace AISIDemo.School.Implementation.CommandHandlers
{
	public class SaveStudentDataCommandHandler : IAsyncCommandHandler<SaveStudentDataCommand, Exception>
	{
		public Task<Result<Unit, Exception>> HandleAsync(SaveStudentDataCommand parameters, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
