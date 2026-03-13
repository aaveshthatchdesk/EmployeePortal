using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
