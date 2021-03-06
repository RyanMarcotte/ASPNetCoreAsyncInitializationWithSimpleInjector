﻿using System;
using System.Threading.Tasks;
using Functional;

// ReSharper disable once CheckNamespace
namespace AsyncInitializationWithSimpleInjectorDemo.Initialization.Steps
{
	internal static class InitializationStatusCheckResultExtensions
	{
		public static Task<Result<Unit, Exception>> BindIfFalseAsync(this Task<Result<bool, Exception>> source, Func<Task<Result<Unit, Exception>>> bind)
		{
			return source.BindAsync(isInitialized => !isInitialized ? bind.Invoke() : Task.FromResult(Result.Unit<Exception>()));
		}
	}
}
