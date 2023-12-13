using System.Data;

namespace University.Students.DataProvider
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}