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
    public class SpecializationsRepository : ISpecializationsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string? connectionString;

        public SpecializationsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Specialization?> GetAsync(Guid id)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.QueryFirstOrDefaultAsync(SpecializationQueries.ById, new { Id = id });

                return result;
            }
        }

        public async Task<List<Specialization>> GetAllAsync()
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<Specialization>(SpecializationQueries.All);

                return result.ToList();
            }
        }
    }
}
