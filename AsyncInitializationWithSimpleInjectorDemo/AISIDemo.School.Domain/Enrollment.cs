using System.Collections.Generic;
using System.Text;

namespace AISIDemo.School.Domain
{
	public class Enrollment
	{
		public Enrollment(int id, int courseID, int studentID, Grade grade)
		{
			ID = id;
			CourseID = courseID;
			StudentID = studentID;
			Grade = grade;
		}

		public int ID { get; }
		public int CourseID { get; }
		public int StudentID { get; }
		public Grade Grade { get; }
	}
}
