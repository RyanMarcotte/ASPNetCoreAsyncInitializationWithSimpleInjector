using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using AsyncInitializationWithSimpleInjectorDemo.Controllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsyncInitializationWithSimpleInjectorDemo.Controllers
{
	[ApiController]
	[Produces("application/json")]
	[Route("ping")]
	public class PingController : ControllerBase
	{
		[HttpGet]
		[ProducesResponseType(typeof(ApplicationMetadataDTO), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Ping() => await Task.FromResult(Ok(new ApplicationMetadataDTO(Assembly.GetExecutingAssembly())));
	}
}