using System.Collections.Generic;
using ChatApp.Models.Entities.DbEntities;
using ChatApp.Models.Entities.ViewEntities;

namespace ChatApp.Models.Mappers
{
    public interface IChatMapper
    {
        IList<ChatLogViewModel> FromChatLogToViewModel(IList<ChatLog> chatLogs);
    }
}
