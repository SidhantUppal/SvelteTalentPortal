using System.Data;
using TalentPortal.BAL.Dto;
using TalentPortal.BAL.Interfaces;
using TalentPortal.DAL;
using TalentPortal.DAL.Models;

namespace TalentPortal.BAL.Services
{
    public class CommonService : ICommonService
    {
        private readonly IDataRepository repository;
        public CommonService(IDataRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            string query = "Select * from projects";
            var dataTable = repository.ExecuteQuery(query);
            List<Project> entities = new List<Project>();
            foreach (DataRow row in dataTable.Rows)
            {
                Project entity = new Project
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = Convert.ToString(row["Name"])
                };
                entities.Add(entity);
            }
            return entities;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            string query = "Select * from Users";
            var dataTable = repository.ExecuteQuery(query);
            List<UserDto> entities = new List<UserDto>();
            foreach (DataRow row in dataTable.Rows)
            {
                UserDto entity = new UserDto
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Email = Convert.ToString(row["Email"])
                };
                entities.Add(entity);
            }
            return entities;
        }

        public async Task<List<LeaveType>> GetLeaveTypes()
        {
            string query = "Select * from LeaveTypes";
            var dataTable = repository.ExecuteQuery(query);
            List<LeaveType> entities = new List<LeaveType>();
            foreach (DataRow row in dataTable.Rows)
            {
                LeaveType entity = new LeaveType
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = Convert.ToString(row["Name"])
                };
                entities.Add(entity);
            }
            return entities;
        }
    }
}
