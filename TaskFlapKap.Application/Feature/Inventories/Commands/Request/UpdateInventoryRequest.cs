using MediatR;
using TaskFlapKap.DataTransfareObject.Inventories;

namespace TaskFlapKap.Application.Feature.Inventories.Commands.Request
{
	public record class UpdateInventoryRequest(int id, InventoryCommand Command) : IRequest<InventoryQuery>;
}
