using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotNetCoreMySql.Models;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace DotNetCoreMySql.Dao.UserDataAccess
{
    /// <summary>
    /// The following class shows an example implementation of how DataAccessBase can be used to interact
    /// with a MySQL database by calling the appropiate stored procedures.
    /// 
    /// This class assumes that the MySQL database contains a table in the following structure.
    /// 
    /// CREATE TABLE user
    /// (
    ///     userid BIGINT UNSIGNED,
    ///     username VARCHAR(80),
    ///     emailaddress VARCHAR(150),
    ///     age TINYINT UNSIGNED,
    ///     dateofbirth DATETIME,
    ///     PRIMARY KEY (userid)
    /// );
    /// </summary>
    public class UserDataAccess : DataAccessBase, IUserDataAccess
    {
        #region Constants

        private const string CreateUserProcedureName = "create_user";
        private const string GetUserProcedureName = "get_user";
        private const string DeleteUserProcedureName = "delete_user";

        private const string UserIdParameterName = "@userId";
        private const string UserNameParameterName = "@userName";
        private const string EmailAddressParameterName = "@emailAddress";
        private const string AgeParameterName = "@age";
        private const string DateOfBirthParameterName = "@dateOfBirth";

        private const string UserNameColumnName = "username";
        private const string EmailAddressColumnName = "emailaddress";
        private const string AgeColumnName = "age";
        private const string DateOfBirthColumnName = "dateofbirth";

        #endregion

        #region Construction

        public UserDataAccess(string connectionString)
            : base(connectionString)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a record to the user table with the specified information and returns the user id of the 
        /// new user.
        /// 
        /// This method assumes that the MySQL database contains a stored procedure that inserts a new user and has the following signature:
        /// 
        /// CREATE PROCEDURE 'create_user' (IN userName VARCHAR(80), IN emailAddress VARCHAR(150), IN age TINYINT UNSIGNED, IN dateOfBirth DATETIME, OUT userId BIGINT UNSIGNED)
        /// 
        /// </summary>
        /// <param name="userName">The username of the new user.</param>
        /// <param name="emailAddress">The email address of the new user.</param>
        /// <param name="age">The age of the new user.</param>
        /// <param name="dateOfBirth">The date of birth of the new user.</param>
        /// <returns>The user id of the new user.</returns>
        public async Task<ulong> CreateUserAsync(string userName, string emailAddress, ushort age, DateTime dateOfBirth)
        {
            MySqlCommand command = CreateStoredProcedureCommand(CreateUserProcedureName);

            command.Parameters.Add(CreateInputParameter(UserNameParameterName, MySqlDbType.UInt64, userName));
            command.Parameters.Add(CreateInputParameter(EmailAddressParameterName, MySqlDbType.VarString, emailAddress));
            command.Parameters.Add(CreateInputParameter(AgeColumnName, MySqlDbType.VarString, age));
            command.Parameters.Add(CreateInputParameter(DateOfBirthColumnName, MySqlDbType.VarString, dateOfBirth));

            command.Parameters.Add(CreateOutputParameter(UserIdParameterName, MySqlDbType.UInt64));

            await command.ExecuteNonQueryAsync();

            return Convert.ToUInt64(command.Parameters[UserIdParameterName].Value);
        }

        /// <summary>
        /// Retrieves the user with the given user id, null otherwise.
        /// 
        /// This method assumes that the MySQL database contains a stored procedure that selects the row with the given user id that has the following signature:
        /// 
        /// CREATE PROCEDURE 'get_user' (IN userId BIGINT UNSIGNED)
        /// 
        /// </summary>
        /// <param name="userId">The user id of the user that will be retrieved.</param>
        /// <returns>The user with the given user id, null otherwise.</returns>
        public async Task<User> GetUserAsync(ulong userId)
        {
            User user = null;

            MySqlCommand command = CreateStoredProcedureCommand(GetUserProcedureName);

            command.Parameters.Add(CreateInputParameter(UserIdParameterName, MySqlDbType.UInt64, userId));

            using (DbDataReader reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync()) // If you need to iterate over a collection of results you could easily change this to a while loop.
                {
                    string userName = Convert.ToString(reader[UserNameColumnName]);
                    string emailAddress = Convert.ToString(reader[EmailAddressColumnName]);
                    ushort age = Convert.ToUInt16(reader[AgeColumnName]);
                    DateTime dateOfBirth = Convert.ToDateTime(reader[DateOfBirthColumnName]);

                    user = new User(userId, userName, emailAddress, age, dateOfBirth);
                }
            }

            return user;
        }

        /// <summary>
        /// Deletes the user with the specified user id.
        /// 
        /// This method assumes that the MySQL database contains a stored procedure that deleted the row with the given user id that has the following signature:
        /// 
        /// CREATE PROCEDURE 'delete_user' (IN userId BIGINT UNSIGNED)
        /// 
        /// </summary>
        /// <param name="userId">The user id of the user to be deleted.</param>
        /// <returns></returns>
        public async Task DeleteUserAsync(ulong userId)
        {
            MySqlCommand command = CreateStoredProcedureCommand(DeleteUserProcedureName);

            command.Parameters.Add(CreateInputParameter(UserIdParameterName, MySqlDbType.UInt64, userId));

            await command.ExecuteNonQueryAsync();
        }

        #endregion
    }
}
