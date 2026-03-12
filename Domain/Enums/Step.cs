using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public class Step
    {
        public enum AttendanceStatus
        {
            Present=1,
            Absent=2,
            Leave=3,
            Late=4,
            HalfDay=5

        }
        public enum LeaveStatus
        {
            Pending=1,
            Approved=2,
            Rejeccted=3
        }
    }
}
