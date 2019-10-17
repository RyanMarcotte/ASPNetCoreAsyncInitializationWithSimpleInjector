using Microsoft.EntityFrameworkCore;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	public class SchoolContext : DbContext
	{
		public SchoolContext(DbContextOptions<SchoolContext> options)
			: base(options)
		{
			
		}

		public DbSet<CourseRecord> Courses { get; set; }
		public DbSet<EnrollmentRecord> Enrollments { get; set; }
		public DbSet<StudentRecord> Students { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CourseRecord>().ToTable("Course");
			modelBuilder.Entity<EnrollmentRecord>().ToTable("Enrollment");
			modelBuilder.Entity<StudentRecord>().ToTable("Student");
		}
	}
}
