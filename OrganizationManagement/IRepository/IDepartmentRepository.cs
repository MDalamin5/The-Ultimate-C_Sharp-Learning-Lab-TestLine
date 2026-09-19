using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrganizationManagement.DTOs.Department;

namespace OrganizationManagement.IRepository
{
    public interface IDepartmentRepository
    {
        Task createCategories(DepartmentCreateDto model);
        Task getAllDepartments();
        Task getDepartmentById(Guid id);
        Task updateDepartmentById(Guid id, DepartmentUpdateDto model);
        Task deleteDepartmentById(Guid id);
    }
}