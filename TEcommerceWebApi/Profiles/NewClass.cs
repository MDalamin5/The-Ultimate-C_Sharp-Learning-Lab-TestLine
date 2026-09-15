using AutoMapper;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Models;

namespace TEcommerceWebApi.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserReadDto>()
                .ForMember(dest => dest.TotalOrdersCount, 
                           opt => opt.MapFrom(src => src.Orders != null ? src.Orders.Count : 0));
        }
    }
}