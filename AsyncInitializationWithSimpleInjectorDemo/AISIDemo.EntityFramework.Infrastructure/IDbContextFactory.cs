using Microsoft.EntityFrameworkCore;

namespace AsyncInitializationWithSimpleInjectorDemo
{
	public interface IDbContextFactory<out T> where T : DbContext
	{
		T CreateContext();
	}
}
