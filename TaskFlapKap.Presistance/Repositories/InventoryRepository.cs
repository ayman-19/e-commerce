using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Domain.Model;
using TaskFlapKap.Presistance.DbContext;

namespace TaskFlapKap.Presistance.Repositories
{
	public class InventoryRepository : Repository<Inventory>, IInventoryRepository
	{
		public InventoryRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
