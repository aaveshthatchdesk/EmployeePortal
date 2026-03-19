using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IRepository
{
    public  interface IAttendanceRepository
    {
        Task<bool> IsMarkedAsync(int userId, DateTime date);
        Task AddAttendanceAsync(int userId,Attendance attendance);
    }
}
