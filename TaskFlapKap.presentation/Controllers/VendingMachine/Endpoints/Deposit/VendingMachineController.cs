using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.VendingMachine.Comands.Request;

namespace TaskFlapKap.presentation.Controllers.VendingMachine.Endpoints
{
	public partial class VendingMachineController : ControllerBase
	{

		[HttpGet("{deposit}")]
		[Authorize(Roles = "Buyer")]
		public async Task<IActionResult> Deposit(double deposit)
		{
			return Ok(await mediator.Send(new DepositRequest(deposit)));
		}
	}
}
