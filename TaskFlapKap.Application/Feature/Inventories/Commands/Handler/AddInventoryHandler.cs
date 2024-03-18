using AutoMapper;
using MediatR;
using TaskFlapKap.Application.Feature.Inventories.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Inventories;
using TaskFlapKap.Domain.Model;

namespace TaskFlapKap.Application.Feature.Inventories.Commands.Handler
{
	public class AddInventoryHandler : IRequestHandler<AddInventoryRequest, InventoryQuery>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly IMapper _mapper;

		public AddInventoryHandler(IUnitOfWork dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<InventoryQuery> Handle(AddInventoryRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var inv = _mapper.Map<Inventory>(request.command);
				await _dbContext.Inventories.AddAsync(inv);
				await _dbContext.SaveChangesAsync();
				await _dbContext.CommitAsync();
				var response = _mapper.Map<InventoryQuery>(inv);
				return response;
			}
			catch (Exception ex)
			{
				await _dbContext.RollbackAsync();
				throw new Exception(ex.Message);
			}
		}
	}
}
