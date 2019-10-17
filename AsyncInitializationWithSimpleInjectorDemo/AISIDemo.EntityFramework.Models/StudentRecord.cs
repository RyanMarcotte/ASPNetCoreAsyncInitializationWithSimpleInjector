using System;
using System.Collections.Generic;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	public class StudentRecord
	{
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime EnrollmentDate { get; set; }

		public ICollection<EnrollmentRecord> Enrollments { get; set; }
	}
}
