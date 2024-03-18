using FastEndpoints;
using MediatR;
using TaskFlapKap.Application.Feature.Categories.Commands.Request;
using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.presentation.Controllers.Categories.Delete
{
	public class DeleteCategory : Endpoint<EmptyRequest, CategoryQuery>
	{
		public override void Configure()
		{
			Delete("DeleteCategory/{id}");
			Roles("Seller");
		}
		public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
		{
			int id = Route<int>("id");
			Response = await Resolve<ISender>().Send(new DeleteCategoryRequest(id));
		}
	}
}
