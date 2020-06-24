using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DotNetCoreMySql.Dao
{
    public class DataAccessBase : IDisposable
    {
        #region Private Members

        private readonly MySqlConnection _connection;

        #endregion

        #region Construction

        protected DataAccessBase(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        }

        #endregion
        
        #region Protected Methods
        
        protected MySqlCommand CreateStoredProcedureCommand(string procedureName)
        {
            MySqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedureName;
            return cmd;
        }

        protected MySqlParameter CreateInputParameter(string parameterName, MySqlDbType type, object value)
        {
            MySqlParameter parameter = new MySqlParameter(parameterName, type);
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        protected MySqlParameter CreateOutputParameter(string parameterName, MySqlDbType type)
        {
            MySqlParameter parameter = new MySqlParameter(parameterName, type);
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        MySqlConnection.ClearPool(_connection);
                        _connection.Close();
                    }
                    _connection.Dispose();
                }
            }
        }

        #endregion
    }
}
