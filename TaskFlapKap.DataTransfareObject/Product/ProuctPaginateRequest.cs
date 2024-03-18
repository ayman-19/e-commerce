namespace TaskFlapKap.DataTransfareObject.Product
{
	public class ProuctPaginateRequest
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string Search { get; set; } = "";
	}
}
