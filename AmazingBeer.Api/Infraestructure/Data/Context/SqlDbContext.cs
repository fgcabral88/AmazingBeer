using Microsoft.Data.SqlClient;
using System.Data;

namespace AmazingBeer.Api.Infraestructure.Data.Context
{
    public class SqlDbContext
    {
        private readonly string _connectionString;

        public SqlDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
