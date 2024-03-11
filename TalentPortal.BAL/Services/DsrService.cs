using Microsoft.Data.SqlClient;
using System.Data;
using TalentPortal.BAL.Dto;
using TalentPortal.BAL.Interfaces;
using TalentPortal.DAL;

namespace TalentPortal.BAL.Services
{
    public class DsrService : IDsrService
    {
        private readonly IDataRepository dataRepository;
        public DsrService(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public async Task<int> AddDsr(DsrDto dto)
        {

            if (dto == null)
            {
                return 0;
            }

            string query = "INSERT INTO Dsrs (ProjectId, FromTime, ToTime, Description,CreatedAt,UpdatedAt,UserId) VALUES (@ProjectId,@FromTime, @ToTime, @Description,@CreatedAt,@UpdatedAt,@UserId)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProjectId", dto.ProjectId),
                new SqlParameter("@FromTime",dto.FromTime),
                new SqlParameter("@ToTime", dto.ToTime),
                new SqlParameter("@Description",dto.Description),
                new SqlParameter("@CreatedAt",DateTime.Now),
                new SqlParameter("@UpdatedAt",DateTime.Now),
                new SqlParameter("@UserId",dto.UserId)
            };
            return await dataRepository.ExecuteCreateQueryAsync(query, parameters);
        }

        public async Task<IEnumerable<DsrSearch>> GetDsr(bool isToday, int userId)
        {
            string query = isToday ?
                "SELECT * FROM Dsrs WHERE UserId = @userId AND CONVERT(date, CreatedAt) = CONVERT(date, GETDATE())" :
                "SELECT * FROM Dsrs WHERE UserId = @userId";

            var parameters = new[]
            { new SqlParameter("@userId", userId)
            };

            DataTable dataTable = dataRepository.ExecuteQuery(query, parameters);

            // Initialize a list to store DsrSearch objects
            List<DsrSearch> dsrs = new List<DsrSearch>();

            // Iterate over each row in the DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Populate DsrSearch object from each row
                DsrSearch dsr = new DsrSearch
                {
                    Id = Convert.ToInt32(row["Id"]),
                    ProjectId = Convert.ToInt32(row["ProjectId"]),
                    Description = Convert.ToString(row["Description"]),
                    UserId = Convert.ToInt32(row["UserId"]),
                    FromTime = Convert.ToDateTime(row["FromTime"]),
                    ToTime = Convert.ToDateTime(row["ToTime"])
                    // Populate other properties as needed
                };

                // Add DsrSearch object to the list
                dsrs.Add(dsr);
            }

