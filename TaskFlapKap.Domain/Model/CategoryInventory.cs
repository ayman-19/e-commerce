using Microsoft.EntityFrameworkCore;
using TaskFlapKap.Domain.Localization;

namespace TaskFlapKap.Domain.Model
{
	[PrimaryKey(nameof(InventoryId), nameof(CategoryId))]
	public class CategoryInventory : GeneralLocalize
	{
		public int InventoryId { get; set; }
		public int CategoryId { get; set; }
		public Category? Category { get; set; }
		public Inventory? Inventory { get; set; }
	}
}
