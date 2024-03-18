using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feature.Users.Commands.Request;

namespace TaskFlapKap.presentation.Controllers.Users.Endpoints
{
	public partial class UserController : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> ConfirmEmail([FromQuery] string code, [FromQuery] string userId)
		{

			//string request = _httpContextAccessor.HttpContext.Request.Query["code"];
			var response = await mediator.Send(new ConfirmEmailRequest(userId, code));
			return Ok(response);
		}
	}
}
