using Microsoft.Data.SqlClient;
using System.Data;
using TalentPortal.BAL.Dto;
using TalentPortal.BAL.Helpers;
using TalentPortal.BAL.Interfaces;
using TalentPortal.DAL;

namespace TalentPortal.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IDataRepository _repo;
        public UserService(IDataRepository dataRepository)
        {
            _repo = dataRepository;
        }
        public async Task<int> CreateUser(UserDto user)
        {
            if (user == null)
            {
                return 0;
            }

            string query = "INSERT INTO Users (FirstName, Username, LastName,Email, Salt) VALUES (@FirstName,@Username, @LastName,@Email, @Salt)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@FirstName", user.FirstName),
                new SqlParameter("@Username",user.UserName),
                new SqlParameter("@LastName", user.LastName),
                new SqlParameter("@Email",user.Email),
                new SqlParameter("@Salt",SecretHasher.Hash(user.Password))
            };

            return await _repo.ExecuteCreateQueryAsync(query, parameters);
        }

        public int DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> GetUser(string username)
        {
            string query = "SELECT * FROM Users WHERE username = @username";
            var parameters = new[]
            {new SqlParameter("@username", username)};

            DataTable dataTable = _repo.ExecuteQuery(query, parameters);

            // Check if any rows were returned
            if (dataTable.Rows.Count == 0)
            {
                return null; // No user found with the specified ID
            }

            // Extract data from the first row of the DataTable
            DataRow row = dataTable.Rows[0];

            // Populate UserDto object
            UserDto user = new UserDto
            {
                Id = Convert.ToInt32(row["Id"]),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                UserName = row["Username"].ToString(),
                Password = row["Salt"].ToString(),
                // Populate other properties as needed
            };

            return user;
        }

        public async Task<UserDto> GetUser(int userId)
        {
            string query = "SELECT * FROM Users WHERE Id = @userId";
            var parameters = new[]
            {new SqlParameter("@userId", userId)};

            DataTable dataTable = _repo.ExecuteQuery(query, parameters);

            // Check if any rows were returned
            if (dataTable.Rows.Count == 0)
            {
                return null; // No user found with the specified ID
            }

            // Extract data from the first row of the DataTable
            DataRow row = dataTable.Rows[0];

            // Populate UserDto object
            UserDto user = new UserDto
            {
                Id = Convert.ToInt32(row["Id"]),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"] != DBNull.Value ? row["LastName"].ToString() : "",
                UserName = row["Username"].ToString(),
                Password = row["Salt"].ToString(),
                Email = row["Email"].ToString()
                // Populate other properties as needed
            };

            return user;
        }
        public int UpdateUser(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
