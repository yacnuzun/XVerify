using System.Data;

namespace Infrastructure.Context
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
