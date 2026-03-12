using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Employee
    {


        public int Id { get; set; }

        public int UserId { get; set; }

        public Users User { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address {  get; set; }

        public string ProfilePhoto {get; set; }

        public string Phone { get; set; }

        
        public int? DesignationId {  get; set; }
        public Designation Designation { get; set; }
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }

        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }

        public ICollection<Employee> Subordinates { get; set; }

        public DateTime JoiningDate { get; set; }

        public ICollection<LeaveRequest> LeaveRequests { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

    }
}
