using Application.Dtos;
using Application.Interfaces.IRepository;
using Application.Interfaces.IService;
using Domain.Entities;
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
        public async Task<DepartmentDto> AddDepartmentAsync(DepartmentDto department)
        {
            var departmentss = new Department
            {
                Name = department.Name,
            };
             await _departmentRepository.AddDepartment(departmentss);
            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
            };
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
        public async Task<DesignationDto> AddDesignationAsync(DesignationDto desigantion)
        {
            var designationss = new Designation
            {
                Name = desigantion.Name,
            };
            await _departmentRepository.AddDesigantion(designationss);
            return new DesignationDto
            {
                Id = designationss.Id,
                Name = designationss.Name,
            };
        }

        public async Task<List<DepartmentDistributionDto>> GetDepartmentDistribution()
        {
            var (Employees, data) = await _departmentRepository.GetDepartmentDistribution();

            return data.Select(x => new DepartmentDistributionDto
            {
                Name = x.DepartmentName,
                Count = x.Count,
                Percentage = Employees == 0
                    ? 0
                    : Math.Round((x.Count * 100.0) / Employees, 2)
            }).ToList();
        }
    }
}
