using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IService
{
    public  interface IAttendanceChartService
    {
        Task<List<AttendanceChartDto>> GetAttendanceOverView();
    }
}
