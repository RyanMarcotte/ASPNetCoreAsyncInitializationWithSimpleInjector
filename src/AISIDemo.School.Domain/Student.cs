using System;

namespace AISIDemo.School.Domain
{
	public class Student
	{
		public Student(int id, string firstName, string lastName, DateTime enrollmentDate)
		{
			ID = id;
			FirstName = firstName;
			LastName = lastName;
			EnrollmentDate = enrollmentDate;
		}

		public int ID { get; }
		public string FirstName { get; }
		public string LastName { get; }
		public DateTime EnrollmentDate { get; }
	}
}
