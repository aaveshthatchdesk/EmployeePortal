using Application.Dtos;
using Application.Interfaces.IRepository;
using Application.Interfaces.IService;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class AttendanceService:IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }
        public async Task<bool> IsMarkedAttendance(int userId, DateTime date)
        {
            return await _attendanceRepository.IsMarkedAsync(userId,date);
        }

        public async Task MarkAttendance(int userId,AttendanceDto dto)
        {
            
            var attendance = new Attendance
            {
                EmployeeId = dto.EmployeeId,
                Date = dto.Date,
                CheckInTime = dto.CheckInTime,
                CheckOutTime = dto.CheckOutTime,
                Status = dto.Status,
                Remarks = dto.Remarks
            };

            await _attendanceRepository.AddAttendanceAsync(userId,attendance);
        }
    }
}
