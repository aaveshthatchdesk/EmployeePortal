using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IService
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetDepartment();
        Task<DepartmentDto> AddDepartmentAsync(DepartmentDto department);
        Task<List<DesignationDto>> GetDesignation();
        Task<DesignationDto> AddDesignationAsync(DesignationDto desigantion);
        Task<List<DepartmentDistributionDto>> GetDepartmentDistribution();
    }
}
