using Application.Dtos;
using Application.Interfaces.IRepository;
using Application.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class DepartmentService:IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<List<DepartmentDto>> GetDepartment()
        {
            var departments=await _departmentRepository.GetDepartments();

            return departments.Select(d => new DepartmentDto
            {
                Id=d.Id,
                Name=d.Name,

            }).ToList();
        }
        public async Task<List<DesignationDto>> GetDesignation()
        {
           var designations= await _departmentRepository.GetDesignations();
            return designations.Select(d=>new DesignationDto
            {
                Id = d.Id,
                Name = d.Name,
            }).ToList();
        }
    }
}
