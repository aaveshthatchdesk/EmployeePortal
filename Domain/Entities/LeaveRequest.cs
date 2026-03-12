using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Enums.Step;

namespace Domain.Entities
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Reason { get; set; }

        public LeaveStatus Status { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
