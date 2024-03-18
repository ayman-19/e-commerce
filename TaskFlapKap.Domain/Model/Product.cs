using System.ComponentModel.DataAnnotations.Schema;
using TaskFlapKap.Domain.Localization;

namespace TaskFlapKap.Domain.Model
{
	public class Product : GeneralLocalize
	{
		public int Id { get; set; }
		public int AmountAvailable { get; set; }
		public double Cost { get; set; }
		public string ArabicName { get; set; }
		public string EnglishName { get; set; }
		public int CategoryId { get; set; }
		public string SellerId { get; set; }
		[ForeignKey(nameof(SellerId))]
		public User? User { get; set; }
		[ForeignKey(nameof(CategoryId))]
		public Category? Category { get; set; }
	}
}
