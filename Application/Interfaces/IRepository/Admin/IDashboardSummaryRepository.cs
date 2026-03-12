using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IRepository.Admin
{
  public interface IDashboardSummaryRepository
    {
        Task<int> GetTotalEmployees();
        Task<int> GetPresentToday(DateTime date);
        Task<int> GetEmployessOnLeave(DateTime date);
        Task<int> GetPendingRequests(DateTime date);
    }
}
