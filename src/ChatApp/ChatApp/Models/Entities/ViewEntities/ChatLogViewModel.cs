using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class ChatLogViewModel
    {
        public DateTime PostAt { get; set; }
        public string Message { get; set; }
    }
}
