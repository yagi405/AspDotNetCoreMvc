using System.Collections.Generic;
using System.Data;
using ChatApp.Common;
using ChatApp.Infrastructure.Persistence.Entities;

namespace ChatApp.Infrastructure.Persistence.Repositories.Imp
{
    public class UserRepository : AbstractRepository, IUserRepository
    {
        public UserRepository(IDbConnection dbConnection)
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
	IsAdministrator,
    IconUrl
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
                    (bool)reader["IsAdministrator"],
                    reader["IconUrl"] as string
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
	IsAdministrator,
    IconUrl
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
                        (bool)reader["IsAdministrator"],
                        reader["IconUrl"] as string
                        )
                );
            }
            return users;
        }

        public void ChangePassword(string userId, string salt, string hashedPassword)
        {
            Args.NotEmpty(userId, nameof(userId));
            Args.NotEmpty(salt, nameof(salt));
            Args.NotEmpty(hashedPassword, nameof(hashedPassword));

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
            cmd.AddParameter("@userId", userId);
            cmd.ExecuteNonQuery();
        }

        public void ChangeUserName(string userId, string userName)
        {
            Args.NotEmpty(userId, nameof(userId));
            Args.NotEmpty(userName, nameof(userName));

            const string cmdText = @"
update 
	Users
set
	UserName = @userName
where
	UserId = @userId 
";
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.AddParameter("@userId", userId);
            cmd.AddParameter("@userName", userName);
            cmd.ExecuteNonQuery();
        }

        public void ChangeUserIcon(string userId, string iconUrl)
        {
            Args.NotEmpty(userId, nameof(userId));
            Args.NotEmpty(iconUrl, nameof(iconUrl));

            const string cmdText = @"
update 
	Users
set
	IconUrl = @iconUrl
where
	UserId = @userId 
";
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.AddParameter("@userId", userId);
            cmd.AddParameter("@iconUrl", iconUrl);
            cmd.ExecuteNonQuery();
        }

        public User Create(User user)
        {
            Args.NotNull(user, nameof(user));

            const string cmdText = @"
insert into
	Users(
		UserId,
		UserName,
		PasswordType,
		PasswordSalt,
		Password,
		IsAdministrator
	)
values(@userId,@userName,@passwordType,@passwordSalt,@password,@isAdministrator)
";

            using var cmd = Connection.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.AddParameter("@userId", user.UserId);
            cmd.AddParameter("@userName", user.UserName);
            cmd.AddParameter("@passwordType", user.PasswordType);
            cmd.AddParameter("@passwordSalt", user.PasswordSalt);
            cmd.AddParameter("@password", user.Password);
            cmd.AddParameter("@isAdministrator", user.IsAdministrator);
            cmd.ExecuteNonQuery();

            return user;
        }

        public void Edit(string userId, string userName, bool isAdministrator)
        {
            Args.NotEmpty(userId, nameof(userId));
            Args.NotEmpty(userName, nameof(userName));

            const string cmdText = @"
update 
	Users
set
	UserName = @userName,
	IsAdministrator = @isAdministrator
where
	UserId = @userId 
";
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.AddParameter("@userId", userId);
            cmd.AddParameter("@userName", userName);
            cmd.AddParameter("@isAdministrator", isAdministrator);
            cmd.ExecuteNonQuery();
        }

        public void Delete(string userId)
        {
            Args.NotEmpty(userId, nameof(userId));

            const string cmdText = @"
delete from
	Users
where
	UserId = @userId
";
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = cmdText;
            cmd.AddParameter("@userId", userId);
            cmd.ExecuteNonQuery();
        }
    }
}
