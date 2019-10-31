using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Functional;

namespace AsyncInitializationWithSimpleInjectorDemo.Initialization
{
	public class RootInitializer
	{
		private readonly IEnumerable<IAsyncInitializer> _initializerCollection;

		public RootInitializer(IEnumerable<IAsyncInitializer> initializerCollection)
		{
			_initializerCollection = initializerCollection ?? throw new ArgumentNullException(nameof(initializerCollection));
		}

		public async Task InitializeAsync(CancellationToken cancellationToken)
		{
			foreach (var initializer in _initializerCollection)
			{
				cancellationToken.ThrowIfCancellationRequested();
				await initializer.InitializeAsync(cancellationToken).Match(
					_ => _,
					exception => throw new Exception($"An exception occurred during initialization ({initializer.GetType().FullName})!", exception));
			}
		}
	}
}
