using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using TaskFlapKap.Application.Feature.Categories.Queries.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.Application.Feature.Categories.Queries.Handler
{
	public class dto
	{
		public string CategoryName { get; set; }
		public string ProductName { get; set; }
	}


	public class Ressult
	{
		public string CategoryName { get; set; }
		public List<string> Products { get; set; }
	}
	public class GetAllCategoryHandler : IRequestHandler<GetAllCategoryRequest, List<CategoryQuery>>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;

		public GetAllCategoryHandler(IUnitOfWork dbContext, IMapper mapper, IConfiguration configuration)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_configuration = configuration;
		}
		public async Task<List<CategoryQuery>> Handle(GetAllCategoryRequest request, CancellationToken cancellationToken)
		{

			try
			{
				var categories = await _dbContext.categories.GetAllAsync(includes: ["Products.User"], astracking: false);
				return _mapper.Map<List<CategoryQuery>>(categories);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
