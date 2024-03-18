using AutoMapper;
using MediatR;
using TaskFlapKap.Application.Feature.Categories.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.Application.Feature.Categories.Commands.Handler
{
	public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, CategoryQuery>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IMapper _mapper;

		public UpdateCategoryHandler(IUnitOfWork dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}
		public async Task<CategoryQuery> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var category = await _dbContext.categories.GetAsync(c => c.Id == request.catId, astracking: false);
				var entity = _mapper.Map(request.Command, category);
				await _dbContext.categories.Update(entity);
				await _dbContext.SaveChangesAsync();
				await _dbContext.CommitAsync();
				return _mapper.Map<CategoryQuery>(entity);

			}
			catch (Exception ex)
			{
				await _dbContext.RollbackAsync();
				throw new Exception(ex.Message);
			}
		}
	}
}
