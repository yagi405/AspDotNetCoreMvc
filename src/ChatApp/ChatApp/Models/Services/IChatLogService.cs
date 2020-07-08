using System.Collections.Generic;
using ChatApp.Models.Entities.DbEntities;

namespace ChatApp.Models.Services
{
    public interface IChatLogService
    {
        IList<ChatLog> GetLatest(int count = 20);
    }
}
