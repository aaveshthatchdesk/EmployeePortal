using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public  class EmployeeDto

    {
        public int Id { get; set; }
        public string FirstName { get; set; }= string.Empty;

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string FullName {  get; set; }= string.Empty;
        public string Name=> $"{FirstName} {LastName}";
        public string Address { get; set; }
        public int? DesignationId {  get; set; }

        public string Designation { get; set; }
        public int? ManagerId { get; set; }

       public string? Role {  get; set; }

        public int? UserId { get; set; }
        public string? ImageUrl { get; set; }
        public int? DepartmentId {  get; set; }
        public String Department { get; set; }
        public DateTime JoiningDate { get; set; }

        public string Status { get; set; }
        public string? Email {  get; set; }

    }
}
