using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace DotNetCoreMySql.Dao.Builder
{
    public class DataAccessBuilderCommand : IDataAccessBuilderCommand
    {
        #region Private Members

        MySqlCommand _command;

        #endregion

        #region Construction

        public DataAccessBuilderCommand(MySqlCommand command)
        {
            _command = command;
        }

        #endregion

        #region IDataAccessBuildCommand Implementation

        public IDataAccessBuilderCommand WithInputParameter(string parameterName, MySqlDbType type, object value)
        {
            MySqlParameter parameter = new MySqlParameter(parameterName, type);
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            _command.Parameters.Add(parameter);

            return this;
        }

        public IDataAccessBuilderCommand WithOutputParameter(string parameterName, MySqlDbType type)
        {
            MySqlParameter parameter = new MySqlParameter(parameterName, type);
            parameter.Direction = ParameterDirection.Output;
            _command.Parameters.Add(parameter);

            return this;
        }

        public async Task<DataAccessResult> ExecuteNonQueryAsync()
        {
            await _command.ExecuteNonQueryAsync();

            return new DataAccessResult(_command);
        }

        public async Task<DataAccessResult> ExecuteReaderAsync()
        {
            DbDataReader reader = await _command.ExecuteReaderAsync();

            return new DataAccessResult(_command, reader);
        }

        public async Task<DataAccessResult> ExecuteScalarAsync()
        {
            object value = await _command.ExecuteScalarAsync();

            return new DataAccessResult(_command, value);
        }

        #endregion
    }
}
