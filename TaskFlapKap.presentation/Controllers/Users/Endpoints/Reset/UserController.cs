using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feature.Users.Commands.Request;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.presentation.Controllers.Users.Endpoints
{
	public partial class UserController : ControllerBase
	{

		[HttpGet]
		public async Task<IActionResult> SendCodeToResetPassword(string email)
		{
			var response = await mediator.Send(new SendCodeResetPasswordRequest(email));
			return Ok(response);
		}
		[HttpGet]
		public async Task<IActionResult> ResetPassword(ResetPassworddto dto)
		{
			var response = await mediator.Send(new ResetPasswordRequest(dto));
			return Ok(response);
		}
	}
}
