using MediatR;
using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.Application.Feature.Categories.Commands.Request
{
	public record class DeleteCategoryRequest(int catId) : IRequest<CategoryQuery>;
}
