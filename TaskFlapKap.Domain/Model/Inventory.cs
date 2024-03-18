using TaskFlapKap.Domain.Localization;

namespace TaskFlapKap.Domain.Model
{
	public class Inventory : GeneralLocalize
	{
		public int Id { get; set; }
		public string ArabicName { get; set; }
		public string EnglishName { get; set; }
		public string Discription { get; set; }
		public List<CategoryInventory>? CategoryInventories { get; set; }
	}
}
