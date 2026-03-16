using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IRepository
{
    public interface IEmployeeRepository
    {

        Task<int> GetTotalEmployees();
        Task<int> GetActiveEmployees(DateTime date);
        Task<int> GetEmployessOnLeave(DateTime date);
        Task<(List<Employee>, int TotalCount)> GetAllEmployeesAsync(int userId, string role, int pageNumber, int pageSize, string? search = null);
        Task AddEmployee(Employee employee);
        Task AddEmployeeWithUserAsync(Users user, Employee employee, string roleName);
       


    }
}
