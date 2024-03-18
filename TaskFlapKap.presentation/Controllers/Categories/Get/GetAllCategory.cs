using FastEndpoints;
using MediatR;
using TaskFlapKap.Application.Feature.Categories.Queries.Request;
using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.presentation.Controllers.Categories.Get
{
	public class GetAllCategory : Endpoint<EmptyRequest, List<CategoryQuery>>
	{
		private readonly ISender _Sender;
		public GetAllCategory(ISender sender)
		{
			_Sender = sender;
		}
		public override void Configure()
		{
			Get("GetAllCategory");
			AllowAnonymous();
			Roles("Seller");
		}
		public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
		{
			Response = await Resolve<ISender>().Send(new GetAllCategoryRequest());
		}
	}
}