            return dsrs;
        }

        public async Task<IEnumerable<DsrSearch>> GetDsr(int logId)
        {
            string query =
                "SELECT d.*, p.Name AS ProjectName, " +
                "(SELECT STRING_AGG(eu.Email, ',') FROM Users eu " +
                "WHERE CHARINDEX(',' + CAST(eu.Id AS VARCHAR) + ',', ',' + dl.SenderIds + ',') > 0) AS SenderEmails, " +
                "(SELECT STRING_AGG(eucc.Email, ',') FROM Users eucc " +
                "WHERE CHARINDEX(',' + CAST(eucc.Id AS VARCHAR) + ',', ',' + dl.SenderCCIds + ',') > 0) AS CCEmails " +
                "FROM dsrlog dl " +
                "INNER JOIN dsrs d ON CHARINDEX(',' + CAST(d.Id AS VARCHAR) + ',', ',' + dl.DsrIds + ',') > 0 " +
                "INNER JOIN projects p ON p.Id = d.ProjectId " +
                "WHERE dl.Id = @logId";

            var parameters = new[]
            {
                new SqlParameter("@logId", logId)
            };

            DataTable dataTable = dataRepository.ExecuteQuery(query, parameters);

            // Initialize a list to store DsrSearch objects
            List<DsrSearch> dsrs = new List<DsrSearch>();

            // Iterate over each row in the DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Populate DsrSearch object from each row
                DsrSearch dsr = new DsrSearch
                {
                    Id = Convert.ToInt32(row["Id"]),
                    ProjectId = Convert.ToInt32(row["ProjectId"]),
                    Description = Convert.ToString(row["Description"]),
                    UserId = Convert.ToInt32(row["UserId"]),
                    FromTime = Convert.ToDateTime(row["FromTime"]),
                    ToTime = Convert.ToDateTime(row["ToTime"]),
                    ProjectName = Convert.ToString(row["ProjectName"]),
                    Status = "Pending",
                    SenderEmails = Convert.ToString(row["SenderEmails"]).Split(',').ToList(), // Split sender emails into list
                    SenderCCEmails = Convert.ToString(row["CCEmails"]).Split(',').ToList()
                    // Populate other properties as needed
                };

                // Add DsrSearch object to the list
                dsrs.Add(dsr);
            }

            return dsrs;
        }

        public async Task<bool> IsDsrCompleted(int userId)
        {
            string query = "SELECT * FROM DsrLog WHERE CONVERT(DATE, SubmitDate) = CONVERT(DATE, GETDATE()) AND UserId = @userId";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            DataTable dataTable = dataRepository.ExecuteQuery(query, parameters);
            if (dataTable.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<DsrSearch>> ReceivedDsrs(int userId)
        {
            string query = "WITH FirstDsr AS (" +
                "SELECT dl.Id AS DsrLogId, " +
                "dl.SubmitDate AS DateTime," +
                "p.Name AS ProjectName," +
                "d.Id AS DsrId," +
                "d.Description," +
                "d.FromTime," +
                "d.ToTime," +
                "ROW_NUMBER() OVER (PARTITION BY dl.Id ORDER BY d.Id) AS RowNumber " +
                "FROM dsrlog dl " +
                "INNER JOIN   projects p ON p.Id = (SELECT TOP 1 ProjectId FROM dsrs WHERE CHARINDEX(',' + CAST(Id AS VARCHAR) + ',', ',' + dl.DsrIds + ',') > 0) " +
                "INNER JOIN  dsrs d ON CHARINDEX(',' + CAST(d.Id AS VARCHAR) + ',', ',' + dl.DsrIds + ',') > 0 " +
                "INNER JOIN Users u on u.Id = dl.UserId " +
                "WHERE @userId IN (SELECT CAST(value AS INT) FROM STRING_SPLIT(dl.SenderIds, ',')) ) " +
                "SELECT DsrLogId,  DateTime,  ProjectName,  DsrId, FromTime, ToTime,Description FROM " +
                "FirstDsr WHERE  RowNumber = 1;";

            var parameters = new[] { new SqlParameter("@userId", userId) };

            DataTable dataTable = dataRepository.ExecuteQuery(query, parameters);

            // Initialize a list to store DsrSearch objects
            List<DsrSearch> dsrs = new List<DsrSearch>();

            // Iterate over each row in the DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Populate DsrSearch object from each row
                DsrSearch dsr = new DsrSearch
                {
                    DsrLogId = Convert.ToInt32(row["DsrLogId"]),
                    Description = Convert.ToString(row["Description"]),
                    FromTime = Convert.ToDateTime(row["FromTime"]),
                    ToTime = Convert.ToDateTime(row["ToTime"]),
                    DateTime = Convert.ToDateTime(row["DateTime"]),
                    ProjectName = Convert.ToString(row["ProjectName"]),
                    Status = "Pending",
                    //ProjectId = Convert.ToInt32(row["ProjectId"]),

                    // Populate other properties as needed
                };

                // Add DsrSearch object to the list
                dsrs.Add(dsr);
            }

            return dsrs;
        }

        public async Task<IEnumerable<DsrSearch>> SentDsrs(int userId)
        {
            string query = "WITH FirstDsr AS (" +
                "SELECT dl.Id AS DsrLogId, " +
                "dl.SubmitDate AS DateTime," +
                "p.Name AS ProjectName," +
                "d.Id AS DsrId," +
                "d.Description," +
                "d.FromTime," +
                "d.ToTime," +
                "ROW_NUMBER() OVER (PARTITION BY dl.Id ORDER BY d.Id) AS RowNumber " +
                "FROM dsrlog dl " +
                "INNER JOIN   projects p ON p.Id = (SELECT TOP 1 ProjectId FROM dsrs WHERE CHARINDEX(',' + CAST(Id AS VARCHAR) + ',', ',' + dl.DsrIds + ',') > 0) " +
                "INNER JOIN  dsrs d ON CHARINDEX(',' + CAST(d.Id AS VARCHAR) + ',', ',' + dl.DsrIds + ',') > 0 " +
                "WHERE  dl.UserId = @userId ) " +
                "SELECT DsrLogId,  DateTime,  ProjectName,  DsrId, FromTime, ToTime,Description FROM " +
                "FirstDsr WHERE  RowNumber = 1;";

            var parameters = new[]
            { new SqlParameter("@userId", userId)
            };

            DataTable dataTable = dataRepository.ExecuteQuery(query, parameters);

            // Initialize a list to store DsrSearch objects
            List<DsrSearch> dsrs = new List<DsrSearch>();

            // Iterate over each row in the DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Populate DsrSearch object from each row
                DsrSearch dsr = new DsrSearch
                {
                    DsrLogId = Convert.ToInt32(row["DsrLogId"]),
                    Description = Convert.ToString(row["Description"]),
                    FromTime = Convert.ToDateTime(row["FromTime"]),
                    ToTime = Convert.ToDateTime(row["ToTime"]),
                    DateTime = Convert.ToDateTime(row["DateTime"]),
                    ProjectName = Convert.ToString(row["ProjectName"]),
                    Status = "Pending"
                    // Populate other properties as needed
                };

                // Add DsrSearch object to the list
                dsrs.Add(dsr);
            }

            return dsrs;
        }

        public async Task<bool> SubmitDsr(DsrSubmitDto dto, int userId)
        {
            if (dto == null || dto.DsrIds == null || dto.SenderIds == null || dto.SenderCCIds == null)
            {
                return false; // Invalid DTO, return false
            }

            try
            {
                // Construct the SQL insert query
                string query = "INSERT INTO dsrlog (UserId, DsrIds, SenderIds, SenderCCIds) VALUES ";

                // Create a comma-separated string for DsrIds, SenderIds, and SenderCCIds
                string dsrIdsString = string.Join(",", dto.DsrIds.Where(c => c != 0).Select(id => $"{id}"));
                string senderIdsString = string.Join(",", dto.SenderIds.Select(id => $"{id}"));
                string senderCCIdsString = string.Join(",", dto.SenderCCIds.Select(id => $"{id}"));

                // Append values to the insert query
                query += $"('{userId}', '{dsrIdsString}', '{senderIdsString}', '{senderCCIdsString}')";

                // Execute the query
                await dataRepository.ExecuteUpdateQueryAsync(query);

                // If execution reaches here, the query was successful
                return true;
            }
            catch (Exception ex)
            {
                // Handle the exception here, such as logging or rethrowing
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if necessary
            }
        }



        public async Task<int> UpdateDsr(DsrDto dto)
        {

            if (dto == null)
            {
                return 0;
            }

            string query = "UPDATE Dsrs SET ProjectId = @ProjectId, FromTime = @FromTime, ToTime = @ToTime, Description = @Description, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@ProjectId", dto.ProjectId),
                new SqlParameter("@FromTime", dto.FromTime),
                new SqlParameter("@ToTime", dto.ToTime),
                new SqlParameter("@Description", dto.Description),
                new SqlParameter("@UpdatedAt", DateTime.Now),
                new SqlParameter("@Id", dto.Id) // Assuming you have an Id field in the DTO
            };
            return await dataRepository.ExecuteUpdateQueryAsync(query, parameters);

        }
    }
}
