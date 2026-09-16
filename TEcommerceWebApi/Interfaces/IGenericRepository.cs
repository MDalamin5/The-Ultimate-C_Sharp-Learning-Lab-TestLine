using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEcommerceWebApi.Controllers;
using TEcommerceWebApi.Helpers;


namespace TEcommerceWebApi.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        // Task<PaginatedResult<TEntity>> GetAllAsync(QueryParameters queryParameter);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task CreateAsync(TEntity model);
        Task UpdateAsync(TEntity model);
        Task DeleteAsync(TEntity model);
    }
}