using FastEndpoints;
using MediatR;
using TaskFlapKap.Application.Feature.Categories.Queries.Request;
using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.presentation.Controllers.Categories.Get
{
	public class GetCategoryByID : Endpoint<EmptyRequest, CategoryQuery>
	{
		public override void Configure()
		{
			Get("GetCategory/{id}");
			Roles("Seller");
		}
		public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
		{
			int id = Route<int>("id");
			Response = await Resolve<ISender>().Send(new GetCategoryByIdRequest(id));
		}
	}
}
