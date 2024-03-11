using TalentPortal.BAL.Dto;

namespace TalentPortal.BAL.Interfaces
{
    public interface IReportService
    {
        Task<int> Add(ReportDto entity);
        Task<int> Update(ReportDto entity);
        Task<ReportSearch> Get(int id);
        Task<List<ReportSearch>> GetByUser(int userId);
        Task<List<ReportSearch>> GetReceivedReport(int userId);
    }
}
