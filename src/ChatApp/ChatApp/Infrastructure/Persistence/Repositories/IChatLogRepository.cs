using System.Collections.Generic;
using ChatApp.Infrastructure.Persistence.Entities;

namespace ChatApp.Infrastructure.Persistence.Repositories
{
    public interface IChatLogRepository
    {
        IList<ChatLog> GetLatest(int count = 20);

        void Add(string message, string userId);
    }
}
