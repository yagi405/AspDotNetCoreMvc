using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models.Entities.DbEntities
{
    public class ChatLog
    {
        public int Id { get; set; }
        public DateTime PostAt { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
    }
}
