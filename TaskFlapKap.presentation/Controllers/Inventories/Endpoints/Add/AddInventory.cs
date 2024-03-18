using FastEndpoints;
using MediatR;
using TaskFlapKap.Application.Feature.Inventories.Commands.Request;
using TaskFlapKap.DataTransfareObject.Inventories;

namespace TaskFlapKap.presentation.Controllers.Inventories.Endpoints.Add
{
	public class AddInventory : Endpoint<InventoryCommand, InventoryQuery>
	{
		public override void Configure()
		{
			Post("AddInvaentory");
			Roles("Seller");
		}
		public override async Task HandleAsync(InventoryCommand req, CancellationToken ct)
		{
			Response = await Resolve<ISender>().Send((new AddInventoryRequest(req)));
		}
	}
}
