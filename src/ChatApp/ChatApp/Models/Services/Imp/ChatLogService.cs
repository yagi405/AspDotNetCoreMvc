using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models.Services.Imp
{
    public class ChatLogService : AbstractDbService, IChatLogService
    {
        public ChatLogService(IDbConnection dbConnection) : base(dbConnection) { }


    }
}
