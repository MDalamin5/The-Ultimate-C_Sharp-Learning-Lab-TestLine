using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Data;
using OrganizationManagement.DTOs.Department;
using OrganizationManagement.Models;

namespace OrganizationManagement.Controllers
{
    [ApiController]
    [Route("/api/v1/departments")]
    public class DepartmentController: ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public DepartmentController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Department Create Endpoint
        [HttpPost]
        public async Task<IActionResult> createCategories([FromBody] DepartmentCreateDto model)
        {
            var data = new Department
            {
                Id = Guid.NewGuid(),
                Name = model.Name
            };

            Console.WriteLine($"Name: {data.Name}, Id: {data.Id}");
            
            await _appDbContext.Departments.AddAsync(data);
            await _appDbContext.SaveChangesAsync();
            return Ok("Categories Created.");
        }

        // Get All Department
        [HttpGet]
        public async Task<IActionResult> getAllDepartments()
        {
            var allDepartments = await _appDbContext.Departments.ToListAsync();
            return Ok(allDepartments);
        }

        // Get a Department by ID
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> getDepartmentById(Guid id)
        {
            var dbObj = await _appDbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);
            
            return Ok(dbObj);
        }

        // Update departmentByID
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> updateDepartmentById(Guid id, [FromBody] DepartmentUpdateDto model)
        {
            var dbObj = await _appDbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);
            dbObj.Name = model.Name;
            await _appDbContext.SaveChangesAsync();

            return Ok(dbObj);
        }
        

        // Delete the Department
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> deleteDepartmentById(Guid id)
        {
            var dbObj = await _appDbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);
            _appDbContext.Departments.Remove(dbObj);
            await _appDbContext.SaveChangesAsync();

            return Ok("Data deleted successfully.");
        }
    }
}