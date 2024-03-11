using TalentPortal.BAL.Dto;
using TalentPortal.DAL.Models;

namespace TalentPortal.BAL.Interfaces
{
    public interface ICommonService
    {
        Task<List<Project>> GetAllProjects();
        Task<List<UserDto>> GetAllUsers();
        Task<List<LeaveType>> GetLeaveTypes();
    }
}
