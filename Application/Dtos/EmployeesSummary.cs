using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class EmployeesSummary
    {
        public int TotalEmployees { get; set; }
        public int Active {  get; set; }
        public int OnLeave { get; set; }
        public int InActive { get; set; }
        
    }
}
