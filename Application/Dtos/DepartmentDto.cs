using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Department name is required")]
        public string Name { get; set; }=String.Empty;
    }
}
