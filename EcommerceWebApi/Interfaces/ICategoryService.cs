using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceWebApi.DTOs;
using EcommerceWebApi.Controllers;

namespace EcommerceWebApi.Interfaces
{
    public interface ICategoryService
    {
        Task<PaginatedRecord<CategoryReadDto>> GetAllCategories(int PageNumber, int PageSize, string? search = null);
        Task<CategoryReadDto?> GetCategoryById(Guid categoryId);
        Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryData);
        Task<CategoryReadDto?> UpdateCategory(Guid categoryId, CategoryUpdateDto categoryData);
        Task<bool> DeleteCategoryById(Guid categoryId);
    }
}