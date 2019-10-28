using Functional.CQS;
using System;

namespace AISIDemo.School.Domain.Commands
{
	public class SaveEnrollmentDataCommand : ICommandParameters<Exception>
	{
		public SaveEnrollmentDataCommand(Enrollment[] enrollmentCollection)
		{
			EnrollmentCollection = enrollmentCollection ?? throw new ArgumentNullException(nameof(enrollmentCollection));
		}

		public Enrollment[] EnrollmentCollection { get; }
	}
}
