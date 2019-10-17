using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Functional;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	public class RootInitializer
	{
		private readonly IEnumerable<IAsyncInitializer> _initializerCollection;

		public RootInitializer(IEnumerable<IAsyncInitializer> initializerCollection)
		{
			_initializerCollection = initializerCollection ?? throw new ArgumentNullException(nameof(initializerCollection));
		}

		public async Task InitializeAsync()
		{
			foreach (var initializer in _initializerCollection)
			{
				await initializer.InitializeAsync(CancellationToken.None).Match(
					_ => _,
					exception => throw new Exception($"An exception occurred during initialization ({initializer.GetType().FullName})!", exception));
			}
		}
	}
}
