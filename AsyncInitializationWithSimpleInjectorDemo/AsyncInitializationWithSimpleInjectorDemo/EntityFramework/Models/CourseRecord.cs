using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	public class CourseRecord
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int ID { get; set; }
		public string Title { get; set; }
		public int Credits { get; set; }

		public ICollection<EnrollmentRecord> Enrollments { get; set; }
	}
}
