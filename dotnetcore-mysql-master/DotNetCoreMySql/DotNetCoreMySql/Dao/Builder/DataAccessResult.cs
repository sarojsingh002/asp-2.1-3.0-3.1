using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

namespace DotNetCoreMySql.Dao.Builder
{
    public class DataAccessResult : IDataAccessResult
    {
        #region IDataAccessResult Implementation

        public MySqlCommand Command { get; }

        public object ScalarResult { get; }

        public DbDataReader Reader { get; }

        #endregion

        #region Construction

        public DataAccessResult(MySqlCommand command)
        {
            Command = command;
        }

        public DataAccessResult(MySqlCommand command, object scalarResult)
            : this(command)
        {
            ScalarResult = scalarResult;
        }

        public DataAccessResult(MySqlCommand command, DbDataReader reader)
            : this(command)
        {
            Reader = reader;
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
                if (Reader != null)
                {
                    Reader.Dispose();
                }
            }
        }

        #endregion
    }
}
