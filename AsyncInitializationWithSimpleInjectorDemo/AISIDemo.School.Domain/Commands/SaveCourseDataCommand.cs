using Functional.CQS;
using System;

namespace AISIDemo.School.Domain.Commands
{
	public class SaveCourseDataCommand : ICommandParameters<Exception>
	{
		public SaveCourseDataCommand(Course[] courseCollection)
		{
			CourseCollection = courseCollection ?? throw new ArgumentNullException(nameof(courseCollection));
		}

		public Course[] CourseCollection { get; }
	}
}
