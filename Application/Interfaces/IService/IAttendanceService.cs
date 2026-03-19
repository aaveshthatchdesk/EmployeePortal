using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IService
{
    public interface IAttendanceService
    {
        Task<bool> IsMarkedAttendance(int userId, DateTime date);
        Task MarkAttendance(int userId,AttendanceDto dto);
    }
}
