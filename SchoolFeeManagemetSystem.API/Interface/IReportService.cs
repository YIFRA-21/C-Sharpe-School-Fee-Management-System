
using SchoolFeeManagemetSystem.API.DTOs;
using static SchoolFeeManagemetSystem.API.DTOs.ReportDTOs;
namespace SchoolFeeManagemetSystem.API.Interface
{


 

    public interface IReportService
    {
        Task<List<ReportDTO>> GetReportsAsync( DateTime from,DateTime to,string className,string category );
        Task<List<ReportDTO>> GetReportsAsync(ReportFilterDTO filter);
        Task<decimal> GetTotalCollectionAsync(DateTime from, DateTime to);
        Task<decimal> GetTotalOutstandingAsync();
        Task<int> GetTotalStudentsAsync();
    }
}