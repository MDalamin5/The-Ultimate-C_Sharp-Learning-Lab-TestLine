using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrganizationManagement.Repository.IRepository;
using OrganizationManagement.Data;
using OrganizationManagement.DTOs.Designation;
using OrganizationManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace OrganizationManagement.Repository
{
    public class DesignationRepository: IDesignationRepository
        {
            private readonly AppDbContext _appDbContext;
            public DesignationRepository(AppDbContext appDbContext)
            {
                _appDbContext = appDbContext;
            }

            public async Task<bool> CreateDesignation(CreateDesignationDto model)
            {
                var data = new Designation
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name
                };

                Console.WriteLine($"Name: {data.Name}, Id: {data.Id}");
                
                await _appDbContext.Designations.AddAsync(data);
                await _appDbContext.SaveChangesAsync();

                return true;
            }

            public async Task<List<ReadDesignationDto>> getAllDesignation()
            {
                var allDeg = await _appDbContext.Designations.Select(d => new ReadDesignationDto
                {
                    Name = d.Name
                }).ToListAsync();

                return allDeg;
            }

            public async Task<ReadDesignationDto> getDesignationById(Guid id)
            {
                var dbObj = await _appDbContext.Designations.FirstOrDefaultAsync(d => d.Id == id);

                return new ReadDesignationDto
                {
                    Name = dbObj.Name
                };
            }

            public async Task<bool> updateDesignationById(Guid id, UpdateDesignationDto model)
            {
                var dbObj = await _appDbContext.Designations.FirstOrDefaultAsync(d => d.Id == id);
                dbObj.Name = model.Name;
                await _appDbContext.SaveChangesAsync();

                return true;
            }

            public async Task<bool> deleteDesignationById(Guid id)
            {
                var dbObj = await _appDbContext.Designations.FirstOrDefaultAsync(d => d.Id == id);
                _appDbContext.Designations.Remove(dbObj);
                await _appDbContext.SaveChangesAsync();

                return true;
            }
        }
}