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
        public async Task<int> GetTotalEmployees(string role,int userId)
        {
            if (role == "Admin")
            {
                return await _context.employees.CountAsync();
            }
            else if(role=="Manager")
            {
                var manager = await _context.employees
             .FirstOrDefaultAsync(e => e.UserId == userId);

                return manager == null
                    ? 0
                    : await _context.employees
                        .Where(e => e.ManagerId == manager.Id)
                        .CountAsync();
            }
            return 0;

        }
        public async Task<int> GetActiveEmployees(DateTime date)
        {
            return await _context.attendances.Where(a => a.Date == date && a.Status == AttendanceStatus.Present).CountAsync();
        }

        public async Task<int> GetEmployessOnLeave(DateTime date)
        {
            return await _context.leaveRequests.Where(l => l.StartDate <= date && l.EndDate >= date && l.Status == LeaveStatus.Approved).CountAsync();
        }


        public async Task<List<Employee>> GetAllManagersAsync()
        {
            return await _context.employees
                .Where(e => e.User.Roles.Name == "Manager")
                .Select(e => new Employee
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName
                })
                .ToListAsync();
        }
        public async Task<List<Role>> GetAllRoles()
        {
      

            return await _context.roles
                .Where(r => r.Name.ToLower() != "admin").ToListAsync();

        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.employees
                .Include(a=>a.User)
                    .ThenInclude(b=>b.Roles)
                .Include(a => a.Department)
                .Include(a => a.Designation)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<(List<Employee>,int TotalCount)> GetAllEmployeesAsync(int userId, string role, int pageNumber, int pageSize, string? search = null)
        {
          var query=  _context.employees.
                Include(a => a.Department)
                .Include(a => a.Designation)
                .Include(a => a.Attendances)
                .Include(a => a.LeaveRequests)
               .AsQueryable();


            if (role == "Manager")
            {
                var managerEmployee = await _context.employees
                        .FirstOrDefaultAsync(e => e.UserId == userId);
                if (managerEmployee != null)
                {
                    query = query.Where(t => t.ManagerId == managerEmployee.Id);
                }
            }
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.FirstName.Contains(search) ||
                t.Designation.Name.Contains(search) ||
                t.Address.Contains(search) ||
                t.Department.Name.Contains(search));
                ;
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
            if (string.IsNullOrWhiteSpace(roleName))
                throw new Exception("Role name is required");
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var role = await _context.roles
                    .FirstOrDefaultAsync(r => r.Name.ToLower() == roleName.ToLower());

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

        public async Task UpdateEmployeeWithUserAsync(Users user, Employee employee, string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new Exception("Role name is required");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingUser = await _context.users
                    .FirstOrDefaultAsync(u => u.Id == user.Id);

                if (existingUser == null)
                    throw new Exception("User not found");

                var role = await _context.roles
                    .FirstOrDefaultAsync(r => r.Name.ToLower() == roleName.ToLower());

                if (role == null)
                    throw new Exception("Role not found");

                // ✅ Update User
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.RoleId = role.Id;

                // Update password only if provided
                if (!string.IsNullOrWhiteSpace(user.PasswordHash))
                {
                    existingUser.PasswordHash = user.PasswordHash;
                }

                _context.users.Update(existingUser);
                await _context.SaveChangesAsync();

                // ✅ Update Employee
                var existingEmployee = await _context.employees
                    .FirstOrDefaultAsync(e => e.Id == employee.Id);

                if (existingEmployee == null)
                    throw new Exception("Employee not found");

                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Address = employee.Address;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.DepartmentId = employee.DepartmentId;
                existingEmployee.DesignationId = employee.DesignationId;
                existingEmployee.ManagerId = employee.ManagerId;
                existingEmployee.JoiningDate = employee.JoiningDate;
                existingEmployee.ProfilePhoto = employee.ProfilePhoto;

                _context.employees.Update(existingEmployee);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteEmployeeWithUserAsync(int employeeId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
               
                var employee = await _context.employees
                    .FirstOrDefaultAsync(e => e.Id == employeeId);

                if (employee == null)
                    throw new Exception("Employee not found");

               
                var subordinates = await _context.employees
                    .Where(e => e.ManagerId == employeeId)
                    .ToListAsync();

                foreach (var sub in subordinates)
                {
                    sub.ManagerId = null; 
                }

                await _context.SaveChangesAsync();

               
                var user = await _context.users
                    .FirstOrDefaultAsync(u => u.Id == employee.UserId);

            
                _context.employees.Remove(employee);
                await _context.SaveChangesAsync();

               
                if (user != null)
                {
                    _context.users.Remove(user);
                    await _context.SaveChangesAsync();
                }

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
