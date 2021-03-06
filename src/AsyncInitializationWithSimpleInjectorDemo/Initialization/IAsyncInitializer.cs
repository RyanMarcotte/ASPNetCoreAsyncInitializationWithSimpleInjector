﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Functional;

namespace AsyncInitializationWithSimpleInjectorDemo.Initialization
{
	public interface IAsyncInitializer
	{
		Task<Result<Unit, Exception>> InitializeAsync(CancellationToken cancellationToken = default);
	}
}
