using System.Collections.Generic;
using System.Linq;
using ChatApp.Models.Entities.DbEntities;
using ChatApp.Models.Entities.ViewEntities;

namespace ChatApp.Models.Mappers.Imp
{
    public class ChatMapper : IChatMapper
    {
        public ChatIndexViewModel FromChatLogToViewModel(IList<ChatLog> chatLogs)
        {
            var details = chatLogs
                .Select(x => new ChatIndexViewModel.Detail()
                {
                    PostAt = x.PostAt,
                    Message = x.Message,
                })
                .ToList();

            return new ChatIndexViewModel()
            {
                Details = details
            };
        }
    }
}
