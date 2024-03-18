using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Users.Commands.Request;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.presentation.Controllers.Users.Endpoints
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public partial class UserController : ControllerBase
	{
		private readonly IMediator mediator;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public UserController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
		{
			this.mediator = mediator;
			_httpContextAccessor = httpContextAccessor;
		}

		[HttpPost]
		public async Task<IActionResult> Register(Register register)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var response = await mediator.Send(new CreateUserRequest(register));
			return Ok(response);
		}
	}
}
