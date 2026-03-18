using Application.Interfaces.IRepository;
using Domain.Entities;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private readonly MainDbContext _context;

        public DepartmentRepository(MainDbContext context)
        {
            _context = context;
        }
      
        public async Task<List<Department>> GetDepartments()
        {
            return await _context.departments.ToListAsync();
        }
        public async Task<List<Designation>> GetDesignations()
        {
            return await _context.designations
            .GroupBy(d => d.Name)
            .Select(g => g.First()).ToListAsync();
        }
    }
}
