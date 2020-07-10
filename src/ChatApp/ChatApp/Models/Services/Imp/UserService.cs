using System.Collections.Generic;
using System.Data;
using ChatApp.Models.Entities;
using ChatApp.Models.Entities.DbEntities;
using ChatApp.Models.Extensions;

namespace ChatApp.Models.Services.Imp
{
    public class UserService : AbstractDbService, IUserService
    {
        public UserService(IDbConnection dbConnection)
            : base(dbConnection) { }

        public User GetById(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            const string cmdText = @"
select
	UserId,
	UserName,
	PasswordType,
	PasswordSalt,
	Password,
	IsAdministrator
from
	Users
where
	UserId = @userId
";
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = cmdText;
            var p = cmd.CreateParameter();
            p.ParameterName = "@userId";
            p.Value = userId;
            cmd.Parameters.Add(p);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return new User(
                    reader["UserId"].ToString(),
                    reader["UserName"].ToString(),
                    (PasswordType)reader["PasswordType"],
                    reader["PasswordSalt"] as string,
                    reader["Password"] as string,
                    (bool)reader["IsAdministrator"]
                );
            }
            return null;
        }

        public IList<User> GetAll()
        {
            const string cmdText = @"
select
	UserId,
	UserName,
	PasswordType,
	PasswordSalt,
	Password,
	IsAdministrator
from
	Users
order by
    UserId
";
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = cmdText;
            using var reader = cmd.ExecuteReader();
            var users = new List<User>();
            while (reader.Read())
            {
                users.Add(
                new User(
                        reader["UserId"].ToString(),
                        reader["UserName"].ToString(),
                        (PasswordType)reader["PasswordType"],
                        reader["PasswordSalt"] as string,
                        reader["Password"] as string,
                        (bool)reader["IsAdministrator"]
                        )
                );
            }
            return users;
        }

        public bool ChangePassword(User user, string salt, string hashedPassword)
        {
            const string cmdText = @"
Update 
	Users
set
	PasswordType = @passwordType,
	PasswordSalt = @passwordSalt,
	Password = @password
where
	UserId = @userId 
";
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.AddParameter("@passwordType", PasswordType.Hashed);
            cmd.AddParameter("@passwordSalt", salt);
            cmd.AddParameter("@password", hashedPassword);
            cmd.AddParameter("@userId", user.UserId);
            return cmd.ExecuteNonQuery() == 1;
        }
    }
}