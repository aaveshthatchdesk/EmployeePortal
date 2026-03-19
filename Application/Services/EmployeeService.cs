using Application.Dtos;
using Application.Interfaces.IRepository;
using Application.Interfaces.IService;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using static Domain.Enums.Step;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<EmployeesSummary> DashboardSummary(string role, int userId)
        {
            var today = DateTime.Now;
            var totalemployees = await _employeeRepository.GetTotalEmployees(role,userId);
            var activeemployees = await _employeeRepository.GetActiveEmployees(userId,role,today);
            var employeesOnLeave = await _employeeRepository.GetEmployessOnLeave(today);
            var inactiveemployees=await _employeeRepository.GetInActiveEmployees(userId,role,today);

            return new  EmployeesSummary
            {
                TotalEmployees = totalemployees,
                Active = activeemployees,
                OnLeave = employeesOnLeave,
                InActive =inactiveemployees

            };

        }

        public async Task<List<EmployeeDto>> GetManagers()
        {
            var managers = await _employeeRepository.GetAllManagersAsync();

            return managers.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FullName = $"{e.FirstName} {e.LastName}"
            }).ToList();
        }
        public async Task<List<RolesDto>> GetAllRolesAsync()
        {
            var roles = await _employeeRepository.GetAllRoles();

            return roles.Select(d => new RolesDto
            {
                Id = d.Id,
                Name = d.Name,

            }).ToList();
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null) return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                UserId=employee.UserId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Phone = employee.Phone,
                Address = employee.Address,
                DepartmentId = employee.DepartmentId,
                Department=employee.Department.Name,
                DesignationId = employee.DesignationId,
                Designation=employee.Designation.Name,
                ManagerId = employee.ManagerId,
                Role = employee.User.Roles.Name,
                Email = employee.User.Email,
                JoiningDate = employee.JoiningDate,
                ImageUrl = employee.ProfilePhoto
            };
        }
        public async Task<PagedResult<EmployeeDto>> GetEmployeesByUserRole(int userId, string role, int pageNumber, int pageSize, string? search)
        {
           
            
                var(employees,totalCount)=await _employeeRepository.GetAllEmployeesAsync(userId, role, pageNumber, pageSize, search);
            //string status = "Inactive";
            var start = DateTime.Today;
            var end = start.AddDays(1);

            var today = DateTime.Now;
            //var result = employees.Select(e =>
            //{
            //    string status = "Inactive";
            //    var start = DateTime.Today;
            //    var end = start.AddDays(1);

                //var attendance = e.Attendances
                //    ?.FirstOrDefault(a => a.Date>start && a.Date<end);

                //if (attendance != null)
                //{
                //    if (attendance.Status == AttendanceStatus.Present ||
                //        attendance.Status == AttendanceStatus.Late)
                //    {
                //        status = "Active";
                //    }
                //    else if (attendance.Status == AttendanceStatus.Absent)
                //    {
                //        status = "Inactive";
                //    }
                //}
                var result = employees.Select(e =>
                {
                    var isActive = e.Attendances
                        ?.Any(a => a.Date >= start &&
                                   a.Date < end &&
                                   (a.Status == AttendanceStatus.Present ||
                                    a.Status == AttendanceStatus.Late)) ?? false;


                    return new EmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Address = e.Address,
                    Designation = e.Designation?.Name,
                    Department = e.Department?.Name,
                    ManagerId = e.ManagerId ?? 0,
                    ImageUrl = e.ProfilePhoto,
                    JoiningDate = e.JoiningDate,
                    Status = isActive ? "Active" : "Inactive"
                    };

            }).ToList();
            return new PagedResult<EmployeeDto>
            {
                Items = result,
                TotalCount = totalCount,
               
            };
        }

        public async Task AddEmployeeAsync(AddEmployeeDto dto)
        {
           

            var user = new Users
            {
                Name = dto.UserName,
                Email=dto.UserEmail,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
                

            };

            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                Phone = dto.Phone,
               
                DepartmentId = dto.DepartmentId,
                DesignationId = dto.DesignationId,
                ManagerId = dto.ManagerId,
                JoiningDate = dto.JoiningDate,
                ProfilePhoto = string.IsNullOrWhiteSpace(dto.ImageUrl) ? "/images/default-user.png" : dto.ImageUrl
            };

            await _employeeRepository.AddEmployeeWithUserAsync(user, employee, dto.Role);
        }

        public async Task UpdateEmployeeAsync(UpdateEmployeeDto dto)
        {
            var user = new Users
            {
                Id = dto.UserId, 
                Name = dto.UserName,
                Email = dto.UserEmail,
                PasswordHash = string.IsNullOrWhiteSpace(dto.Password)
                    ? null
                    : BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            var employee = new Employee
            {
                Id = dto.EmployeeId, 
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                Phone = dto.Phone,
                DepartmentId = dto.DepartmentId,
                DesignationId = dto.DesignationId,
                ManagerId = dto.ManagerId,
                JoiningDate = dto.JoiningDate,
                ProfilePhoto = string.IsNullOrWhiteSpace(dto.ImageUrl)
                    ? "/images/default-user.png"
                    : dto.ImageUrl
            };

            await _employeeRepository.UpdateEmployeeWithUserAsync(user, employee, dto.Role);
        }


    }
}
