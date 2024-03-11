using Microsoft.Data.SqlClient;
using System.Data;
using TalentPortal.BAL.Dto;
using TalentPortal.BAL.Interfaces;
using TalentPortal.DAL;
using TalentPortal.DAL.Models;

namespace TalentPortal.BAL.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly IDataRepository dataRepository;
        public LeaveService(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;

        }
        public async Task<int> Add(LeaveDto leave)
        {
            if (leave == null)
            {
                return 0;
            }

            string query = "INSERT INTO Leaves (LeaveTypeId, FromDate, ToDate, Reason, SenderEmailIds, SenderCCEmailIds, UserId,Status) " +
               "VALUES (@LeaveTypeId, @FromDate, @ToDate, @Reason, @SenderEmailIds, @SenderCCEmailIds, @UserId, @Status)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@LeaveTypeId", leave.LeaveTypeId),
                new SqlParameter("@FromDate", leave.FromDate),
                new SqlParameter("@ToDate", leave.ToDate),
                new SqlParameter("@Reason", leave.Reason),
                new SqlParameter("@SenderEmailIds",  string.Join(",", leave.SenderEmailIds)),
                new SqlParameter("@SenderCCEmailIds", string.Join(",",leave.SenderCCEmailIds)),
                new SqlParameter("@UserId", leave.UserId),
                new SqlParameter("@Status","Pending")
            };
            return await dataRepository.ExecuteCreateQueryAsync(query, parameters);
        }

        public Task<Leave> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LeaveSearch>> GetByUser(int userId)
        {
            string query = "SELECT l.Id,l.FromDate,l.ToDate,lt.Name as LeaveType FROM leaves l inner join " +
                "leavetypes lt on lt.Id = l.LeaveTypeId  WHERE l.UserId = @userid";
            var parameters = new[]
            {new SqlParameter("@userId", userId)};

            DataTable dataTable = dataRepository.ExecuteQuery(query, parameters);
            List<LeaveSearch> entities = new List<LeaveSearch>();
            foreach (DataRow row in dataTable.Rows)
            {
                LeaveSearch leave = new LeaveSearch
                {
                    Id = Convert.ToInt32(row["Id"]),
                    FromDate = Convert.ToDateTime(row["FromDate"]),
                    ToDate = Convert.ToDateTime(row["ToDate"]),
                    Shift = Convert.ToString(row["LeaveType"]),
                    Status = "Pending"
                    // Populate other properties as needed
                };
                entities.Add(leave);
            }
            return entities;
        }

        public async Task<IEnumerable<LeaveSearch>> GetSentLeaves(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> Remove(Leave leave)
        {
            throw new NotImplementedException();
        }
    }
}
