using Application.Dtos;
using Application.Interfaces.IRepository;
using Application.Interfaces.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public  class AttendanceChartService:IAttendanceChartService
    {
        private readonly IAttendanceChartRepository _attendanceChartRepository;

        public AttendanceChartService(IAttendanceChartRepository attendanceChartRepository)
        {
            _attendanceChartRepository = attendanceChartRepository;
        }
        public async Task<List<AttendanceChartDto>>GetAttendanceOverView()
        {
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            var endOfWeek = startOfWeek.AddDays(4);

            var attendances = await _attendanceChartRepository.GetWeeklyAttendance(startOfWeek, endOfWeek);

            var result = attendances
                .GroupBy(a => a.Date.DayOfWeek)
                .Select(g => new AttendanceChartDto
                {
                    Day = g.Key.ToString().Substring(0, 3),
                    count = g.Count()
                })
                .OrderBy(x => x.Day)
                .ToList();

            return result;
        }
    }
}
