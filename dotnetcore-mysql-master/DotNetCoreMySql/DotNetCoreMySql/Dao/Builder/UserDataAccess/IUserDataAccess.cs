using DotNetCoreMySql.Models;
using System;
using System.Threading.Tasks;

namespace DotNetCoreMySql.Dao.Builder.UserDataAccess
{
    public interface IUserDataAccess
    {
        Task<ulong> CreateUserAsync(string userName, string emailAddress, ushort age, DateTime dateOfBirth);

        Task<User> GetUserAsync(ulong userId);

        Task DeleteUserAsync(ulong userId);
    }
}
