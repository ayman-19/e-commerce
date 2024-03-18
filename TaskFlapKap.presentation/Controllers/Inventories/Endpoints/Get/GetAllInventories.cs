using FastEndpoints;
using MediatR;
using TaskFlapKap.Application.Feature.Inventories.Queries.Request;
using TaskFlapKap.DataTransfareObject.Inventories;

namespace TaskFlapKap.presentation.Controllers.Inventories.Endpoints.Get
{
	public class GetAllInventories : Endpoint<EmptyRequest, List<InventoryQuery>>
	{
		public override void Configure()
		{
			Get("GetAllInventory");
			Roles("Seller");
		}
		public override async Task HandleAsync(EmptyRequest req, CancellationToken ct) =>
			 Response = await Resolve<ISender>().Send(new GetAllInventoriesRequest());
	}
}
