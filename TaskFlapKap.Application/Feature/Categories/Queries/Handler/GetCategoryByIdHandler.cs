using AutoMapper;
using MediatR;
using TaskFlapKap.Application.Feature.Categories.Queries.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.Application.Feature.Categories.Queries.Handler
{
	public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdRequest, CategoryQuery>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IMapper _mapper;

		public GetCategoryByIdHandler(IUnitOfWork dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<CategoryQuery> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var category = await _dbContext.categories.GetAsync(c => c.Id == request.catId, ["Products.User"], false);
				return _mapper.Map<CategoryQuery>(category);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
