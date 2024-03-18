using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Domain.Model;
using TaskFlapKap.Presistance.DbContext;

namespace TaskFlapKap.Presistance.Repositories
{
	public class CategoryInventoryRepository : Repository<CategoryInventory>, ICategoryInventoryRepository
	{
		private readonly DbSet<CategoryInventory> _repository;
		public CategoryInventoryRepository(ApplicationDbContext context) : base(context)
		{
			_repository = context.Set<CategoryInventory>();
		}
		public async Task<bool> NameIsExist(Expression<Func<CategoryInventory, bool>> predicate)
			=> await _repository.Include(c => c.Category).AnyAsync(predicate);
	}
}
