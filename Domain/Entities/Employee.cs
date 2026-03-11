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

        public int? ManagerUserId { get; set; }

        public DateTime JoiningDate { get; set; }

        
    }
}
