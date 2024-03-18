using FastEndpoints;
using MediatR;
using TaskFlapKap.Application.Feature.Inventories.Commands.Request;
using TaskFlapKap.DataTransfareObject.Inventories;

namespace TaskFlapKap.presentation.Controllers.Inventories.Endpoints.Delete
{
	public class DeleteInventory : Endpoint<EmptyRequest, InventoryQuery>
	{
		public override void Configure()
		{
			Delete("DeleteInventory/{id}");
			Roles("Seller");
		}
		public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
		{
			var id = Route<int>("id");
			Response = await Resolve<ISender>().Send(new DeleteInventoryRequest(id));
		}
	}
}
