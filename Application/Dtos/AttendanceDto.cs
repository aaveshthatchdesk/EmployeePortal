using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Enums.Step;

namespace Application.Dtos
{
    public class AttendanceDto
    {
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public AttendanceStatus Status { get; set; }
        public string? Remarks { get; set; }
    }
}
