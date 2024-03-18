using Microsoft.EntityFrameworkCore;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Domain.Model;
using TaskFlapKap.Presistance.DbContext;

namespace TaskFlapKap.Presistance.Repositories
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly ApplicationDbContext context;

		public ProductRepository(ApplicationDbContext context) : base(context)
		{
			this.context = context;
		}
		public Task<double> GetCostProductById(int id)
		{
			return Task.FromResult(context.Products.Where(p => p.Id == id)
				.AsQueryable().Select(p => p.Cost).FirstOrDefault());
		}
		public bool InValidProduct(Func<Product, bool> predicate)
		{
			return context.Products.Any(predicate);
		}

		public IQueryable<Product> GetPaginated(string search = null!, string[] includes = default!)
		{
			var src = _entities.AsNoTracking().Where(p => p.EnglishName.Contains(search)).AsQueryable();
			if (includes is not null)
				foreach (var include in includes)
					src = src.Include(include);
			return src;
		}
	}
}
