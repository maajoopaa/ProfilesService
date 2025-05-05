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
    public class ReceptionistsRepository : IReceptionistsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string? connectionString;

        public ReceptionistsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Receptionist>> GetAllAsync()
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<Receptionist>(ReceptionistQueries.All);

                return result.ToList();
            }
        }

        public async Task<Receptionist?> GetAsync(Guid id)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.QueryFirstOrDefaultAsync<Receptionist>(ReceptionistQueries.ById, new { Id = id });

                return result;
            }
        }

        public async Task<string> AddAsync(Receptionist entity)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var result = await connection.ExecuteAsync(ReceptionistQueries.Add, entity);

                return result.ToString();
            }
        }

        public async Task<string> UpdateAsync(Receptionist entity)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.ExecuteAsync(ReceptionistQueries.Update, entity);

                return result.ToString();
            }
        }

        public async Task<string> RemoveAsync(Guid id)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.ExecuteAsync(ReceptionistQueries.Add, new { Id = id });

                return result.ToString();
            }
        }
    }
}
