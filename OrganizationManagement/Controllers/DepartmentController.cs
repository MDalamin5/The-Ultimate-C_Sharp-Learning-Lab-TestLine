using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Data;
using OrganizationManagement.DTOs.Department;
using OrganizationManagement.IRepository;
using OrganizationManagement.Models;

namespace OrganizationManagement.Controllers
{
    [ApiController]
    [Route("/api/v1/departments")]
    public class DepartmentController: ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _departmentRepository = departmentRepository;
        }

        // Department Create Endpoint
        [HttpPost]
        public async Task<IActionResult> createCategories([FromBody] DepartmentCreateDto model)
        {
            var response = await _departmentRepository.createCategories(model);
            if(response == true)
            {
                return Ok("Category created Successfully.");
            }
            else
                return Ok("Category is not created.");
        }

        // Get All Department
        [HttpGet]
        public async Task<IActionResult> getAllDepartments()
        {
            var allDepartments = await _departmentRepository.getAllDepartments();
            return Ok(allDepartments);
        }

        // Get a Department by ID
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> getDepartmentById(Guid id)
        {
            var dbObj = await _departmentRepository.getDepartmentById(id);
            
            return Ok(dbObj);
        }

        // Update departmentByID
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> updateDepartmentById(Guid id, [FromBody] DepartmentUpdateDto model)
        {
            var dbObj = await _departmentRepository.updateDepartmentById(id, model);
            

            return Ok(dbObj);
        }
        

        // Delete the Department
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> deleteDepartmentById(Guid id)
        {
            var dbObj = await _departmentRepository.deleteDepartmentById(id);
            

            return Ok("Data deleted successfully.");
        }
    }
}