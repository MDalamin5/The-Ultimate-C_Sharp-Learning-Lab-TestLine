using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrganizationManagement.Data;
using OrganizationManagement.Repository.IRepository;
using OrganizationManagement.DTOs.Designation;

namespace OrganizationManagement.Controllers
{
    [ApiController]
    [Route("/api/v1/designations")]
    public class DesignationController: ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDesignationRepository _designationRepository;

        public DesignationController(IDesignationRepository designationRepository, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _designationRepository = designationRepository;
        }

        // Department Create Endpoint
        [HttpPost]
        public async Task<IActionResult> createCategories([FromBody] CreateDesignationDto model)
        {
            var response = await _designationRepository.CreateDesignation(model);
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
            var allDepartments = await _designationRepository.getAllDesignation();
            return Ok(allDepartments);
        }

        // Get a Department by ID
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> getDepartmentById(Guid id)
        {
            var dbObj = await _designationRepository.getDesignationById(id);
            
            return Ok(dbObj);
        }

        // Update departmentByID
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> updateDepartmentById(Guid id, [FromBody] UpdateDesignationDto model)
        {
            var dbObj = await _designationRepository.updateDesignationById(id, model);
            

            return Ok(dbObj);
        }
        

        // Delete the Department
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> deleteDepartmentById(Guid id)
        {
            var dbObj = await _designationRepository.deleteDesignationById(id);
            

            return Ok("Data deleted successfully.");
        }
    }
}