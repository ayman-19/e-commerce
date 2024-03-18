using MediatR;
using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.Application.Feature.Categories.Queries.Request
{
	public record class GetAllCategoryRequest : IRequest<List<CategoryQuery>>;
}
