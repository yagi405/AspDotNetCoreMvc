using System.Collections.Generic;
using System.Linq;
using ChatApp.Models.Entities.DbEntities;
using ChatApp.Models.Entities.ViewEntities;

namespace ChatApp.Models.Mappers.Imp
{
    public class ChatMapper : IChatMapper
    {
        public IList<ChatLogViewModel> FromChatLogToViewModel(IList<ChatLog> chatLogs)
        {
            return chatLogs
                .Select(x => new ChatLogViewModel()
                {
                    PostAt = x.PostAt,
                    Message = x.Message,
                })
                .ToList();
        }
    }
}
