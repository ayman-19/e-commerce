using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.VendingMachine.Comands.Request;
using TaskFlapKap.Application.Filters;

namespace TaskFlapKap.presentation.Controllers.VendingMachine.Endpoints
{
	public partial class VendingMachineController : ControllerBase
	{
		[HttpGet]
		//[Authorize(Roles = "Buyer")]
		[ServiceFilter(typeof(Filter))]
		public async Task<IActionResult> Rest()
		{
			return Ok(await mediator.Send(new ResetRequest()));
		}
	}
}
