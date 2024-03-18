using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Domain.Model;
using TaskFlapKap.Presistance.DbContext;

namespace TaskFlapKap.Presistance.Repositories
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		public CategoryRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
