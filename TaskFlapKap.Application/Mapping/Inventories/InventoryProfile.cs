using AutoMapper;
using TaskFlapKap.DataTransfareObject.Inventories;
using TaskFlapKap.Domain.Model;

namespace TaskFlapKap.Application.Mapping.Inventories
{
	public class InventoryProfile : Profile
	{
		public InventoryProfile()
		{
			CreateMap<Inventory, InventoryQuery>()
				.ForMember(des => des.Name, op => op.MapFrom(src => src.Localize(src.ArabicName, src.EnglishName)))
				.ForMember(des => des.Categories, op => op.MapFrom(src => src.CategoryInventories));
			CreateMap<InventoryCommand, Inventory>().ReverseMap();
		}
	}
}
