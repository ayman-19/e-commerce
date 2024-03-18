using AutoMapper;
using MediatR;
using TaskFlapKap.Application.Feature.Categories.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Category;
using TaskFlapKap.Domain.Model;

namespace TaskFlapKap.Application.Feature.Categories.Commands.Handler
{
	public class AddCategoryHandler : IRequestHandler<AddCategoryRequest, CategoryQuery>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IMapper _mapper;

		public AddCategoryHandler(IUnitOfWork dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<CategoryQuery> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var category = _mapper.Map<Category>(request.Command);
				category.CategoryInventories!.Add(new CategoryInventory { InventoryId = request.Command.InventoryId });
				await _dbContext.categories.AddAsync(category);
				await _dbContext.SaveChangesAsync();
				await _dbContext.CommitAsync();
				return _mapper.Map<CategoryQuery>(category);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
