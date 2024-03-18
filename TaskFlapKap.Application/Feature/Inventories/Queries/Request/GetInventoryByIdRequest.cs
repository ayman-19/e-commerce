using MediatR;
using TaskFlapKap.DataTransfareObject.Inventories;

namespace TaskFlapKap.Application.Feature.Inventories.Queries.Request
{
	public record class GetInventoryByIdRequest(int invId) : IRequest<InventoryQuery>;
}
