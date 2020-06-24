using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DotNetCoreMySql.Dao.Builder
{
    public class DataAccessBuilder : IDataAccessBuilder
    {
        #region Member Variables

        private MySqlConnection _connection;

        #endregion

        #region Construction

        public DataAccessBuilder(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        }

        #endregion

        #region IDataAccessBuilder Implementation

        public IDataAccessBuilderCommand CreateStoredProcedureCommand(string procedureName)
        {
            MySqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedureName;

            return new DataAccessBuilderCommand(cmd);
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
