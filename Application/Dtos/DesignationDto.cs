using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class DesignationDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Designation is required")]
        public string Name { get; set; }=string.Empty;
    }
}
