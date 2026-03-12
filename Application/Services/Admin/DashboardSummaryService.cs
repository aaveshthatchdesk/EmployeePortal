using Application.Dtos;
using Application.Interfaces.IRepository.Admin;
using Application.Interfaces.IService.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Admin
{
    public  class DashboardSummaryService:IDashboardSummaryService
    {
        private readonly IDashboardSummaryRepository _summaryRepository;

        public DashboardSummaryService(IDashboardSummaryRepository summaryRepository)
        {
            _summaryRepository = summaryRepository;
        }
        public async Task<DashboardSummaryDto> DashboardSummary()
        {
            var today=DateTime.Now;
           var totalemployees= await _summaryRepository.GetTotalEmployees();
            var presentToday = await _summaryRepository.GetPresentToday(today);
            var employeesOnLeave =await _summaryRepository.GetEmployessOnLeave(today);
            var PendingRequests=await _summaryRepository.GetPendingRequests(today);

            return new DashboardSummaryDto
            {
                TotalEmployees = totalemployees,
                PresentToday = presentToday,
                OnLeave = employeesOnLeave,
                PendingRequests = PendingRequests,

            };

        }
    }
}
