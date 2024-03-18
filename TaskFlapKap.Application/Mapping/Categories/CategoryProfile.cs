using AutoMapper;
using TaskFlapKap.DataTransfareObject.Category;
using TaskFlapKap.Domain.Model;

namespace TaskFlapKap.Application.Mapping.Categories
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<CategoryCommand, Category>().ReverseMap();
			CreateMap<UpdateCategoryCommand, Category>().ReverseMap();
			CreateMap<Category, CategoryQuery>().ForMember(des => des.Name, op => op.MapFrom(src => src.Localize(src.ArabicName, src.EnglishName)))
			.ReverseMap();
			CreateMap<CategoryInventory, CategoryQuery>()
				.ForMember(des => des.Name, op => op.MapFrom(src => src.Category.Localize(src.Category.ArabicName, src.Category.EnglishName)))


				.ForMember(des => des.Id, op => op.MapFrom(src => src.Category.Id))


				.ForMember(destinationMember: des => des.Description, op => op.MapFrom(src => src.Category.Description))


				.ForMember(des => des.Products, op => op.MapFrom(src => src.Category.Products));
		}
	}
}
