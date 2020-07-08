using System;

namespace ChatApp.Models.Entities.DbEntities
{
    public class ChatLog
    {
        public int Id { get; set; }
        public DateTime PostAt { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }

        public ChatLog(int id, DateTime postAt, string message, string name) =>
            (Id, PostAt, Message, Name) = (id, postAt, message, name);
    }
}
