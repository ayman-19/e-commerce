using MediatR;
using TaskFlapKap.Application.Pagination;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.Application.Feather.Products.Queries.Request
{
	public record class GatAllProductRequest(ProuctPaginateRequest dto) : IRequest<ResultPaginate<ProductResponse>>;
}
