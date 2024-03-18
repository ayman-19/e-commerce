using TaskFlapKap.DataTransfareObject.Category;

namespace TaskFlapKap.DataTransfareObject.Inventories
{
	public class InventoryQuery
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<CategoryQuery> Categories { get; set; }
	}
}
