using AutoMapper;
using MediatR;
using TaskFlapKap.Application.Feature.Inventories.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Inventories;

namespace TaskFlapKap.Application.Feature.Inventories.Commands.Handler
{
	public class UpdateInventoryHandler : IRequestHandler<UpdateInventoryRequest, InventoryQuery>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IMapper _mapper;

		public UpdateInventoryHandler(IUnitOfWork dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<InventoryQuery> Handle(UpdateInventoryRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var inv = await _dbContext.Inventories.GetAsync(inv => inv.Id == request.id
				, includes: ["CategoryInventories.Category.Products.User"]);
				var inventory = _mapper.Map(request.Command, inv);
				await _dbContext.Inventories.Update(inv);
				await _dbContext.SaveChangesAsync();
				await _dbContext.CommitAsync();
				return _mapper.Map<InventoryQuery>(inventory);
			}
			catch (Exception ex)
			{
				await _dbContext.RollbackAsync();
				throw new Exception(ex.Message);
			}
		}
	}
}
