using Application.Interfaces.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Enums.Step;

namespace Infrastructure.Repositories
{
    public  class AttendanceChartRepository:IAttendanceChartRepository
    {
        private readonly MainDbContext _context;

        public AttendanceChartRepository(MainDbContext context)
        {
            _context = context;
        }
        public async Task<List<Attendance>> GetWeeklyAttendance(DateTime startofWeek, DateTime endofWeek)
        {
            return await _context.attendances
                   .Where(a => a.Date >= startofWeek &&a.Date <= endofWeek &&
                               a.Status == AttendanceStatus.Present).ToListAsync();
        }
    }
}
