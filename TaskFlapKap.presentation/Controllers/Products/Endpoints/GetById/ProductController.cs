using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Products.Queries.Request;

namespace TaskFlapKap.presentation.Controllers.Products.Endpoints
{
	public partial class ProductController : ControllerBase
	{
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			var response = await mediator.Send(new GetProductByIdRequest(id));
			return Ok(response);
		}
	}
}
