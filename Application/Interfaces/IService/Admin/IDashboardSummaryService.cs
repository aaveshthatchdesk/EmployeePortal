using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IService.Admin
{
    public interface IDashboardSummaryService
    {
        Task<DashboardSummaryDto> DashboardSummary();
    }
}
