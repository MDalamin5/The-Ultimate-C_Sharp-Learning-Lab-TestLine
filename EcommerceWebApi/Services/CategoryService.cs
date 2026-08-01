using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceWebApi.data;
using EcommerceWebApi.DTOs;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Models;
using EcommerceWebApi.Profies;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApi.Services
{
    public class CategoryService: ICategoryService
    {
        // private static readonly List<Category> _categories = new List<Category>();
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }


        public async Task<List<CategoryReadDto>> GetAllCategories(int PageNumber, int PageSize)
        {
            // Before Mapping.
            // return _categories.Select(c => new CategoryReadDto
            // {
            //     CategoryId = c.CategoryId,
            //     Name = c.Name,
            //     Description = c.Description,
            //     CreatedAt = c.CreatedAt
            // }).ToList();

            //After Mapping. All category data map to CategoryReadDto and return.
            var categories = await _appDbContext.Categories.ToListAsync();
            return _mapper.Map<List<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto?> GetCategoryById(Guid categoryId)
        {
            var foundCategory = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            // if(foundCategory == null)
            //     return null;
            // else
            // {
                // var FoundCategory = new CategoryReadDto
                // {
                //     CategoryId = foundCategory.CategoryId,
                //     Name = foundCategory.Name,
                //     Description = foundCategory.Description,
                //     CreatedAt = foundCategory.CreatedAt
                // };
                // return FoundCategory;

            //     return _mapper.Map<CategoryReadDto>(foundCategory);
            // }

            return foundCategory == null ? null : _mapper.Map<CategoryReadDto>(foundCategory);
        }

        public async Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryData)
        {
            
            // var newCategory = new Category
            // {
            //     CategoryId = Guid.NewGuid(),
            //     Name = categoryData.Name,
            //     Description = categoryData.Description,
            //     CreatedAt = DateTime.UtcNow
            // };
            var newCategory = _mapper.Map<Category>(categoryData);
            newCategory.CategoryId = Guid.NewGuid();
            newCategory.CreatedAt = DateTime.UtcNow;

            await _appDbContext.Categories.AddAsync(newCategory);
            await _appDbContext.SaveChangesAsync();

            // var categoryReadDto = new CategoryReadDto
            // {
            //   CategoryId = newCategory.CategoryId,
            //   Name = newCategory.Name,
            //   Description = newCategory.Description,
            //   CreatedAt = newCategory.CreatedAt  
            // };
            // return categoryReadDto;

            return _mapper.Map<CategoryReadDto>(newCategory);
        }

        public async Task<CategoryReadDto?> UpdateCategory(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);

            if (foundCategory == null)
                return null;
            
            // foundCategory.Name = categoryData.Name;
            // foundCategory.Description = categoryData.Description;

            _mapper.Map(categoryData, foundCategory);
            _appDbContext.Categories.Update(foundCategory);
            await _appDbContext.SaveChangesAsync();
            
            // return new CategoryReadDto
            // {
            //     CategoryId = foundCategory.CategoryId,
            //     Name = foundCategory.Name,
            //     Description = foundCategory.Description,
            //     CreatedAt = foundCategory.CreatedAt
            // };

            return _mapper.Map<CategoryReadDto>(foundCategory);
        }

        public async Task<bool> DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);

            if (foundCategory == null)
                return false;

            _appDbContext.Categories.Remove(foundCategory);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}