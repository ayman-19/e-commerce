namespace TaskFlapKap.Application.IRepositories
{
	public interface IUnitOfWork : IDisposable
	{
		IUserRepository Users { get; }
		IInventoryRepository Inventories { get; }
		ICategoryRepository categories { get; }
		IProductRepository Products { get; }
		ICategoryInventoryRepository CategoryInventories { get; }
		Task CommitAsync();
		Task RollbackAsync();
		Task<int> SaveChangesAsync();
	}
}
