# Asynchronous Initialization + Simple Injector for ASP.NET Core Web APIs

## Summary

This is a demo application built using .NET Core using Simple Injector as its dependency injection container to accompandy [this blog post](https://creativeknowledgepool.wordpress.com/?p=1743).  The application is configured to perform Entity Framework database migration and data seeding during startup in an asynchronous fashion.  It is based on [Microsoft's Entity Framework tutorial](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application) and code from the [AspNetCore.AsyncInitialization](https://github.com/thomaslevesque/AspNetCore.AsyncInitialization) library.  The application uses [Functional](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application) and [Functional.CQS](https://github.com/RyanMarcotte/Functional.CQS) libraries for functional programming patterns and applying command-query separation, respectively.

## Details

See [Startup.cs](src\AsyncInitializationWithSimpleInjectorDemo\Startup.cs) and [Program.cs](src\AsyncInitializationWithSimpleInjectorDemo\Program.cs).
