using DotNetCoreMySql.Models;
using System;
using System.Threading.Tasks;

namespace DotNetCoreMySql.Dao.UserDataAccess
{
    public interface IUserDataAccess : IDisposable
    {
        Task<ulong> CreateUserAsync(string userName, string emailAddress, ushort age, DateTime dateOfBirth);

        Task<User> GetUserAsync(ulong userId);

        Task DeleteUserAsync(ulong userId);
    }
}
