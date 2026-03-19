using Application.Interfaces.IRepository;
using Domain.Entities;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AttendanceRepository:IAttendanceRepository
    {
        private readonly MainDbContext _context;

        public AttendanceRepository(MainDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IsMarkedAsync(int userId, DateTime date)
        {
            var employee = await _context.employees
       .FirstOrDefaultAsync(e => e.UserId == userId);

            var EmployeeId = employee.Id;

            if (employee == null)
                throw new Exception("Employee not found");
            return await _context.attendances
                .AnyAsync(a => a.EmployeeId == EmployeeId && a.Date == date);
        }

        public async Task AddAttendanceAsync(int userId,Attendance attendance)
        {
            var employee = await _context.employees
        .FirstOrDefaultAsync(e => e.UserId == userId);

            if (employee == null)
                throw new Exception("Employee not found");

            // ✅ Set correct EmployeeId
            attendance.EmployeeId = employee.Id;

            // (Optional) prevent duplicate
            var exists = await _context.attendances
                .AnyAsync(a => a.EmployeeId == employee.Id && a.Date == attendance.Date);

            if (exists)
                return;
            await _context.attendances.AddAsync(attendance);
            var result=await _context.SaveChangesAsync();
            Console.WriteLine($"Rows affected: {result}");
        }
    }
}
