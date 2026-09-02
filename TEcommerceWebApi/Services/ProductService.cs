using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TEcommerceWebApi.Controllers;
using TEcommerceWebApi.data;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Helpers;
using TEcommerceWebApi.Interfaces;
using TEcommerceWebApi.Models;
using TEcommerceWebApi.Enums;

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

        public async Task<PaginatedResult<ProductReadDto>> GetAllProducts(QueryParameters queryParameter)
        {
            IQueryable<Product>? query = _appDbContext.Products.AsNoTracking().Include(p => p.Category).AsQueryable();

            // Searching Performing
            if (!string.IsNullOrWhiteSpace(queryParameter.SearchValue))
            {
               
                var formattedSearch = $"%{queryParameter.SearchValue.Trim()}%";

                query = query.Where(p => EF.Functions.ILike(p.Name, formattedSearch) || EF.Functions.ILike(p.Category.Name, formattedSearch));
            
            }
            // start to implement the product sorting
            if (!string.IsNullOrWhiteSpace(queryParameter.SortOrder))
            {
                var formattedSortOrder = queryParameter.SortOrder.Trim();

                // 1. Parse into enum variable 'parsedSortOrder'
                if (Enum.TryParse<SortOrder>(formattedSortOrder, true, out var parsedSortOrder))
                {
                    // 2. Switch on the parsed enum 👇
                    switch (parsedSortOrder)
                    {
                        case SortOrder.NameAsc:
                            query = query.OrderBy(p => p.Name);
                            break;

                        case SortOrder.NameDesc:
                            query = query.OrderByDescending(p => p.Name);
                            break;

                        default:
                            query = query.OrderBy(p => p.Name);
                            break;
                    }
                }
                else
                {
                    // If user sends invalid text like ?sortOrder=invalidText
                    query = query.OrderBy(p => p.Name);
                }
            }
            else
            {
                // Default sorting if queryParameter.SortOrder is null
                query = query.OrderBy(p => p.Name);
            }

            var totalCount = await query.CountAsync();

            var items = await query
            .Skip((queryParameter.PageNumber -1) * queryParameter.PageSize).Take(queryParameter.PageSize)
            .Select(p => new ProductReadDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : string.Empty
            }).ToListAsync();

            return new PaginatedResult<ProductReadDto>{
                Items = items,
                TotalCount = totalCount,
                PageNumber = queryParameter.PageNumber,
                PageSize = queryParameter.PageSize
            };
            // Direct projection using Select: Generates optimal SQL INNER JOIN
            // return await _appDbContext.Products
            //     .AsNoTracking()
            //     .Select(p => new ProductReadDto
            //     {
            //         ProductId = p.ProductId,
            //         Name = p.Name,
            //         Price = p.Price,
            //         CategoryId = p.CategoryId,
            //         CategoryName = p.Category != null ? p.Category.Name : string.Empty
            //     })
            //     .ToListAsync();
        }
    }
}