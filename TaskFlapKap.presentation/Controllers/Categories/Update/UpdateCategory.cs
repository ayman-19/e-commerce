using FastEndpoints;
using MediatR;
using TaskFlapKap.Application.Feature.Categories.Commands.Request;
using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.presentation.Controllers.Categories.Update
{
	public class UpdateCategory : Endpoint<UpdateCategoryCommand, CategoryQuery>
	{
		public override void Configure()
		{
			Put("UpdateCategory/{id}");
			Roles("Seller");
		}
		public override async Task HandleAsync(UpdateCategoryCommand req, CancellationToken ct)
		{
			int id = Route<int>("id");
			Response = await Resolve<ISender>().Send(new UpdateCategoryRequest(id, req));
		}
	}
}
