using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Users.Commands.Request;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.presentation.Controllers.Users.Endpoints
{
	public partial class UserController : ControllerBase
	{
		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> Edit(string id, UpdateUserdto user)
		{
			var response = await mediator.Send(new UpdateUserRequest(id, user));
			return Ok(response);
		}
	}
}
