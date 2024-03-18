using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskFlapKap.Application.IAccountServices;

namespace TaskFlapKap.Application.Filters
{
	public class Filter : IAsyncActionFilter
	{
		private readonly IAccountService _service;
		public Filter(IAccountService service)
		{
			_service = service;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (context.HttpContext.User.Identity.IsAuthenticated)
			{
				var user = await _service.GetUserAsync();
				var roles = await _service.GetUserRolesAsync(user);
				if (roles.All(r => r != "Buyer"))
					context.Result = new ObjectResult("Forbidden!")
					{
						StatusCode = StatusCodes.Status403Forbidden
					};
				else
					await next();
			}
		}
	}
}
