using System;
using System.Collections.Generic;
using System.Data;
using ChatApp.Models.Entities.DbEntities;

namespace ChatApp.Models.Services.Imp
{
    public class ChatLogService : AbstractDbService, IChatLogService
    {
        public ChatLogService(IDbConnection dbConnection) : base(dbConnection) { }

        public IList<ChatLog> GetLatest(int count = 20)
        {
            var cmdText = $@"
select top {count}
    Id,
	PostAt,
	Message,
	Name
from
	ChatLogs
order by
	PostAt desc
";
            var chatLogs = new List<ChatLog>();
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = cmdText;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                chatLogs.Add(new ChatLog(
                    (int)reader["Id"],
                    (DateTime)reader["PostAt"],
                    reader["Message"] as string,
                    reader["Name"] as string
                ));
            }
            return chatLogs;
        }

        public void Post(string message, string name)
        {
            const string cmdText = @"
insert into 
	ChatLogs(PostAt,Message,Name)
Values
	(SYSDATETIME(),@message,@name)
";
            using var cmd = Connection.CreateCommand();
            cmd.CommandText = cmdText;
            var pMessage = cmd.CreateParameter();
            pMessage.ParameterName = "@message";
            pMessage.Value = message;
            cmd.Parameters.Add(pMessage);
            var pName = cmd.CreateParameter();
            pName.ParameterName = "@name";
            pName.Value = name;
            cmd.Parameters.Add(pName);

            cmd.ExecuteNonQuery();
        }
    }
}
