using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TEcommerceWebApi.Models;
using TEcommerceWebApi.DTOs;

namespace TEcommerceWebApi.Profiles
{
    public class CategoryProfile: Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryReadDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category,CategoryUpdateDto>().ReverseMap();
        }
    }
}