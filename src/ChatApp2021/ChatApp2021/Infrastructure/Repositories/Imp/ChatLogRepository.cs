using System;
using System.Collections.Generic;
using System.Data;
using ChatApp2021.Infrastructure.Entities;

namespace ChatApp2021.Infrastructure.Repositories.Imp
{
    public class ChatLogRepository : AbstractRepository, IChatLogRepository
    {
        public ChatLogRepository(IDbConnection dbConnection)
            : base(dbConnection) { }

        public IList<ChatLog> GetLatest(int count = 20)
        {
            var cmdText = $@"
select top {count}
    Id,
    PostAt,
    Message,
    UserId
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
                chatLogs.Add(new ChatLog
                (
                    (int)reader["Id"],
                    (DateTime)reader["PostAt"],
                    reader["Message"] as string,
                    reader["UserId"] as string
                ));
            }
            return chatLogs;
        }
    }
}
