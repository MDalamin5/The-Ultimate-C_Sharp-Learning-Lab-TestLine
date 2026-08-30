using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Models;
using TEcommerceWebApi.Interfaces;
using TEcommerceWebApi.Profiles;
using AutoMapper;
using TEcommerceWebApi.data;
using Microsoft.EntityFrameworkCore;
using TEcommerceWebApi.Controllers;

namespace TEcommerceWebApi.Services
{
    public class CategoryService: ICategoryService
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public CategoryService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        // private static readonly List<Category> _categories = new List<Category>();
        public async Task<PaginatedResult<CategoryReadDto>> GetAllCategory(int pageNumber, int pageSize, string ?searchValue = null)
        {
            IQueryable<Category> ?query = _appDbContext.Categories;
            
            var totalCategory = await query.CountAsync();

            // Searching Performing
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                var formattedSearch = $"%{searchValue.Trim()}%";

                query = query.Where(c => EF.Functions.ILike(c.Name, formattedSearch) || EF.Functions.ILike(c.Description, formattedSearch));
            }

            var Items = await query.Skip((pageNumber - 1)*pageSize).Take(pageSize).ToListAsync();
            

            var result = _mapper.Map<List<CategoryReadDto>>(Items);

            // using without mapper.
            /*
            return categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();
            */
            return new PaginatedResult<CategoryReadDto>
            {
                Items = result,
                TotalCount = totalCategory,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

        }

        public async Task<CategoryReadDto?> GetCategoryById(Guid categoryId)
        {
            var foundCategory = await _appDbContext.Categories.FirstOrDefaultAsync(category => category.CategoryId == categoryId);
            if(foundCategory == null)
                return null;

            return _mapper.Map<CategoryReadDto>(foundCategory);

            // without using the Mapper.
            // return new CategoryReadDto
            //     {
            //         CategoryId = foundCategory.CategoryId,
            //         Name = foundCategory.Name,
            //         Description = foundCategory.Description,
            //         CreatedAt = foundCategory.CreatedAt
            //     };
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

            //return via mapper
            return _mapper.Map<CategoryReadDto>(newCategory);

            //Return Data followed by CategoryReadDto
            // return new CategoryReadDto
            // {
            //     CategoryId = newCategory.CategoryId,
            //     Name = newCategory.Name,
            //     Description = newCategory.Description,
            //     CreatedAt = newCategory.CreatedAt
            // };

        }


        public async Task<CategoryReadDto?> UpdateCategory(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = await _appDbContext.Categories.FirstOrDefaultAsync(category => category.CategoryId == categoryId);

            if(foundCategory == null)
                return null;
            
            
            //Assuming the Name is not empty and the descriptions must gater then 10 char.
            // foundCategory.Name = categoryData.Name;
            // foundCategory.Description = categoryData.Description;

            //using mapper categoryUpdateDto -> category
            _mapper.Map(categoryData, foundCategory);
            _appDbContext.Categories.Update(foundCategory);
            await _appDbContext.SaveChangesAsync();

            
            return _mapper.Map<CategoryReadDto>(foundCategory);
        }

        public async Task<bool> DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = await _appDbContext.Categories.FirstOrDefaultAsync(category => category.CategoryId == categoryId);
            if(foundCategory == null)
                return false;
            
            _appDbContext.Categories.Remove(foundCategory);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}