using System.Collections.Generic;
using ChatApp2021.Infrastructure.Entities;

namespace ChatApp2021.Infrastructure.Repositories
{
    public interface IChatLogRepository
    {
        public IList<ChatLog> GetLatest(int count = 20);
    }
}
