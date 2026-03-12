using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public  class DashboardSummaryDto
    {
        public int TotalEmployees {  get; set; }
        public int PresentToday {  get; set; }
        public int OnLeave {  get; set; }
        public int PendingRequests {  get; set; }

    }
}
