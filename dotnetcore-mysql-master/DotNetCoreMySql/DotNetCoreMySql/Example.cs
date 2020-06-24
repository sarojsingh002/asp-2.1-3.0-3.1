using DotNetCoreMySql.Dao.UserDataAccess;
using DotNetCoreMySql.Models;
using System;

namespace DotNetCoreMySql
{
    public class Example
    {
        public async static void Main(string[] args)
        {
            string connectionString = "Server=<ServerAddress>;Database=<DataBaseName>;Uid=<DataBaseUserName>;Pwd=<DataBasePassword";

            using (IUserDataAccess dao = new UserDataAccess(connectionString))
            {
                ulong userId = await dao.CreateUserAsync("nruffing", "nicholasruffing70@gmail.com", 26, new DateTime(1990, 1, 1));

                User user = await dao.GetUserAsync(userId);

                await dao.DeleteUserAsync(userId);
            }
        }
    }
}
