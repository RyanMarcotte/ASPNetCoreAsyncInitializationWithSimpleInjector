using System.Diagnostics;
using System.Reflection;

namespace AsyncInitializationWithSimpleInjectorDemo.Controllers.Models
{
	public class ApplicationMetadataDTO
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationMetadataDTO"/> class.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		public ApplicationMetadataDTO(Assembly assembly)
		{
			ApplicationVersion = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
		}

		/// <summary>
		/// Gets the application version.
		/// </summary>
		public string ApplicationVersion { get; }
	}
}