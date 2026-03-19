using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IRepository
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartments();
        Task AddDepartment(Department department);
        Task<List<Designation>> GetDesignations();
        Task AddDesigantion(Designation designation);
        Task<(int totalEmployees, List<(string DepartmentName, int Count)> data)> GetDepartmentDistribution();
    }
}
