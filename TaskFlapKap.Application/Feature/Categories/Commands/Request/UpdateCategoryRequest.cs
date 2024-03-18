using MediatR;
using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.Application.Feature.Categories.Commands.Request
{
	public record class UpdateCategoryRequest(int catId, UpdateCategoryCommand Command) : IRequest<CategoryQuery>;
}
