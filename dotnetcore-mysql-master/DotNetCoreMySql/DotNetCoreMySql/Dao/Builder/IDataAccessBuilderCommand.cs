using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Threading.Tasks;

namespace DotNetCoreMySql.Dao.Builder
{
    public interface IDataAccessBuilderCommand
    {
        IDataAccessBuilderCommand WithInputParameter(string parameterName, MySqlDbType type, object value);

        IDataAccessBuilderCommand WithOutputParameter(string parameterName, MySqlDbType type);

        Task<IDataAccessResult> ExecuteNonQueryAsync();

        Task<IDataAccessResult> ExecuteScalarAsync();

        Task<IDataAccessResult> ExecuteReaderAsync();
    }
}
