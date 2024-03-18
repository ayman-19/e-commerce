using MediatR;
using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.Application.Feature.Categories.Commands.Request
{
	public record class AddCategoryRequest(CategoryCommand Command) : IRequest<CategoryQuery>;
}
