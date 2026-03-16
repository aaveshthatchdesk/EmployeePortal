using System;
using System.Collections.Generic;
using System.Text;
using Application.Dtos;

namespace Application.Interfaces.IService
{
    public interface IEmployeeService
    {
        Task<PagedResult<EmployeeDto>> GetEmployeesByUserRole(int userId, string role, int pageNumber, int pageSize, string? search);
        Task AddEmployeeAsync(AddEmployeeDto dto);
        Task<EmployeesSummary> DashboardSummary();
    }


}
