using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Products.Commands.Request;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.presentation.Controllers.Products.Endpoints
{
	public partial class ProductController : ControllerBase
	{
		[HttpPut("{id}")]
		[Authorize(Roles = "Seller")]
		public async Task<IActionResult> Edit(int id, UpdateProductdto update)
		{
			var response = await mediator.Send(new UpdateProductRequest(id, update));
			return Ok(response);
		}
	}
}
