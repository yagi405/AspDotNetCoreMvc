using System;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class AppErrorViewModel
    {
        public int StatusCode { get; set; }

        public Exception Ex { get; set; }
    }
}
