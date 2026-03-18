using System;
using System.Collections.Generic;
using System.Text;
using Application.Dtos;

namespace Application.Interfaces.IService
{
    public interface IEmployeeService
    {
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<PagedResult<EmployeeDto>> GetEmployeesByUserRole(int userId, string role, int pageNumber, int pageSize, string? search);
        Task AddEmployeeAsync(AddEmployeeDto dto);

        Task UpdateEmployeeAsync(UpdateEmployeeDto dto);

        Task<EmployeesSummary> DashboardSummary(string role, int userId);

        Task<List<EmployeeDto>> GetManagers();
        Task<List<RolesDto>> GetAllRolesAsync();
    }


}
