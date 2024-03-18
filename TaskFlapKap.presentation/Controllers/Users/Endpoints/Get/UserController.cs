using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Users.Queries.Request;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.presentation.Controllers.Users.Endpoints
{

	public partial class UserController : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Login(Login login)
		{
			var response = await mediator.Send(new LoginRequest(login));
			return Ok(response);
		}
	}
}
