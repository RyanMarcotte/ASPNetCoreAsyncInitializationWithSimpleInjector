using System;
using System.Threading;
using System.Threading.Tasks;
using AISIDemo.EntityFramework.Infrastructure;
using AISIDemo.School.Domain;
using AISIDemo.School.Domain.Commands;
using AsyncInitializationWithSimpleInjectorDemo.Initialization.QueryHandlers;
using Functional;
using Functional.CQS;

namespace AsyncInitializationWithSimpleInjectorDemo.Initialization.Steps
{
	public class EntityFrameworkDatabaseSeedDataAsyncInitializer : IAsyncInitializer
	{
		private readonly IAsyncQueryHandler<GetSchoolInitializationStatusQuery, Result<bool, Exception>> _getSchoolInitializationStatusQueryHandler;
		private readonly IAsyncCommandHandler<SaveStudentDataCommand, Exception> _saveStudentDataCommandHandler;
		private readonly IAsyncCommandHandler<SaveCourseDataCommand, Exception> _saveCourseDataCommandHandler;
		private readonly IAsyncCommandHandler<SaveEnrollmentDataCommand, Exception> _saveEnrollmentDataCommandHandler;

		private readonly IDbContextFactory<SchoolContext> _dbContextFactory;

		public EntityFrameworkDatabaseSeedDataAsyncInitializer(
			IAsyncQueryHandler<GetSchoolInitializationStatusQuery, Result<bool, Exception>> getSchoolInitializationStatusQueryHandler,
			IAsyncCommandHandler<SaveStudentDataCommand, Exception> saveStudentDataCommandHandler,
			IAsyncCommandHandler<SaveCourseDataCommand, Exception> saveCourseDataCommandHandler,
			IAsyncCommandHandler<SaveEnrollmentDataCommand, Exception> saveEnrollmentDataCommandHandler)
		{
			_getSchoolInitializationStatusQueryHandler = getSchoolInitializationStatusQueryHandler ?? throw new ArgumentNullException(nameof(getSchoolInitializationStatusQueryHandler));
			_saveStudentDataCommandHandler = saveStudentDataCommandHandler ?? throw new ArgumentNullException(nameof(saveStudentDataCommandHandler));
			_saveCourseDataCommandHandler = saveCourseDataCommandHandler ?? throw new ArgumentNullException(nameof(saveCourseDataCommandHandler));
			_saveEnrollmentDataCommandHandler = saveEnrollmentDataCommandHandler ?? throw new ArgumentNullException(nameof(saveEnrollmentDataCommandHandler));
		}

		public Task<Result<Unit, Exception>> InitializeAsync(CancellationToken cancellationToken = default)
		{
			return _getSchoolInitializationStatusQueryHandler.HandleAsync(new GetSchoolInitializationStatusQuery(), cancellationToken)
				.BindIfFalseAsync(async () =>
				{
					return await _saveStudentDataCommandHandler.HandleAsync(new SaveStudentDataCommand(CreateStudentSeedData()), cancellationToken)
						.BindAsync(_ => _saveCourseDataCommandHandler.HandleAsync(new SaveCourseDataCommand(CreateCourseSeedData()), cancellationToken))
						.BindAsync(_ => _saveEnrollmentDataCommandHandler.HandleAsync(new SaveEnrollmentDataCommand(CreateEnrollmentSeedData()), cancellationToken));
				});

			/*{
				
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
			});*/
		}

		private static Student[] CreateStudentSeedData()
		{
			return new Student[] { };
		}

		private static Course[] CreateCourseSeedData()
		{
			return new Course[] { };
		}

		private static Enrollment[] CreateEnrollmentSeedData()
		{
			return new Enrollment[] { };
		}
	}
}
