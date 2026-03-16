using Application.Interfaces.IRepository;
using Domain.Entities;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Domain.Enums.Step;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly MainDbContext _context;

        public EmployeeRepository(MainDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetTotalEmployees()
        {
            return await _context.employees.CountAsync();
        }
        public async Task<int> GetActiveEmployees(DateTime date)
        {
            return await _context.attendances.Where(a => a.Date == date && a.Status == AttendanceStatus.Present).CountAsync();
        }

        public async Task<int> GetEmployessOnLeave(DateTime date)
        {
            return await _context.leaveRequests.Where(l => l.StartDate <= date && l.EndDate >= date && l.Status == LeaveStatus.Approved).CountAsync();
        }
       
        public async Task<(List<Employee>,int TotalCount)> GetAllEmployeesAsync(int userId, string role, int pageNumber, int pageSize, string? search = null)
        {
          var query=  _context.employees.
                Include(a => a.Department)
                .Include(a => a.Designation)
                .Include(a => a.Attendances)
                .Include(a => a.LeaveRequests)
               .AsQueryable();


            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.FirstName.Contains(search) ||
                t.Designation.Name.Contains(search) ||
                t.Address.Contains(search) ||
                t.Department.Name.Contains(search));
                ;
            }
           


            else if (role == "Manager")
            {
                query = query.Where(t => t.ManagerId == userId);

            }
          
            var totalCount = await query.CountAsync();

            var tasks = await query
     .OrderByDescending(t => t.Id)
     .Skip((pageNumber - 1) * pageSize)
     .Take(pageSize)
     .ToListAsync();
            return (tasks, totalCount);
        }

        public async Task AddEmployee(Employee employee)
        {
            await _context.employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }



        public async Task AddEmployeeWithUserAsync(Users user, Employee employee, string roleName)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var role = await _context.roles
                    .FirstOrDefaultAsync(r => r.Name == roleName);

                if (role == null)
                    throw new Exception("Role not found");

                user.RoleId = role.Id;

                _context.users.Add(user);
                await _context.SaveChangesAsync();

                employee.UserId = user.Id;

                _context.employees.Add(employee);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
