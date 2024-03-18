using TaskFlapKap.Domain.Localization;
namespace TaskFlapKap.Domain.Model
{
	public class Category : GeneralLocalize
	{
		public int Id { get; set; }
		public string ArabicName { get; set; }
		public string EnglishName { get; set; }
		public string Description { get; set; }
		public List<Product>? Products { get; set; }
		public List<CategoryInventory>? CategoryInventories { get; set; }
		public Category()
		{
			Products = new List<Product>();
			CategoryInventories = new List<CategoryInventory>();
		}
	}

}
