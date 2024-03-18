using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Products.Queries.Request;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.presentation.Controllers.Products.Endpoints
{
	public partial class ProductController : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAllProduct([FromQuery] ProuctPaginateRequest dto)
		{
			var response = await mediator.Send(new GatAllProductRequest(dto));
			return Ok(response);
		}
	}
}
