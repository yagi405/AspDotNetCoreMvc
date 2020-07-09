using System;

namespace ChatApp.Models.Entities.DbEntities
{
    public class ChatLog
    {
        public int Id { get; set; }
        public DateTime PostAt { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }

        public ChatLog(int id, DateTime postAt, string message, string userId) =>
            (Id, PostAt, Message, UserId) = (id, postAt, message, userId);
    }
}
