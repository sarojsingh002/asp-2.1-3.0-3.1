using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

namespace DotNetCoreMySql.Dao.Builder
{
    public interface IDataAccessResult : IDisposable
    {
        MySqlCommand Command { get; }
        object ScalarResult { get; }
        DbDataReader Reader { get; }
    }
}
