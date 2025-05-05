using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Profiles.Domain.Models;
using Profiles.Sql.Queries;
using System.Data;
using System.Data.SqlClient;

namespace Profiles.DataAccess.Repositories
{
    public class DoctorsRepository : IDoctorsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string? connectionString;

        public DoctorsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<Doctor>(DoctorQueries.All);

                return result.ToList();
            }
        }

        public async Task<Doctor?> GetAsync(Guid id)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.QueryFirstOrDefaultAsync<Doctor>(DoctorQueries.ById, new { Id = id });

                return result;
            }
        }

        public async Task<string> AddAsync(Doctor entity)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.ExecuteAsync(DoctorQueries.Add, entity);

                return result.ToString();
            }
        }

        public async Task<string> UpdateAsync(Doctor entity)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.ExecuteAsync(DoctorQueries.Update, entity);

                return result.ToString();
            }
        }

        public async Task<string> RemoveAsync(Guid id)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.ExecuteAsync(DoctorQueries.Add, new { Id = id });

                return result.ToString();
            }
        }
    }
}
