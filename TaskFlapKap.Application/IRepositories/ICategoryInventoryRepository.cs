using System.Linq.Expressions;
using TaskFlapKap.Domain.Model;

namespace TaskFlapKap.Application.IRepositories
{
	public interface ICategoryInventoryRepository : IRepository<CategoryInventory>
	{
		Task<bool> NameIsExist(Expression<Func<CategoryInventory, bool>> predicate);
	}
}
