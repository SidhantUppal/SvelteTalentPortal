using TalentPortal.BAL.Dto;

namespace TalentPortal.BAL.Interfaces
{
    public interface IUserService
    {
        public Task<int> CreateUser(UserDto user);
        public Task<UserDto> GetUser(string username);
        public Task<UserDto> GetUser(int userId);
        public int UpdateUser(UserDto user);
        public int DeleteUser(int id);
    }
}
