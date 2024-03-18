using Microsoft.EntityFrameworkCore;

namespace TaskFlapKap.Application.Pagination
{
	public static class ProccesPaginate
	{
		public static async Task<ResultPaginate<T>> ToPaginated<T>(this IQueryable<T> query, int pageNumber, int pagesize) where T : class
		{
			if (query is null)
				throw new ArgumentNullException("List Is Empty!");
			pageNumber = pageNumber == 0 ? 1 : pageNumber;
			pagesize = pagesize == 0 ? 10 : pagesize;
			int count = await query.AsNoTracking().CountAsync();
			if (count == 0) return new(new List<T>(), count, pageNumber, pagesize);
			var result = query.Skip((pageNumber - 1) * pagesize).Take(pagesize).ToList();
			return new(result, count, pageNumber, pagesize);
		}
	}
}
