using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IRepository
{
    public interface IEmployeeRepository
    {
        Task<(List<Employee>, int TotalCount)> GetAllEmployeesAsync(int userId, string role, int pageNumber, int pageSize, string? search = null);
    }
}
