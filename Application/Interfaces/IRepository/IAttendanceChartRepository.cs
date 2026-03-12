using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IRepository
{
    public interface IAttendanceChartRepository
    {
        Task<List<Attendance>> GetWeeklyAttendance(DateTime startofWeek, DateTime endofWeek);
    }
}
