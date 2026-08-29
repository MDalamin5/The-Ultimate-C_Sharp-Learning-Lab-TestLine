using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TEcommerceWebApi.data;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Interfaces;
using TEcommerceWebApi.Models;

namespace TEcommerceWebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        // Injected AppDbContext and AutoMapper
        public ProductService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ProductReadDto?> CreateProduct(ProductCreateDto productData)
        {
            // 1. Verify category exists in the database
            var category = await _appDbContext.Categories.FindAsync(productData.CategoryId);
            if (category == null)
            {
                return null;
            }

            // 2. Map DTO to Entity using AutoMapper
            var newProduct = _mapper.Map<Product>(productData);
            newProduct.ProductId = Guid.NewGuid();

            // 3. Save to database
            await _appDbContext.Products.AddAsync(newProduct);
            await _appDbContext.SaveChangesAsync();

            // 4. Map Entity to ReadDto and attach the category name
            var responseDto = _mapper.Map<ProductReadDto>(newProduct);
            responseDto.CategoryName = category.Name;

            return responseDto;
        }

        public async Task<List<ProductReadDto>> GetAllProducts()
        {
            // Direct projection using Select: Generates optimal SQL INNER JOIN
            return await _appDbContext.Products
                .AsNoTracking()
                .Select(p => new ProductReadDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.Name : string.Empty
                })
                .ToListAsync();
        }
    }
}