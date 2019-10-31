namespace AISIDemo.EntityFramework.Models
{
	public class EnrollmentRecord
	{
		public int ID { get; set; }
		public int CourseID { get; set; }
		public int StudentID { get; set; }
		public Grade? Grade { get; set; }

		public CourseRecord Course { get; set; }
		public StudentRecord Student { get; set; }
	}

	public enum Grade { A, B, C, D, F };
}
