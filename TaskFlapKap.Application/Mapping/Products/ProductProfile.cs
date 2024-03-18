using AutoMapper;
using TaskFlapKap.DataTransfareObject.Product;
using TaskFlapKap.Domain.Model;

namespace TaskFlapKap.Application.Mapping.Products
{
	internal class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductResponse>().ForMember(p => p.SellerName, op => op.MapFrom(d => d.User!.UserName)).ForMember(res => res.ProductName, op => op.MapFrom(des => des.Localize(des.ArabicName, des.EnglishName))).ReverseMap();

			CreateMap<Product, Productdto>().ReverseMap();
			CreateMap<Product, UpdateProductdto>().ReverseMap();


		}
	}
}
