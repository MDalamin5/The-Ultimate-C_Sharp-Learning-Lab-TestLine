using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEcommerceWebApi.DTOs;

namespace TEcommerceWebApi.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryReadDto> GetAllCategory();
        CategoryReadDto? GetCategoryById(Guid categoryId);
        CategoryReadDto CreateCategory(CategoryCreateDto categoryData);
        CategoryReadDto? UpdateCategory(Guid categoryId, CategoryUpdateDto categoryData);
        bool DeleteCategoryById(Guid categoryId);
    }
}