using Application.Interfaces.IRepository;
using Domain.Entities;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
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
        
       
    }
}
