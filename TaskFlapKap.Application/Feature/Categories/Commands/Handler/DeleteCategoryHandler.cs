using AutoMapper;
using MediatR;
using TaskFlapKap.Application.Feature.Categories.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.Application.Feature.Categories.Commands.Handler
{
	public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequest, CategoryQuery>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IMapper _mapper;

		public DeleteCategoryHandler(IUnitOfWork dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}
		public async Task<CategoryQuery> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var category = await _dbContext.categories.GetAsync(c => c.Id == request.catId, astracking: false);
				await _dbContext.categories.Delete(category);
				await _dbContext.SaveChangesAsync();
				await _dbContext.CommitAsync();
				return _mapper.Map<CategoryQuery>(category);
			}
			catch (Exception ex)
			{
				await _dbContext.RollbackAsync();
				throw new Exception(ex.Message);
			}
		}
	}
}
