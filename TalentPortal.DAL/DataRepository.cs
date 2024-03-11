using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace TalentPortal.DAL
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;
        public DataRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async void ExecuteNonQueryAsync(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<int> ExecuteCreateQueryAsync(string query, SqlParameter[] parameters = null)
        {
            object insertedId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        command.CommandText += " SELECT SCOPE_IDENTITY()"; // Replace 'Id' with the actual ID column name

                        await connection.OpenAsync();
                        // Execute the command and retrieve the inserted ID
                        insertedId = await command.ExecuteScalarAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here, such as logging or rethrowing
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if necessary
            }

            return Convert.ToInt32(insertedId);
        }


        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
        public async Task<int> ExecuteUpdateQueryAsync(string query, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        await connection.OpenAsync();
                        // Execute the command and retrieve the number of rows affected
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here, such as logging or rethrowing
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if necessary
            }

            return rowsAffected;
        }


    }
    public interface IDataRepository
    {
        public void ExecuteNonQueryAsync(string query, SqlParameter[] parameters = null);
        public Task<int> ExecuteCreateQueryAsync(string query, SqlParameter[] parameters = null);
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null);
        public Task<int> ExecuteUpdateQueryAsync(string query, SqlParameter[] parameters = null);
    }

}
