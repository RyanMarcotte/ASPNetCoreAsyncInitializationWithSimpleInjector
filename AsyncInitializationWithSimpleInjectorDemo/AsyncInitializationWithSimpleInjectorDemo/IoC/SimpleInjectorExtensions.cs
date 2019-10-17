using Microsoft.AspNetCore.Builder;
using SimpleInjector;

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
			container.RegisterAllFunctionalCQSHandlers(Lifestyle.Singleton, typeof(Startup).Assembly);
			container.Verify();
		}
	}
}
