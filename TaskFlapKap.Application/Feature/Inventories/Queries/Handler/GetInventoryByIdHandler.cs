using AutoMapper;
using MediatR;
using TaskFlapKap.Application.Feature.Inventories.Queries.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Inventories;

namespace TaskFlapKap.Application.Feature.Inventories.Queries.Handler
{
	public class GetInventoryByIdHandler : IRequestHandler<GetInventoryByIdRequest, InventoryQuery>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IMapper _mapper;

		public GetInventoryByIdHandler(IUnitOfWork dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}
		public async Task<InventoryQuery> Handle(GetInventoryByIdRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var inventory = await _dbContext.Inventories.GetAsync(inv => inv.Id == request.invId, includes: ["CategoryInventories.Category.Products.User"]);
				return _mapper.Map<InventoryQuery>(inventory);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
