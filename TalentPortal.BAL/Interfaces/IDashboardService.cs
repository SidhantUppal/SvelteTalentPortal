using TalentPortal.DAL.Models;

namespace TalentPortal.BAL.Interfaces
{
    public interface IDashboardService
    {
        Task<List<AttendanceLog>> GetAttendanceLogAsync(int id);
        Task<IEnumerable<Project>> GetProjectsAsync(int id);
        Task<AttendanceLog> GetTodayAttendance(int userId);
        Task<int> RecordTime(int userId, bool timeOut = false);
    }
}
