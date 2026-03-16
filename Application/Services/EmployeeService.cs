using Application.Dtos;
using Application.Interfaces.IRepository;
using Application.Interfaces.IService;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using static Domain.Enums.Step;

namespace Application.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<EmployeesSummary> DashboardSummary()
        {
            var today = DateTime.Now;
            var totalemployees = await _employeeRepository.GetTotalEmployees();
            var activeemployees = await _employeeRepository.GetActiveEmployees(today);
            var employeesOnLeave = await _employeeRepository.GetEmployessOnLeave(today);
            var inactiveemployees=totalemployees-activeemployees-employeesOnLeave;

            return new  EmployeesSummary
            {
                TotalEmployees = totalemployees,
                Active = activeemployees,
                OnLeave = employeesOnLeave,
                InActive =inactiveemployees

            };

        }
        public async Task<PagedResult<EmployeeDto>> GetEmployeesByUserRole(int userId, string role, int pageNumber, int pageSize, string? search)
        {
           
            
                var(employees,totalCount)=await _employeeRepository.GetAllEmployeesAsync(userId, role, pageNumber, pageSize, search);
            
          
            var today= DateTime.Now;
            var result = employees.Select(e =>
            {
                string status = "Inactive";

                var leave = e.LeaveRequests
                    ?.FirstOrDefault(l => l.Status == LeaveStatus.Approved &&
                                          today >= l.StartDate.Date &&
                                          today <= l.EndDate.Date);

                var attendance = e.Attendances
                    ?.FirstOrDefault(a => a.Date.Date == today);

                if (leave != null)
                    status = "On Leave";
                else if (attendance != null &&
                        (attendance.Status == AttendanceStatus.Present ||
                         attendance.Status == AttendanceStatus.Late))
                    status = "Active";

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
                    Status = status
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
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            var user = new Users
            {
                Name = dto.UserName,
                Email=dto.UserEmail,

                 PasswordHash = Convert.ToBase64String(bytes)
                //hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password)
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
                JoiningDate = dto.JoiningDate
            };

            await _employeeRepository.AddEmployeeWithUserAsync(user, employee, dto.Role);
        }


    }
}
