using System;
using System.Threading;
using System.Threading.Tasks;
using Functional;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	public interface IAsyncInitializer
	{
		Task<Result<Unit, Exception>> InitializeAsync(CancellationToken cancellationToken = default);
	}
}
