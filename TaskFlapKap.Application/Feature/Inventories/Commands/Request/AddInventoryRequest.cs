using MediatR;
using TaskFlapKap.DataTransfareObject.Inventories;

namespace TaskFlapKap.Application.Feature.Inventories.Commands.Request
{
	public record class AddInventoryRequest(InventoryCommand command) : IRequest<InventoryQuery>;
}
