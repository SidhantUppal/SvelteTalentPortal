using TalentPortal.BAL.Dto;
using TalentPortal.DAL.Models;

namespace TalentPortal.BAL.Interfaces
{
    public interface ILeaveService
    {
        Task<int> Add(LeaveDto leave);
        Task<int> Remove(Leave leave);
        Task<Leave> Get(int id);
        Task<List<LeaveSearch>> GetByUser(int userId);
        Task<IEnumerable<LeaveSearch>> GetSentLeaves(int userId);
    }
}
