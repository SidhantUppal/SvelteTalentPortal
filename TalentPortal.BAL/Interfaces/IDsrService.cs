using TalentPortal.BAL.Dto;

namespace TalentPortal.BAL.Interfaces
{
    public interface IDsrService
    {
        public Task<int> AddDsr(DsrDto dto);
        public Task<int> UpdateDsr(DsrDto dto);
        public Task<IEnumerable<DsrSearch>> GetDsr(bool isToday, int userId);
        Task<bool> SubmitDsr(DsrSubmitDto dto, int userId);
        public Task<IEnumerable<DsrSearch>> SentDsrs(int userId);
        public Task<IEnumerable<DsrSearch>> ReceivedDsrs(int userId);
        public Task<IEnumerable<DsrSearch>> GetDsr(int logId);
        Task<bool> IsDsrCompleted(int userId);

    }
}
