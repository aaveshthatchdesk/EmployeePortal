using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IRepository
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartments();
        Task<List<Designation>> GetDesignations();
    }
}
