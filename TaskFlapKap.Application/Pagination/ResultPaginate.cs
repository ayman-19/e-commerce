namespace TaskFlapKap.Application.Pagination
{
	public class ResultPaginate<T>
	{
		public ResultPaginate(List<T> date, int count, int numberPage, int pageSize)
		{
			Data = date;
			CurrentPage = numberPage;
			PageSize = pageSize;
			TotalCountPage = (int)Math.Ceiling(count / (double)pageSize);
		}
		public List<T> Data { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalCountPage { get; set; }
		public bool PreviosPage => CurrentPage > 1;
	}
}
