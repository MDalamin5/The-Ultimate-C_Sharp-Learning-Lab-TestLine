using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrganizationManagement.DTOs.Department;

namespace OrganizationManagement.IRepository
{
    public interface IDepartmentRepository
    {
        Task<bool> createCategories(DepartmentCreateDto model);
        Task<List<DepartmentReadDto>> getAllDepartments();
        Task<DepartmentReadDto> getDepartmentById(Guid id);
        Task<bool> updateDepartmentById(Guid id, DepartmentUpdateDto model);
        Task<bool> deleteDepartmentById(Guid id);
    }
}