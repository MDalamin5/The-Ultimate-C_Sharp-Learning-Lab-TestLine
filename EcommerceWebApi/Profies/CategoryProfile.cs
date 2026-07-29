using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceWebApi.Models;
using EcommerceWebApi.DTOs;

namespace EcommerceWebApi.Profies
{
    public class CategoryProfile: Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}