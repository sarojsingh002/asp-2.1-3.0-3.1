using System;

namespace DotNetCoreMySql.Models
{
    public class User
    {
        public ulong userId { get; }
        public string UserName { get; }
        public string EmailAddress { get; }
        public ushort Age { get; }
        public DateTime DateOfBirth { get; }

        public User(ulong userId, string userName, string emailAddress, ushort age, DateTime dateOfBirth)
        {
            UserName = userName;
            EmailAddress = emailAddress;
            Age = age;
            DateOfBirth = dateOfBirth;
        }
    }
}
