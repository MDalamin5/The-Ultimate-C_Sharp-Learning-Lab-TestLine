using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        
    }
}