using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public  class AddEmployeeDto
    {
       
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int? DepartmentId { get; set; }

        public int? DesignationId { get; set; }

        public int? ManagerId { get; set; }

        public DateTime JoiningDate { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string Password { get; set; }

        public int? RoleId { get; set;}
        public string  Role { get; set; }
        public IBrowserFile? ImageFile { get; set; }
        public string? ImageUrl {  get; set; }
    }
}
