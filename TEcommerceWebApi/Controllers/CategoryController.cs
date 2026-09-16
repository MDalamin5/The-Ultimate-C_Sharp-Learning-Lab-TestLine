using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEcommerceWebApi.data;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Helpers;
using TEcommerceWebApi.Interfaces;
using TEcommerceWebApi.Models;

namespace TEcommerceWebApi.Controllers
{
    [ApiController]
    [Route("/api/v2/categories")]
    public class CategoryController: ControllerBase
    {
        public readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        
        public CategoryController(ICategoryService categoryService, IMapper mapper, AppDbContext appDbContext)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }


        // Read all categories
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] QueryParameters queryParameter)
        {
            queryParameter.Validate();
            // Data Binding With Read Dto
            var responseCategory = await _categoryService.GetAllAsync();

            return Ok(responseCategory);
        }

        //Read a category byId
        
        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid categoryId)
        {
            
            var responseCategory = await _categoryService.GetByIdAsync(categoryId);

            if(responseCategory == null)
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>{"Category not found with this id."}, 404, "Validation Invalid."));

            return Ok(responseCategory);
        }

        // Create Category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto model)
        {
            var dbObj = _mapper.Map<Category>(model);
            dbObj.CategoryId = Guid.NewGuid();
            dbObj.CreatedAt = DateTime.UtcNow;

            
            //Return Data followed by CategoryReadDto
            await _categoryService.CreateAsync(dbObj);

            return Ok();
        }


        // update a Category
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategoryById(Guid id, [FromBody] CategoryUpdateDto model)
        {
            //finding the category

            var foundCategory = await _categoryService.GetByIdAsync(id);

            _mapper.Map(model,foundCategory);

            await _categoryService.UpdateAsync(foundCategory);

            return Ok();
            
        }




        // delete category by ID
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategoryById(Guid id)
        {
            var dbObj = await _categoryService.GetByIdAsync(id);

            await _categoryService.DeleteAsync(dbObj);
            
            return Ok();
        }
    
    }
}