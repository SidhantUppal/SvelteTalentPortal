using Microsoft.Data.SqlClient;
using System.Data;
using TalentPortal.BAL.Interfaces;
using TalentPortal.DAL;
using TalentPortal.DAL.Models;

namespace TalentPortal.BAL.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDataRepository _dataRepository;
        public DashboardService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;

        }
        public async Task<List<AttendanceLog>> GetAttendanceLogAsync(int userId)
        {
            string query = "SELECT * FROM AttendanceLog WHERE UserId = @userId";
            var parameters = new[]
            {
                new SqlParameter("@userId", userId)
            };

            DataTable dataTable = _dataRepository.ExecuteQuery(query, parameters);

            // Check if any rows were returned
            if (dataTable.Rows.Count == 0)
            {
                return new List<AttendanceLog>(); // Return an empty list if no logs found
            }

            // Initialize a list to store AttendanceLog objects
            List<AttendanceLog> attendanceLogs = new List<AttendanceLog>();

            // Iterate over each row in the DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Populate AttendanceLog object from each row
                AttendanceLog attendanceLog = new()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    UserId = Convert.ToInt32(row["UserId"]),
                    TimeIn = Convert.ToDateTime(row["TimeIn"]),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    TimeOut = row["TimeOut"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["TimeOut"]) : null,
                    TotalWorkingHours = row["TotalWorkingHours"] != DBNull.Value ? TimeSpan.Parse(Convert.ToString(row["TotalWorkingHours"])) : (TimeSpan?)null,
                    Status = Convert.ToString(row["Status"]),
                };

                // Add AttendanceLog object to the list
                attendanceLogs.Add(attendanceLog);
            }

            return attendanceLogs;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(int id)
        {
            string query = "SELECT p.* FROM Projects p Inner join ProjectMapping pm on pm.UserId = p.Id pm.UserId = @userId";
            var parameters = new[]
            {
                new SqlParameter("@userId", id)
            };

            DataTable dataTable = _dataRepository.ExecuteQuery(query, parameters);

            // Check if any rows were returned
            if (dataTable.Rows.Count == 0)
            {
                return new List<Project>(); // Return an empty list if no logs found
            }

            // Initialize a list to store AttendanceLog objects
            List<Project> projects = new List<Project>();

            // Iterate over each row in the DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Populate AttendanceLog object from each row
                Project project = new()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = Convert.ToString(row["Name"]),
                };

                // Add AttendanceLog object to the list
                projects.Add(project);
            }

            return projects;
        }

        public async Task<AttendanceLog> GetTodayAttendance(int userId)
        {
            string getQ = "SELECT * FROM AttendanceLog WHERE CONVERT(DATE, CreatedAt) = CONVERT(DATE, GETDATE()) AND UserId = @userId";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            DataTable dataTable = _dataRepository.ExecuteQuery(getQ, parameters);
            if (dataTable.Rows.Count == 0)
            {
                return null;
            }
            DataRow row = dataTable.Rows[0];

            // Populate UserDto object
            AttendanceLog log = new AttendanceLog
            {
                Id = Convert.ToInt32(row["Id"]),
                TimeIn = Convert.ToDateTime(row["TimeIn"]),
                TimeOut = row["TimeOut"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["TimeOut"]) : null,
                TotalWorkingHours = row["TotalWorkingHours"] != DBNull.Value ? TimeSpan.Parse(Convert.ToString(row["TotalWorkingHours"])) : (TimeSpan?)null,
                //CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                //Status = Convert.ToString(row["Status"]),
                UserId = Convert.ToInt32(row["UserId"]),
            };

            //if (row["TotalWorkingHours"] != null)
            //{
            //    log.TotalWorkingHours = log.TimeIn - log.TimeOut;
            //    log.Status = log.TotalWorkingHours >= TimeSpan.FromHours(9.5) ? "Present" : "Absent";
            //}
            //else
            //{
            //    log.TotalWorkingHours = TimeSpan.Zero;  // Assume working
            //    log.Status = "Working";
            //}
            return log;
        }

        public async Task<int> RecordTime(int userId, bool timeOut)
        {
            if (userId == 0)
            {
                return 0;
            }
            string getQ = "SELECT * FROM AttendanceLog WHERE CONVERT(DATE, CreatedAt) = CONVERT(DATE, GETDATE()) AND UserId = @userId";
            SqlParameter[] parameters = { new SqlParameter("@userId", userId) };
            DataTable dataTable = _dataRepository.ExecuteQuery(getQ, parameters);
            DateTime timeInField = DateTime.Now;
            string query;
            // Check if any rows were returned
            if (dataTable.Rows.Count == 0)
            {
                query = "INSERT INTO AttendanceLog (TimeIn, Status, UserId) " +
               "VALUES (@TimeIn, @Status, @UserId)";
                SqlParameter[] insertParameters =
                    {
                        new SqlParameter("@TimeIn", timeInField),
                        new SqlParameter("@Status", "Working"),
                        new SqlParameter("@UserId",  userId),

                    };
                return await _dataRepository.ExecuteCreateQueryAsync(query, insertParameters);
            }
            else
            {
                DataRow row = dataTable.Rows[0];
                string status = "";
                DateTime? timeOutField = DateTime.Now;
                TimeSpan? totalWorkingHours = null;
                if (timeOut)
                {
                    totalWorkingHours = timeOutField - Convert.ToDateTime(row["TimeIn"]);
                    status = totalWorkingHours >= TimeSpan.FromHours(9.5) ? "Present" : "Absent";
                }
                query = "UPDATE AttendanceLog SET " +
               "    TimeOut = @TimeOut, " +
               "    TotalWorkingHours = @TotalWorkingHours, " +
               "    Status = @Status, " +
               "    UserId = @UserId " +
               "    WHERE Id = @Id";

                SqlParameter[] updateParameters =
                {
                new SqlParameter("@Id", row["Id"]),
                new SqlParameter("@TimeOut", timeOutField),
                new SqlParameter("@TotalWorkingHours", totalWorkingHours),
                new SqlParameter("@Status", status),
                new SqlParameter("@UserId", userId)
                };

                return await _dataRepository.ExecuteUpdateQueryAsync(query, updateParameters);

            }
        }
    }
}
