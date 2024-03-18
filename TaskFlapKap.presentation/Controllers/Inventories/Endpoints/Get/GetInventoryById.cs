using FastEndpoints;
using MediatR;
using TaskFlapKap.Application.Feature.Inventories.Queries.Request;
using TaskFlapKap.DataTransfareObject.Inventories;

namespace TaskFlapKap.presentation.Controllers.Inventories.Endpoints.Get
{
	public class GetInventoryById : Endpoint<EmptyRequest, InventoryQuery>
	{
		public override void Configure()
		{
			Get("GetInventorybyid/{id}");
			Roles("Seller");
		}
		public async override Task HandleAsync(EmptyRequest req, CancellationToken ct)
		{
			int id = Route<int>("id");
			Response = await Resolve<ISender>().Send(new GetInventoryByIdRequest(id));
		}
	}
}
