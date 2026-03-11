using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public  class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PasswordHash { get; set; }
        public string Email { get; set; }
      
        public int RoleId { get; set; }

        public Role Roles { get; set; }
      



    }
}
