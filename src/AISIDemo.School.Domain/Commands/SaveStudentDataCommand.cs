using Functional.CQS;
using System;
using System.Collections.Generic;
using System.Text;

namespace AISIDemo.School.Domain.Commands
{
	public class SaveStudentDataCommand : ICommandParameters<Exception>
	{
		public SaveStudentDataCommand(Student[] studentCollection)
		{
			StudentCollection = studentCollection ?? throw new ArgumentNullException(nameof(studentCollection));
		}

		public Student[] StudentCollection { get; }
	}
}
