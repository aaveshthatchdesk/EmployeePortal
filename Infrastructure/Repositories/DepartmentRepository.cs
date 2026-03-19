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

        public async Task AddDepartment(Department department)
        {
            var existing =  _context.departments
        .FirstOrDefault(d => d.Name == department.Name);

            if (existing != null)
            {
                throw new Exception("Department already exists");
            }
            await _context.departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Designation>> GetDesignations()
        {
            return await _context.designations
            .GroupBy(d => d.Name)
            .Select(g => g.First()).ToListAsync();
        }
        public async Task AddDesigantion(Designation designation)
        {
            var existing = _context.designations
        .FirstOrDefault(d => d.Name == designation.Name);

            if (existing != null)
            {
                throw new Exception("Designation already exists");
            }
            await _context.designations.AddAsync(designation);
            await _context.SaveChangesAsync();
        }
        public async Task<(int totalEmployees, List<(string DepartmentName, int Count)> data)> GetDepartmentDistribution()
        {
            var totalEmployees = await _context.employees.CountAsync();

            var data = await _context.employees
                .Include(e => e.Department)
                 .Where(e => e.DepartmentId != null)
                .GroupBy(e => e.Department.Name)

                .Select(g => new ValueTuple<string, int>(
                    g.Key,
                    g.Count()
                ))
                .ToListAsync();

            return (totalEmployees, data);
        }
    }
}
