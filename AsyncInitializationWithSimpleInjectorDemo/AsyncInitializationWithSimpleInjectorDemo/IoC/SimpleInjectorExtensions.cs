using AISIDemo.School.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using System;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	internal static class SimpleInjectorExtensions
	{
		public static IApplicationBuilder UseSimpleInjectorWithApplicationComponents(this IApplicationBuilder app, Container container)
		{
			app.UseSimpleInjector(container);
			container.RegisterApplicationComponentsAndVerify();
			return app;
		}

		public static void RegisterApplicationComponentsAndVerify(this Container container)
		{
			container.RegisterSingleton<IDbContextFactory<SchoolContext>, SchoolContextFactory>();
			container.RegisterAllFunctionalCQSHandlers(Lifestyle.Singleton, typeof(Startup).Assembly, typeof(AssemblyMarker).Assembly);
			container.Verify();
		}

		// this exists to get around lifestyle mismatch / captive dependency problem (https://blog.ploeh.dk/2014/06/02/captive-dependency/)
		// 'DbContextOptions<PrintBinTagsContext> is registered with Microsoft container as 'async scoped', but this factory is registered as 'singleton'
		// direct dependency on container is fine since this is part of the application composition root (https://stackoverflow.com/questions/21905058/factory-interface-in-simple-injector)
		private class SchoolContextFactory : IDbContextFactory<SchoolContext>
		{
			private readonly Container _container;

			public SchoolContextFactory(Container container)
			{
				_container = container ?? throw new ArgumentNullException(nameof(container));
			}

			public SchoolContext CreateContext()
			{
				return new SchoolContext(_container.GetInstance<DbContextOptions<SchoolContext>>());
			}
		}
	}
}
