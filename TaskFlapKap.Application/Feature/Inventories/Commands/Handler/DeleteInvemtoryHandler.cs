using AutoMapper;
using MediatR;
using TaskFlapKap.Application.Feature.Inventories.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Inventories;

namespace TaskFlapKap.Application.Feature.Inventories.Commands.Handler
{
	public class DeleteInvemtoryHandler : IRequestHandler<DeleteInventoryRequest, InventoryQuery>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IMapper _mapper;

		public DeleteInvemtoryHandler(IUnitOfWork dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<InventoryQuery> Handle(DeleteInventoryRequest request, CancellationToken cancellationToken)
		{

			var deleteInv = await _dbContext.Inventories.GetAsync(inv => inv.Id == request.invId, astracking: false, includes: ["CategoryInventories.Category.Products.User"]);
			try
			{
				await _dbContext.Inventories.Delete(deleteInv);
				await _dbContext.SaveChangesAsync();
				await _dbContext.CommitAsync();
				var res = _mapper.Map<InventoryQuery>(deleteInv);
				return res;
			}
			catch (Exception ex)
			{
				await _dbContext.RollbackAsync();
				throw new Exception(ex.Message);
			}
		}
	}
}
