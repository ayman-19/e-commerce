using FastEndpoints;
using MediatR;
using TaskFlapKap.Application.Feature.Inventories.Commands.Request;
using TaskFlapKap.DataTransfareObject.Inventories;

namespace TaskFlapKap.presentation.Controllers.Inventories.Endpoints.Update
{
	public class UpdateInventory : Endpoint<InventoryCommand, InventoryQuery>
	{
		public override void Configure()
		{
			Put("UpdateInventory/{id}");
			Roles("Seller");
		}
		public override async Task HandleAsync(InventoryCommand req, CancellationToken ct)
		{
			var id = Route<int>("id");
			Response = await Resolve<ISender>().Send(new UpdateInventoryRequest(id, req));
		}
	}
}
