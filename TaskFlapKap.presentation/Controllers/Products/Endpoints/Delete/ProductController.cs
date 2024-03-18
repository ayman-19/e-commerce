using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Products.Commands.Request;

namespace TaskFlapKap.presentation.Controllers.Products.Endpoints
{
	public partial class ProductController : ControllerBase
	{
		[HttpDelete("{id}")]
		[Authorize(Roles = "Seller")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var response = await mediator.Send(new DeleteProductRequest(id));
			return Ok(response);
		}
	}
}
