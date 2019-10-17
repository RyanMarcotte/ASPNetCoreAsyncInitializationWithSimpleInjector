using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Functional;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	public class EntityFrameworkDatabaseSeedDataAsyncInitializer : IAsyncInitializer
	{
		private readonly IDbContextFactory<SchoolContext> _dbContextFactory;

		public EntityFrameworkDatabaseSeedDataAsyncInitializer(IDbContextFactory<SchoolContext> dbContextFactory)
		{
			_dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
		}

		public Task<Result<Unit, Exception>> InitializeAsync(CancellationToken cancellationToken = default)
		{
			return Result.TryAsync(async () =>
			{
				using (var context = _dbContextFactory.CreateContext())
				{
					if (context.Students.Any())
						return;

					var studentRecords = new StudentRecord[]
					{
						new StudentRecord{FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
						new StudentRecord{FirstName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
						new StudentRecord{FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
						new StudentRecord{FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
						new StudentRecord{FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
						new StudentRecord{FirstName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
						new StudentRecord{FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
						new StudentRecord{FirstName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
					};

					foreach (var s in studentRecords)
						context.Students.Add(s);

					var courseRecords = new CourseRecord[]
					{
						new CourseRecord{ID=1050,Title="Chemistry",Credits=3},
						new CourseRecord{ID=4022,Title="Microeconomics",Credits=3},
						new CourseRecord{ID=4041,Title="Macroeconomics",Credits=3},
						new CourseRecord{ID=1045,Title="Calculus",Credits=4},
						new CourseRecord{ID=3141,Title="Trigonometry",Credits=4},
						new CourseRecord{ID=2021,Title="Composition",Credits=3},
						new CourseRecord{ID=2042,Title="Literature",Credits=4}
					};

					foreach (CourseRecord c in courseRecords)
						context.Courses.Add(c);

					var enrollmentRecords = new EnrollmentRecord[]
					{
						new EnrollmentRecord{StudentID=1,CourseID=1050,Grade=Grade.A},
						new EnrollmentRecord{StudentID=1,CourseID=4022,Grade=Grade.C},
						new EnrollmentRecord{StudentID=1,CourseID=4041,Grade=Grade.B},
						new EnrollmentRecord{StudentID=2,CourseID=1045,Grade=Grade.B},
						new EnrollmentRecord{StudentID=2,CourseID=3141,Grade=Grade.F},
						new EnrollmentRecord{StudentID=2,CourseID=2021,Grade=Grade.F},
						new EnrollmentRecord{StudentID=3,CourseID=1050},
						new EnrollmentRecord{StudentID=4,CourseID=1050},
						new EnrollmentRecord{StudentID=4,CourseID=4022,Grade=Grade.F},
						new EnrollmentRecord{StudentID=5,CourseID=4041,Grade=Grade.C},
						new EnrollmentRecord{StudentID=6,CourseID=1045},
						new EnrollmentRecord{StudentID=7,CourseID=3141,Grade=Grade.A},
					};

					foreach (EnrollmentRecord e in enrollmentRecords)
						context.Enrollments.Add(e);
					
					await context.SaveChangesAsync();
				}
			});
		}
	}
}
