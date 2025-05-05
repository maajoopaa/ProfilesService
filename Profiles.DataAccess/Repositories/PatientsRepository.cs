using Microsoft.Extensions.Configuration;
using Profiles.Domain.Models;
using Profiles.Sql.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace Profiles.DataAccess.Repositories
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string? connectionString;

        public PatientsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Patient?> GetAsync(Guid id)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.QueryFirstOrDefaultAsync<Patient>(PatientQueries.ById, new { Id = id });

                return result;
            }
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<Patient>(PatientQueries.All);

                return result.ToList();
            }
        }

        public async Task<string?> AddAsync(Patient entity)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.ExecuteAsync(PatientQueries.Add, entity);

                return result.ToString();
            }
        }

        public async Task<string?> UpdateAsync(Patient entity)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.ExecuteAsync(PatientQueries.Update, entity);

                return result.ToString();
            }
        }

        public async Task<string?> RemoveAsync(Guid id)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.ExecuteAsync(PatientQueries.Add, new { Id = id });

                return result.ToString();
            }
        }
    }
}
