using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Repository.DBContext
{
    public class DbContext : IDbContext
    {
        public readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DBConn");
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
    }
}
