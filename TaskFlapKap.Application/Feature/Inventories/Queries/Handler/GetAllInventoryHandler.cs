using AutoMapper;
using MediatR;
using TaskFlapKap.Application.Feature.Inventories.Queries.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Inventories;

namespace TaskFlapKap.Application.Feature.Inventories.Queries.Handler
{
	public class GetAllInventoryHandler : IRequestHandler<GetAllInventoriesRequest, List<InventoryQuery>>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IMapper _mapper;

		public GetAllInventoryHandler(IUnitOfWork dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}
		public async Task<List<InventoryQuery>> Handle(GetAllInventoriesRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var inventories = (await _dbContext.Inventories.GetAllAsync(includes: ["CategoryInventories.Category.Products.User"])).ToList();
				var result = _mapper.Map<List<InventoryQuery>>(inventories);
				return result;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
