using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.DataTransfareObject.Category
{
	public class CategoryQuery
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<ProductResponse> Products { get; set; }
	}
}
