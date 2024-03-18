using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Users.Commands.Request;

namespace TaskFlapKap.presentation.Controllers.Users.Endpoints
{
	public partial class UserController : ControllerBase
	{
		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> Delete(string id)
		{
			var response = await mediator.Send(new DeleteUserRequest(id));
			return Ok(response);
		}
	}
}
