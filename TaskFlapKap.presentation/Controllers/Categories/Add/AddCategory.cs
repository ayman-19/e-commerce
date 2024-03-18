using FastEndpoints;
using MediatR;
using TaskFlapKap.Application.Feature.Categories.Commands.Request;
using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.presentation.Controllers.Categories.Add
{
	public class AddCategory : Endpoint<CategoryCommand, CategoryQuery>
	{
		public override void Configure()
		{
			Post("AddCategory");
			Roles("Seller");
		}
		public override async Task HandleAsync(CategoryCommand req, CancellationToken ct)
		{
			Response = await Resolve<ISender>().Send(new AddCategoryRequest(req));
		}
	}
}
