using System;
using System.Threading.Tasks;
using TEcommerceWebApi.DTOs;
using TEcommerceWebApi.Helpers;
using TEcommerceWebApi.Controllers;

namespace TEcommerceWebApi.Interfaces
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductReadDto>> GetAllProducts(QueryParameters queryParameters);
        Task<ProductReadDto?> GetProductByIdAsync(Guid productId);
        Task<ProductReadDto?> CreateProduct(ProductCreateDto productData);
        Task<ProductReadDto?> UpdateProductAsync(Guid productId, ProductUpdateDto updateData);
        Task<bool> DeleteProductAsync(Guid productId);
    }
}