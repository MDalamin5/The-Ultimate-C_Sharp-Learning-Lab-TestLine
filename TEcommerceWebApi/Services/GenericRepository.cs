using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TEcommerceWebApi.Controllers;
using TEcommerceWebApi.data;
using TEcommerceWebApi.Helpers;
using TEcommerceWebApi.Interfaces;

namespace TEcommerceWebApi.Services
{
    public class GenericRepository<TEntity>: IGenericRepository<TEntity>  where TEntity: class
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _dbSet = appDbContext.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
           
           return await _dbSet.ToListAsync();

        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var dbObj = await _dbSet.FindAsync(id);
            if(dbObj == null)
                return null;

            return _mapper.Map<TEntity>(dbObj);
        }

        public async Task CreateAsync(TEntity model)
        {
            var newObj = _mapper.Map<TEntity>(model);

            await _dbSet.AddAsync(newObj);
            await _appDbContext.SaveChangesAsync();

            //return via mapper
            _mapper.Map<TEntity>(newObj);
        }


        public async Task UpdateAsync(TEntity model)
        {
            _dbSet.Update(model);
            await _appDbContext.SaveChangesAsync();
                        
        }

        public async Task DeleteAsync(TEntity model)
        {
            _dbSet.Remove(model);
            await _appDbContext.SaveChangesAsync();
            // return true;
        }
    }
}