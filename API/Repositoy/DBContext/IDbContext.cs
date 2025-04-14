using System.Data;

namespace API.Repository.DBContext
{
    public interface IDbContext
    {
        IDbConnection Connection { get; }
    }
}
