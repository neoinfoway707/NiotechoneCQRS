using System.Data;

namespace NiotechoneCQRS.Infrastructure.Persistence.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
