using System;

namespace DotNetCoreMySql.Dao.Builder
{
    public interface IDataAccessBuilder : IDisposable
    {
        IDataAccessBuilderCommand CreateStoredProcedureCommand(string procedureName);
    }
}
