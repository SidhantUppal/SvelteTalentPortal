using Microsoft.Data.SqlClient;
using System.Data;
using TalentPortal.BAL.Dto;
using TalentPortal.BAL.Interfaces;
using TalentPortal.DAL;

namespace TalentPortal.BAL.Services
{
    public class ReportService : IReportService
    {
        public readonly IDataRepository dataRepository;
        public ReportService(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public async Task<int> Add(ReportDto entity)
        {
            if (entity == null)
            {
                return 0;
            }

            string query = "INSERT INTO Reports (ProjectId,Description,SenderEmailIds,SenderCCEmailIds,UserId) " +
               "VALUES (@ProjectId, @Description,@SenderEmailIds, @SenderCCEmailIds, @UserId)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@ProjectId", entity.ProjectId),
                new SqlParameter("@Description", entity.Description),
                new SqlParameter("@SenderEmailIds",  string.Join(",", entity.SenderEmailIds)),
                new SqlParameter("@SenderCCEmailIds", string.Join(",",entity.SenderCCEmailIds)),
                new SqlParameter("@UserId", entity.UserId),
            };
            return await dataRepository.ExecuteCreateQueryAsync(query, parameters);
        }

        public Task<ReportSearch> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ReportSearch>> GetByUser(int userId)
        {
            string query =
                "SELECT r.*, p.Name AS ProjectName, " +
                "(SELECT STRING_AGG(eu.Email, ',') FROM Users eu " +
                "WHERE CHARINDEX(',' + CAST(eu.Id AS VARCHAR) + ',', ',' + r.SenderEmailIds + ',') > 0) AS SenderEmails, " +
                "(SELECT STRING_AGG(eucc.Email, ',') FROM Users eucc " +
                "WHERE CHARINDEX(',' + CAST(eucc.Id AS VARCHAR) + ',', ',' + r.SenderCCEmailIds + ',') > 0) AS CCEmails " +
                "FROM reports r " +
                "INNER JOIN projects p ON p.Id = r.ProjectId " +
                "WHERE r.UserId = @userId";

            var parameters = new[]
            {
                new SqlParameter("@userId", userId)
            };

            DataTable dataTable = dataRepository.ExecuteQuery(query, parameters);
            List<ReportSearch> entities = new List<ReportSearch>();
            foreach (DataRow row in dataTable.Rows)
            {
                // Populate DsrSearch object from each row
                ReportSearch dsr = new ReportSearch
                {
                    Id = Convert.ToInt32(row["Id"]),
                    ProjectId = Convert.ToInt32(row["ProjectId"]),
                    Description = Convert.ToString(row["Description"]),
                    UserId = Convert.ToInt32(row["UserId"]),
                    ProjectName = Convert.ToString(row["ProjectName"]),
                    SenderEmailIds = Convert.ToString(row["SenderEmails"]).Split(',').ToList(), // Split sender emails into list
                    SenderCCEmailIds = Convert.ToString(row["CCEmails"]).Split(',').ToList(),
                    Date = Convert.ToDateTime(row["CreatedAt"])
                    // Populate other properties as needed
                };

                // Add DsrSearch object to the list
                entities.Add(dsr);
            }
            return entities;
        }

        public async Task<List<ReportSearch>> GetReceivedReport(int userId)
        {
            string query = "SELECT r.*, p.Name AS ProjectName, "
                + "(SELECT STRING_AGG(eu.Email, ',') FROM Users eu "
                + "WHERE CHARINDEX(',' + CAST(eu.Id AS VARCHAR) + ',', ',' + r.SenderEmailIds + ',') > 0) AS SenderEmails,"
                + "(SELECT STRING_AGG(eucc.Email, ',') FROM Users eucc "
                + "WHERE CHARINDEX(',' + CAST(eucc.Id AS VARCHAR) + ',', ',' + r.SenderCCEmailIds + ',') > 0) AS CCEmails "
                + "FROM reports r "
                + "INNER JOIN projects p ON p.Id = r.ProjectId "
                + "WHERE @userId in (SELECT CAST(value AS INT) FROM STRING_SPLIT(r.SenderEmailIds, ','))";


            var parameters = new[]
            {
                new SqlParameter("@userId", userId)
            };

            DataTable dataTable = dataRepository.ExecuteQuery(query, parameters);
            List<ReportSearch> entities = new List<ReportSearch>();
            foreach (DataRow row in dataTable.Rows)
            {
                // Populate DsrSearch object from each row
                ReportSearch dsr = new ReportSearch
                {
                    Id = Convert.ToInt32(row["Id"]),
                    ProjectId = Convert.ToInt32(row["ProjectId"]),
                    Description = Convert.ToString(row["Description"]),
                    UserId = Convert.ToInt32(row["UserId"]),
                    ProjectName = Convert.ToString(row["ProjectName"]),
                    SenderEmailIds = Convert.ToString(row["SenderEmails"]).Split(',').ToList(), // Split sender emails into list
                    SenderCCEmailIds = Convert.ToString(row["CCEmails"]).Split(',').ToList(),
                    Date = Convert.ToDateTime(row["CreatedAt"])
                    // Populate other properties as needed
                };

                // Add DsrSearch object to the list
                entities.Add(dsr);
            }
            return entities;
        }

        public Task<int> Update(ReportDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
