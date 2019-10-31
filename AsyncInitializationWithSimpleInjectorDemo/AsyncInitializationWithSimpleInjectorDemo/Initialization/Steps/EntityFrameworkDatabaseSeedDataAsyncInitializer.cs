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
		}

		private static Student[] CreateStudentSeedData()
		{
			return Array.Empty<Student>();
		}

		private static Course[] CreateCourseSeedData()
		{
			return new Course[]
			{
				new Course(1050, "Chemistry", 3),
				new Course(4022, "Microeconomics", 3),
				new Course(4041, "Macroeconomics", 3),
				new Course(1045, "Calculus", 4),
				new Course(3141, "Trigonometry", 4),
				new Course(2021, "Composition", 3),
				new Course(2042, "Literature", 4)
			};
		}

		private static Enrollment[] CreateEnrollmentSeedData()
		{
			return Array.Empty<Enrollment>();
		}
	}
}
