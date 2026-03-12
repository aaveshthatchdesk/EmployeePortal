using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Enums.Step;

namespace Domain.Entities
{
    public class Attendance
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public DateTime Date { get; set; }

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }

        public AttendanceStatus Status { get; set; } // Present, Absent, Leave, Late

        public string Remarks { get; set; }
    }
}
