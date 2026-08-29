using AutoMapper;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Models;

namespace TEcommerceWebApi.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // ProductCreateDto -> Product
            CreateMap<ProductCreateDto, Product>();

            // Product -> ProductReadDto
            CreateMap<Product, ProductReadDto>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));
        }
    }
}