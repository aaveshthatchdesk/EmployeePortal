using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IService
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetDepartment();
        Task<List<DesignationDto>> GetDesignation();
    }
}
