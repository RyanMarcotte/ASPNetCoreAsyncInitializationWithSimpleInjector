using Microsoft.EntityFrameworkCore;

namespace AISIDemo.EntityFramework.Infrastructure
{
	public interface IDbContextFactory<out T> where T : DbContext
	{
		T CreateContext();
	}
}
