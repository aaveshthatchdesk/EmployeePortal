using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public  class LoginResponseDto
    {
        public bool ok { get; set; }
        public int status { get; set; }
        public string statusText { get; set; }

    }
}
