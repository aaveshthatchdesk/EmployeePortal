using Application.Interfaces.IRepository.Admin;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static Domain.Enums.Step;

namespace Infrastructure.Repositories.Admin
{
    public class DashboardSummaryRepository : IDashboardSummaryRepository
    {
        private readonly MainDbContext _context;

        public DashboardSummaryRepository(MainDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetTotalEmployees()
        {
           return await _context.employees.CountAsync();
        }
        public async Task<int> GetPresentToday(DateTime date)
        {
            return await _context.attendances.Where(a => a.Date == date && a.Status == AttendanceStatus.Present).CountAsync();
        }

        public async Task<int> GetEmployessOnLeave(DateTime date)
        {
            return await _context.leaveRequests.Where(l => l.StartDate <= date && l.EndDate >= date && l.Status == LeaveStatus.Approved).CountAsync();
        }
        public async Task<int> GetPendingRequests(DateTime date)
        {
            return await _context.leaveRequests.Where(l => l.StartDate <= date && l.EndDate >= date && l.Status == LeaveStatus.Pending).CountAsync();
        }

    }
}
